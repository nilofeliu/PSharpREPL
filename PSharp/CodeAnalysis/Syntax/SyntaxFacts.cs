using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Internal;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax;

public static partial class SyntaxFacts
{
    private static readonly List<SyntaxSymbol> Operators = new();
    private static readonly List<SyntaxSymbol> Keywords = new();
    private static readonly List<SyntaxSymbol> FlowKeywords = new();
    private static readonly List<SyntaxSymbol> Punctuation = new();
    private static readonly List<SyntaxSymbol> SpecialTypes = new();
    private static readonly List<SyntaxSymbol> Trivia = new();
    private static readonly List<SyntaxSymbol> Tokens = new();
    private static readonly List<SyntaxSymbol> Expressions = new();  // ← new
    private static readonly List<SyntaxSymbol> Statements = new();  // ← new
    private static readonly List<SyntaxSymbol> Declarations = new();  // ← new

    private static readonly Dictionary<SyntaxKind, (int Binary, int Unary)> _precedenceIndex = new();
    private static readonly Dictionary<string, SyntaxKind> _kindIndex = new();
    private static readonly Dictionary<SyntaxKind, string> _textIndex = new();

    internal static void AddToKindIndex(List<SyntaxSymbol> symbols)
    {
        foreach (var symbol in symbols)
            if (!string.IsNullOrEmpty(symbol.Text) && !_kindIndex.ContainsKey(symbol.Text))
                _kindIndex.Add(symbol.Text, symbol.Kind);
    }

    internal static void AddToTextIndex(List<SyntaxSymbol> symbols)
    {
        foreach (var symbol in symbols)
            if (!_textIndex.ContainsKey(symbol.Kind))
                _textIndex.Add(symbol.Kind, symbol.Text);
    }

    private static void AddToPrecedenceIndex(List<SyntaxSymbol> symbols)
    {
        foreach (var symbol in symbols)
            if (symbol.BinaryPrecedence > 0 || symbol.UnaryPrecedence > 0)
                _precedenceIndex[symbol.Kind] = (symbol.BinaryPrecedence, symbol.UnaryPrecedence);
    }

    internal static void Register(SymbolTable table, List<SyntaxSymbol> symbolList)
    {
        var list = table switch
        {
            SymbolTable.Operators => Operators,
            SymbolTable.Keywords => Keywords,
            SymbolTable.FlowKeywords => FlowKeywords,
            SymbolTable.Punctuation => Punctuation,
            SymbolTable.SpecialTypes => SpecialTypes,
            SymbolTable.Trivia => Trivia,
            SymbolTable.Tokens => Tokens,
            SymbolTable.Expressions => Expressions,  // ← new
            SymbolTable.Statements => Statements,   // ← new
            SymbolTable.Declarations => Declarations, // ← new
            _ => throw new ArgumentException($"Unknown table {table}")
        };
        list.AddRange(symbolList);
        AddToKindIndex(symbolList);
        AddToTextIndex(symbolList);
        AddToPrecedenceIndex(symbolList);
    }

    public static SyntaxSymbol ResolveSymbol(SyntaxKind kind)
    {
        var symbol = Operators.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = FlowKeywords.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = Keywords.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = Punctuation.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = SpecialTypes.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = Trivia.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = Tokens.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = Expressions.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = Statements.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        symbol = Declarations.FirstOrDefault(s => s.Kind == kind);
        if (symbol != null) return symbol;

        return null;
    }

    public static string? GetText(SyntaxKind kind)
    {
        if (_textIndex.TryGetValue(kind, out var text))
            return text;
        return null;
    }

    public static SyntaxKind? GetSyntaxKind(string text)
    {
        if (_kindIndex.TryGetValue(text, out var syntaxType))
            return syntaxType;
        return null;
    }

    internal static (int Binary, int Unary)? GetPrecedence(SyntaxKind kind)
    {
        if (_precedenceIndex.TryGetValue(kind, out var precedence))
            return precedence;
        return null;
    }

    public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
    {
        foreach (var symbol in Operators)
            if (symbol.UnaryPrecedence > 0)
                yield return symbol.Kind;
    }

    public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
    {
        foreach (var symbol in Operators)
            if (symbol.BinaryPrecedence > 0)
                yield return symbol.Kind;
    }

    public static Dictionary<string, SyntaxKind> GetTokenIndex() => _kindIndex;

    public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        => _precedenceIndex.TryGetValue(kind, out var p) ? p.Binary : 0;

    public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        => _precedenceIndex.TryGetValue(kind, out var p) ? p.Unary : 0;

    // ── Is* checks ────────────────────────────────────────────────────────────

    public static bool IsSyntaxKind(SyntaxKind kind) => _textIndex.ContainsKey(kind);
    public static bool IsOperator(SyntaxKind kind) => Operators.Any(s => s.Kind == kind);
    public static bool IsSystemKeyword(SyntaxKind kind) => Keywords.Any(s => s.Kind == kind);
    public static bool IsSystemKeyword(string text) => Keywords.Any(s => s.Text == text);
    public static bool IsControlKeyword(SyntaxKind kind) => FlowKeywords.Any(s => s.Kind == kind);
    public static bool IsControlKeyword(string text) => FlowKeywords.Any(s => s.Text == text);
    public static bool IsPunctuation(SyntaxKind kind) => Punctuation.Any(s => s.Kind == kind);
    public static bool IsSpecialTypeKeyword(SyntaxKind kind) => SpecialTypes.Any(s => s.Kind == kind);
    public static bool IsTrivia(SyntaxKind kind) => Trivia.Any(s => s.Kind == kind);
    public static bool IsLiteralToken(SyntaxKind kind) => Tokens.Any(s => s.Kind == kind);
    public static bool IsExpression(SyntaxKind kind) => Expressions.Any(s => s.Kind == kind);   // ← new
    public static bool IsStatement(SyntaxKind kind) => Statements.Any(s => s.Kind == kind);    // ← new
    public static bool IsDeclaration(SyntaxKind kind) => Declarations.Any(s => s.Kind == kind);  // ← new
    public static bool IsKeyword(SyntaxKind kind) => IsSystemKeyword(kind) || IsControlKeyword(kind);
    public static bool IsKeyword(string text) => IsSystemKeyword(text) || IsControlKeyword(text);





    public static bool IsComparisonOperator(SyntaxKind kind)
        => Operators.Any(s => s.Kind == kind && s.Group == SyntaxGroup.ComparisonOperator);
    public static bool IsAssignmentOperator(SyntaxKind kind)
    => Operators.Any(s => s.Kind == kind && s.Group == SyntaxGroup.AssignmentOperator);

    public static bool IsLogicalOperators(SyntaxKind kind)
        => Operators.Any(s => s.Kind == kind && s.Group == SyntaxGroup.LogicalOperator);


    public static bool IsBinaryExpression(SyntaxKind kind)
    => Expressions.Any(s => s.Kind == kind && s.Group == SyntaxGroup.BinaryExpression);

    public static bool IsComparisonExpression(SyntaxKind kind)
        => Expressions.Any(s => s.Kind == kind && s.Group == SyntaxGroup.ComparisonExpression);

    public static bool IsLogicalExpression(SyntaxKind kind)
        => Expressions.Any(s => s.Kind == kind && s.Group == SyntaxGroup.LogicalExpression);

    public static bool IsUnaryExpression(SyntaxKind kind)
        => Expressions.Any(s => s.Kind == kind && s.Group == SyntaxGroup.UnaryExpression);

    public static bool IsAssignmentExpression(SyntaxKind kind)
        => Expressions.Any(s => s.Kind == kind && s.Group == SyntaxGroup.AssignmentExpression);

    public static bool IsLiteralExpression(SyntaxKind kind)
        => Expressions.Any(s => s.Kind == kind && s.Group == SyntaxGroup.LiteralExpression);


    public static SyntaxKind GetKeywordKind(string text)
        => _kindIndex.TryGetValue(text, out var kind) ? kind : SyntaxKind.IdentifierToken;
}