using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;
using PSharp.CodeAnalysis.Syntax.Nodes.Statements;
using PSharp.CodeAnalysis.Syntax.Parser;
using PSharp.CodeAnalysis.Text;
using System.Diagnostics;

namespace Minsk.CodeAnalysis.Syntax.Parser;


/// <summary>
/// Simple wrapper for array elements to avoid covariance issues (pattern from Roslyn).
/// </summary>
internal struct ArrayElement<T>
{
    public T Value;
}

/// <summary>
/// Represents a saved parser state for backtracking (reset point).
/// </summary>
internal struct ResetPoint
{
    public readonly int ResetCount;
    public readonly LexerMode Mode;
    public readonly int Position;           // Absolute token position (firstToken + offset)
    public readonly SyntaxNode PrevTokenTrailingTrivia;

    public ResetPoint(int resetCount, LexerMode mode, int position, SyntaxNode prevTokenTrailingTrivia)
    {
        ResetCount = resetCount;
        Mode = mode;
        Position = position;
        PrevTokenTrailingTrivia = prevTokenTrailingTrivia;
    }
}

/// <summary>
/// Roslyn‑style parser with lazy token buffer, reset points, and mode support.
/// Produces green nodes (tokens are SyntaxToken). Red nodes are built separately later.
/// </summary>
internal partial class SyntaxParser
{
    // Lexer and mode
    private readonly Lexer _lexer;
    private LexerMode _mode;
    private readonly CancellationToken _cancellationToken;
    private DiagnosticBag _diagnostic = new();
    private bool _hasInsertedMissingToken = false;

    public DiagnosticBag Diagnostics => _diagnostic;


    // Token buffer (sliding window over green tokens)
    private ArrayElement<SyntaxToken>[] _lexedTokens;
    private int _firstToken;          // Absolute index of the first token in _lexedTokens
    private int _tokenOffset;          // Index of current token within _lexedTokens
    private int _tokenCount;           // Number of valid tokens in _lexedTokens

    // Reset point tracking (for low‑water mark)
    private int _resetCount;
    private int _resetStart;            // Absolute position of the earliest reset point (or -1)

    // Cached current token and trailing trivia (for diagnostics)
    private SyntaxToken _currentToken;
    private SyntaxNode _prevTokenTrailingTrivia;

    // Diagnostics (to be attached to green nodes)
    private readonly DiagnosticBag _diagnosticBag;

    /// <summary>
    /// Initializes a new parser for the given source text.
    /// </summary>
    /// <param name="text">Source text to parse.</param>
    /// <param name="mode">Initial lexer mode.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    //public Parser(SourceText text, LexerMode mode = LexerMode.Normal, CancellationToken cancellationToken = default)
    public SyntaxParser(SourceText text)
    {
        _lexer = new Lexer(text);          // Assume Lexer now returns SyntaxToken
        //_mode = mode;
        //_cancellationToken = cancellationToken;

        // Initialize token buffer with a reasonable initial size (e.g., 32)
        _lexedTokens = new ArrayElement<SyntaxToken>[32];
        _firstToken = 0;
        _tokenOffset = 0;
        _tokenCount = 0;
        _resetCount = 0;
        _resetStart = -1;

        _diagnosticBag = new DiagnosticBag();
    }

    // ----------------------------------------------------------------------
    // Token management (lazy acquisition, sliding window)
    // ----------------------------------------------------------------------

    /// <summary>
    /// Gets the current token (the one at peek offset 0). Cached for performance.
    /// </summary>
    private SyntaxToken CurrentToken
    {
        get
        {
            if (_currentToken == null)
            {
                _currentToken = PeekToken(0);
            }
            return _currentToken;
        }
    }

    /// <summary>
    /// Returns the token at the given lookahead offset (0‑based from current position).
    /// Fetches more tokens from the lexer if necessary.
    /// </summary>
    private SyntaxToken PeekToken(int offset)
    {
        Debug.Assert(offset >= 0);
        int targetIndex = _tokenOffset + offset;
        //int targetIndex = _tokenOffset - _firstToken + offset;

        // Ensure enough tokens are available
        while (targetIndex >= _tokenCount)
        {
            FetchMoreTokens();
        }

        return _lexedTokens[targetIndex].Value;
    }


    /// <summary>
    /// Fetches one more token from the lexer and appends it to the buffer.
    /// </summary>
    private void FetchMoreTokens()
    {
        // Fetch the next token using the current lexer mode
        //var token = _lexer.Lex(_mode);
        var token = _lexer.Lex();
        if (token.Kind == SyntaxKind.EndOfFileToken)
        {
            // EOF token is always added; we stop fetching after that.
        }

        // Ensure buffer capacity
        if (_tokenCount >= _lexedTokens.Length)
        {
            var newArray = new ArrayElement<SyntaxToken>[_lexedTokens.Length * 2];
            Array.Copy(_lexedTokens, 0, newArray, 0, _tokenCount);
            _lexedTokens = newArray;
        }

        // Append the new token
        _lexedTokens[_tokenCount].Value = token;
        _tokenCount++;
    }

    /// <summary>
    /// Consumes the current token and advances to the next.
    /// </summary>
    private SyntaxToken EatToken()
    {
        var token = CurrentToken;
        MoveToNextToken();
        return token;
    }

    /// <summary>
    /// Consumes the current token if its kind matches <paramref name="kind"/>;
    /// otherwise creates a missing token of the expected kind with an appropriate diagnostic.
    /// </summary>
    private SyntaxToken EatToken(SyntaxKind kind)
    {
        if (CurrentToken.Kind == kind)
        {
            return EatToken();
        }

        // Create a missing token of the expected kind

        if (_hasInsertedMissingToken)
        {
            var missingGreen = new GreenToken(kind, string.Empty, null, isMissing: true);
            return new SyntaxToken(missingGreen, null, CurrentToken.Position);
        }

        _hasInsertedMissingToken = true;
        var info = new DiagnosticInfo(ErrorCode.ERR_UnexpectedToken, DiagnosticSeverity.Error, CurrentToken.Span, CurrentToken.Kind, kind);
        var green = new GreenToken(kind, string.Empty, null, isMissing: true);
        var greenWithDiag = (GreenToken)green.WithDiagnostics(info);
        return new SyntaxToken(greenWithDiag, null, CurrentToken.Position);
    }


    /// <summary>
    /// Advances to the next token, clearing cached current token and updating trailing trivia.
    /// </summary>
    private void MoveToNextToken()
    {
        //   _prevTokenTrailingTrivia = _currentToken.TrailingTrivia.Last();    // Store for diagnostic placement
        _currentToken = null;
        _tokenOffset++;
    }

    // ----------------------------------------------------------------------
    // Reset points
    // ----------------------------------------------------------------------

    /// <summary>
    /// Saves the current parser state for later restoration.
    /// </summary>
    internal ResetPoint GetResetPoint()
    {
        int position = _firstToken + _tokenOffset;
        if (_resetCount == 0)
        {
            _resetStart = position;   // low‑water mark
        }
        _resetCount++;
        return new ResetPoint(_resetCount, _mode, position, _prevTokenTrailingTrivia);
    }

    /// <summary>
    /// Restores a previously saved parser state.
    /// </summary>
    internal void Reset(ref ResetPoint point)
    {
        int offset = point.Position - _firstToken;
        Debug.Assert(offset >= 0);

        // If the requested position is beyond our current buffer, fetch tokens up to that point
        if (offset >= _tokenCount)
        {
            PeekToken(offset - _tokenOffset);   // this will fetch more tokens as needed
            offset = point.Position - _firstToken;   // recalc after possible shift
        }

        Debug.Assert(offset >= 0 && offset < _tokenCount);
        _mode = point.Mode;
        _tokenOffset = offset;
        _currentToken = null;
        _prevTokenTrailingTrivia = point.PrevTokenTrailingTrivia;
        _resetCount--;
    }

    /// <summary>
    /// Releases a reset point (must be called in the reverse order of acquisition).
    /// </summary>
    internal void ReleaseResetPoint(ref ResetPoint point)
    {
        Debug.Assert(_resetCount == point.ResetCount);
        _resetCount--;
        if (_resetCount == 0)
        {
            _resetStart = -1;
        }
    }

    // ----------------------------------------------------------------------
    // Mode management
    // ----------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the current lexer mode. Changing the mode invalidates the current token
    /// and may require re‑lexing from the mode change point.
    /// </summary>
    internal LexerMode Mode
    {
        get { return _mode; }
        set
        {
            if (_mode != value)
            {
                _mode = value;
                // Invalidate cached current token; next access will fetch using new mode.
                _currentToken = null;
                // Future tokens will be obtained with the new mode.
            }
        }
    }

    // ----------------------------------------------------------------------
    // Diagnostics and helpers
    // ----------------------------------------------------------------------

    /// <summary>
    /// Creates a diagnostic for an unexpected token error.
    /// </summary>
    private DiagnosticInfo CreateExpectedTokenError(SyntaxKind expected, SyntaxKind actual)
    {
        // Use your existing error code and message generation
        return new DiagnosticInfo(
            ErrorCode.ERR_UnexpectedToken,
            DiagnosticSeverity.Error,
            CurrentToken.Span,   // assume SyntaxToken has a Span property
            actual,
            expected);
    }

    /// <summary>
    /// Attaches a diagnostic to a green node (token or syntax node).
    /// </summary>
    //private TNode WithDiagnostic<TNode>(TNode node, DiagnosticInfo diagnostic) where TNode : SyntaxNode
    //{
    //    var existing = node.GetDiagnostics();
    //    if (existing == null || existing.Length == 0)
    //    {
    //        return (TNode)node.WithDiagnostics(diagnostic);
    //    }
    //    else
    //    {
    //        var newDiags = new DiagnosticInfo[existing.Length + 1];
    //        Array.Copy(existing, newDiags, existing.Length);
    //        newDiags[existing.Length] = diagnostic;
    //        return (TNode)node.WithDiagnostics(newDiags);
    //    }
    //}

    // ----------------------------------------------------------------------
    // Placeholder for the main parse entry point
    // ----------------------------------------------------------------------

    /// <summary>
    /// Parses a compilation unit (the root of a source file). This is a placeholder;
    /// actual parsing of statements, expressions, etc. will be added later.
    /// </summary>
    public CompilationUnitSyntax ParseCompilationUnit()
    {
        var statement = ParseStatement();
        var endOfFileToken = EatToken(SyntaxKind.EndOfFileToken);
        return new CompilationUnitSyntax(statement, endOfFileToken);
    }

    internal StatementSyntax ParseStatement()
    {
        switch (CurrentToken.Kind)
        {
            case SyntaxKind.OpenBraceToken:
                return ParseBlockStatement();
            case SyntaxKind.LetKeyword:
            case SyntaxKind.VarKeyword:
                return ParseVariableDeclaration();
            case SyntaxKind.IfKeyword:
                return ParseIfStatement();
            case SyntaxKind.SwitchKeyword:
            case SyntaxKind.MatchKeyword:
                return ParseSwitchStatement();
            case SyntaxKind.WhileKeyword:
                return ParseWhileStatement();
            case SyntaxKind.DoKeyword:
                return ParseDoWhileStatement();
            case SyntaxKind.ForKeyword:
                return ParseForStatement();
            default:
                if (SyntaxFacts.IsSpecialTypeKeyword(CurrentToken.Kind))
                    return ParseVariableDeclaration();
                return ParseExpressionStatement();
        }
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {
        switch (CurrentToken.Kind)
        {
            case SyntaxKind.OpenParenthesisToken:
                return ParseParenthesizedExpression();

            case SyntaxKind.TrueKeyword:
            case SyntaxKind.FalseKeyword:
                return ParseBooleanLiteral();

            case SyntaxKind.NumericLiteralToken:
            case SyntaxKind.IntegerLiteralToken:
            case SyntaxKind.LongLiteralToken:
            case SyntaxKind.FloatLiteralToken:
            case SyntaxKind.DoubleLiteralToken:
            case SyntaxKind.DecimalLiteralToken:
                return ParseNumberLiteral();

            case SyntaxKind.StringLiteralToken:
                return ParseStringLiteral();

            default:
                return ParseNameExpression();
        }
    }


}
