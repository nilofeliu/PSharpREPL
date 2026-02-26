using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;
using System.Text;

namespace PSharp.CodeAnalysis.Syntax.Green;

public sealed class GreenToken : GreenNode
{
    public string TokenText { get; }
    public object Value { get; }
    public int Position { get; }
    public int Index { get; }
    public bool IsMissing { get; }

    private readonly int _leadingWidth;

    private readonly int _trailingWidth;

    public override int LeadingWidth => _leadingWidth;
    public override int TrailingWidth => _trailingWidth;

    public GreenNode LeadingTrivia { get; }
    public GreenNode TrailingTrivia { get; }

    public GreenToken(SyntaxKind kind, string text, object value = null,
                      SyntaxTrivia[]? leadingTrivia = null, SyntaxTrivia[]? trailingTrivia = null,
                      bool isMissing = false,
                      DiagnosticInfo[]? diagnostics = null,
                      int position = 0,
                      int index = -1)
        : base(kind)
    {
        TokenText = text;
        Value = value;
        IsMissing = isMissing
            || kind == SyntaxKind.EndOfFileToken && string.IsNullOrEmpty(TokenText)
            || kind == SyntaxKind.BadToken && string.IsNullOrEmpty(TokenText);

        LeadingTrivia = new GreenTriviaList(leadingTrivia ?? Array.Empty<SyntaxTrivia>());
        TrailingTrivia = new GreenTriviaList(trailingTrivia ?? Array.Empty<SyntaxTrivia>());

        Position = position;
        Index = index;

        _leadingWidth = LeadingTrivia.FullWidth;
        _trailingWidth = TrailingTrivia.FullWidth;
        FullWidth = _leadingWidth + TokenText.Length + _trailingWidth;
        Diagnostics = diagnostics;
    }

    public override string Text => TokenText;
    public GreenToken WithLeadingTrivia(SyntaxTrivia[] newLeading)
    {
        var trailingArray = (TrailingTrivia as GreenTriviaList)?.TriviaArray ?? Array.Empty<SyntaxTrivia>();
        return new GreenToken(Kind, TokenText, Value, newLeading, trailingArray,
                              IsMissing, Diagnostics, Position, Index);
    }

    public GreenToken WithTrailingTrivia(SyntaxTrivia[] newTrailing)
    {
        var leadingArray = (LeadingTrivia as GreenTriviaList)?.TriviaArray ?? Array.Empty<SyntaxTrivia>();
        return new GreenToken(Kind, TokenText, Value, leadingArray, newTrailing,
                              IsMissing, Diagnostics, Position, Index);
    }

    protected override GreenNode CreateWithDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        // Retrieve the original arrays from the trivia lists
        var leadingArray = (LeadingTrivia as GreenTriviaList)?.TriviaArray ?? Array.Empty<SyntaxTrivia>();
        var trailingArray = (TrailingTrivia as GreenTriviaList)?.TriviaArray ?? Array.Empty<SyntaxTrivia>();

        return new GreenToken(Kind, TokenText, Value, leadingArray, trailingArray,
                              IsMissing, diagnostics, Position, Index);
    }
    public override bool IsEquivalentTo(GreenNode other)
    => other is GreenToken token &&
       Kind == token.Kind &&
       TokenText == token.TokenText &&
       Value?.Equals(token.Value) != false;
    public override string ToFullString()
    {
        var sb = new StringBuilder();
        sb.Append(LeadingTrivia.ToFullString());
        sb.Append(TokenText);
        sb.Append(TrailingTrivia.ToFullString());
        return sb.ToString();
    }
    // Optional helpers to get the arrays if needed elsewhere
    internal SyntaxTrivia[] GetLeadingTriviaArray()
        => (LeadingTrivia as GreenTriviaList)?.TriviaArray ?? Array.Empty<SyntaxTrivia>();

    internal SyntaxTrivia[] GetTrailingTriviaArray()
        => (TrailingTrivia as GreenTriviaList)?.TriviaArray ?? Array.Empty<SyntaxTrivia>();

}

