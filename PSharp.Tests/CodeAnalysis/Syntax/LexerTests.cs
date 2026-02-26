using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Syntax;
using Minsk.CodeAnalysis.Syntax.Kind;
using Minsk.CodeAnalysis.Text;

namespace PSharp.Tests.CodeAnalysis.Syntax;

public class LexerTests
{
    [Fact]
    public void Lexer_Leves_UnterminatedString()
    {
        var text = "\"text";
        var errorText = "Unterminated string literal.";
        var tokens = SyntaxTree.ParseTokens(text, out var diagnostics);

        var token = Assert.Single(tokens);
        Assert.Equal(SyntaxKind.StringLiteralToken, token.Kind);
        Assert.Equal(text, token.Text);

        var diagnostic = Assert.Single(diagnostics);
        Assert.Equal(new TextSpan(0, 5), diagnostic.Span);
        Assert.Equal(errorText, diagnostic.Message);
    }

    [Fact]
    public void Lexer_Covers_AllTokens()
    {
        var tokenKinds = Enum.GetValues(typeof(SyntaxKind))
            .Cast<SyntaxKind>()
            .Where(k => k.ToString().EndsWith("Keyword") ||
                        k.ToString().EndsWith("Token"))
            .ToList();

        var testedTokenKinds = GetTokens().Concat(GetSeparators())
            .Select(t => t.kind)
            .Distinct()
            .ToList();

        var untestedTokenKinds = new SortedSet<SyntaxKind>(tokenKinds);
        untestedTokenKinds.ExceptWith(testedTokenKinds);
        untestedTokenKinds.Remove(SyntaxKind.BadToken);
        untestedTokenKinds.Remove(SyntaxKind.EndOfFileToken);
        untestedTokenKinds.Remove(SyntaxKind.WhiteSpaceTrivia); // now trivia, not a token

        Assert.Empty(untestedTokenKinds);
    }

    [Theory]
    [MemberData(nameof(GetTokensData))]
    public void Lexer_Lexes_Token(SyntaxKind kind, string text)
    {
        if (SyntaxFacts.IsTrivia(kind))
            return;

        if (string.IsNullOrEmpty(text))
            return;

        var tokens = SyntaxTree.ParseTokens(text);

        var token = Assert.Single(tokens);
        Assert.Equal(kind, token.Kind);
        Assert.Equal(text, token.Text);
    }

    [Theory]
    [MemberData(nameof(GetTokensPairsData))]
    public void Lexer_Lexes_TokenPairs(SyntaxKind t1Kind, string t1Text,
                                       SyntaxKind t2Kind, string t2Text)
    {
        if (string.IsNullOrEmpty(t1Text) || string.IsNullOrEmpty(t2Text))
            return;

        var text = t1Text + t2Text;
        var tokens = SyntaxTree.ParseTokens(text).ToArray();

        Assert.Equal(2, tokens.Length);
        Assert.Equal(t1Kind, tokens[0].Kind);
        Assert.Equal(t1Text, tokens[0].Text);
        Assert.Equal(t2Kind, tokens[1].Kind);
        Assert.Equal(t2Text, tokens[1].Text);
    }

    //[Theory]
    //[MemberData(nameof(GetTokensPairsWithSeparatorData))]
    //public void Lexer_Lexes_TokenPairs_WithSeparator(SyntaxKind t1Kind, string t1Text,
    //    SyntaxKind separatorKind, string separatorText,
    //    SyntaxKind t2Kind, string t2Text)
    //{
    //    if (string.IsNullOrEmpty(t1Text) || string.IsNullOrEmpty(t2Text))
    //        return;

    //    var text = t1Text + separatorText + t2Text;
    //    var tokens = SyntaxTree.ParseTokens(text).ToArray();

    //    // Whitespace is now trivia attached to tokens — only 2 tokens expected
    //    Assert.Equal(2, tokens.Length);
    //    Assert.Equal(t1Kind, tokens[0].Kind);
    //    Assert.Equal(t1Text, tokens[0].Text);
    //    Assert.Equal(t2Kind, tokens[1].Kind);
    //    Assert.Equal(t2Text, tokens[1].Text);

    //    // Verify separator is attached as trailing trivia on t1 or leading trivia on t2
    //    var trailingTrivia = tokens[0].TrailingTrivia;
    //    var leadingTrivia = tokens[1].LeadingTrivia;
    //    var triviaText = string.Concat(trailingTrivia.Select(t => t.Text))
    //                       + string.Concat(leadingTrivia.Select(t => t.Text));
    //    Assert.Contains(separatorText, triviaText);
    //}

    public static IEnumerable<object?[]> GetTokensData()
    {
        foreach (var t in GetTokens().Concat(GetSeparators()))
            yield return new object?[] { t.kind, t.text };
    }

    public static IEnumerable<object?[]> GetTokensPairsData()
    {
        foreach (var t in GetTokensPairs())
            yield return new object?[] { t.t1kind, t.t1text, t.t2kind, t.t2text };
    }

    public static IEnumerable<object?[]> GetTokensPairsWithSeparatorData()
    {
        foreach (var t in GetTokensPairsWithSeparator())
            yield return new object?[] { t.t1kind, t.t1text, t.separatorKind, t.separatorText, t.t2kind, t.t2text };
    }

    private static IEnumerable<(SyntaxKind kind, string text)> GetTokens()
    {
        var fixedTokens = Enum.GetValues(typeof(SyntaxKind))
            .Cast<SyntaxKind>()
            .Select(k => (kind: k, text: SyntaxFacts.GetText(k)))
            .Where(t => t.text != null)
            .Where(t => !t.kind.ToString().EndsWith("Trivia")); // exclude trivia

        var dynamicTokens = new[]
        {
            (SyntaxKind.IdentifierToken, "a"),
            (SyntaxKind.IdentifierToken, "abc"),
            (SyntaxKind.IntegerLiteralToken, "1"),
            (SyntaxKind.IntegerLiteralToken, "123"),
            (SyntaxKind.DoubleLiteralToken, "12.3"),
            (SyntaxKind.StringLiteralToken, "\"Hello World!\""),
        };

        return fixedTokens.Concat(dynamicTokens);
    }

    private static IEnumerable<(SyntaxKind kind, string text)> GetSeparators()
    {
        return new[]
        {
            (SyntaxKind.WhiteSpaceTrivia, " "),
            (SyntaxKind.WhiteSpaceTrivia, "  "),
            (SyntaxKind.NewLineTrivia, "\r"),
            (SyntaxKind.NewLineTrivia, "\n"),
            (SyntaxKind.NewLineTrivia, "\r\n"),
        };
    }

    private static IEnumerable<(SyntaxKind t1kind, string t1text, SyntaxKind t2kind, string t2text)> GetTokensPairs()
    {
        foreach (var t1 in GetTokens())
            foreach (var t2 in GetTokens())
                if (!RequiresSeparator(t1.kind, t2.kind))
                    yield return (t1.kind, t1.text, t2.kind, t2.text);
    }

    private static IEnumerable<(SyntaxKind t1kind, string t1text,
        SyntaxKind separatorKind, string separatorText,
        SyntaxKind t2kind, string t2text)> GetTokensPairsWithSeparator()
    {
        foreach (var t1 in GetTokens())
            foreach (var t2 in GetTokens())
                foreach (var s in GetSeparators())
                    yield return (t1.kind, t1.text, s.kind, s.text, t2.kind, t2.text);
    }

    private static bool RequiresSeparator(SyntaxKind t1, SyntaxKind t2)
    {
        var t1keyword = t1.ToString().EndsWith("Keyword");
        var t2keyword = t2.ToString().EndsWith("Keyword");

        if (t1keyword && t2keyword) return true;
        if (t1keyword && t2 == SyntaxKind.IdentifierToken) return true;
        if (t1keyword && t2 == SyntaxKind.IntegerLiteralToken) return true;
        if (t1keyword && t2 == SyntaxKind.DoubleLiteralToken) return true;
        if (t1 == SyntaxKind.IntegerLiteralToken && t2keyword) return true;
        if (t1 == SyntaxKind.DoubleLiteralToken && t2keyword) return true;
        if (t1 == SyntaxKind.IdentifierToken && t2keyword) return true;
        if (t1 == SyntaxKind.SlashToken && t2 == SyntaxKind.SlashToken) return true;
        if (t1 == SyntaxKind.SlashToken && t2 == SyntaxKind.StarToken) return true;
        if (t1 == SyntaxKind.IdentifierToken && t2 == SyntaxKind.IdentifierToken) return true;
        if (t1 == SyntaxKind.IdentifierToken && t2 == SyntaxKind.IdentifierToken) return true;
        if (t1 == SyntaxKind.IdentifierToken && t2 == SyntaxKind.DoubleLiteralToken) return true;
        if (t1 == SyntaxKind.IdentifierToken && t2 == SyntaxKind.IntegerLiteralToken) return true;
        if (t1 == SyntaxKind.DoubleLiteralToken && t2 == SyntaxKind.DoubleLiteralToken) return true;
        if (t1 == SyntaxKind.IntegerLiteralToken && t2 == SyntaxKind.IntegerLiteralToken) return true;
        if (t1 == SyntaxKind.IntegerLiteralToken && t2 == SyntaxKind.StringLiteralToken) return true;
        if (t1 == SyntaxKind.DoubleLiteralToken && t2 == SyntaxKind.IntegerLiteralToken) return true;
        if (t1 == SyntaxKind.IntegerLiteralToken && t2 == SyntaxKind.DoubleLiteralToken) return true;
        if (t1 == SyntaxKind.StringLiteralToken && t2 == SyntaxKind.IntegerLiteralToken) return true;
        if (t1 == SyntaxKind.UIntLiteralToken && t2 == SyntaxKind.StringLiteralToken) return true;
        if (t1 == SyntaxKind.StringLiteralToken && t2 == SyntaxKind.UIntLiteralToken) return true;
        if (t1 == SyntaxKind.DoubleLiteralToken && t2 == SyntaxKind.StringLiteralToken) return true;
        if (t1 == SyntaxKind.StringLiteralToken && t2 == SyntaxKind.DoubleLiteralToken) return true;
        if (t1 == SyntaxKind.StringLiteralToken && t2 == SyntaxKind.StringLiteralToken) return true;
        if (t1 == SyntaxKind.BangToken && t2 == SyntaxKind.EqualsToken) return true;
        if (t1 == SyntaxKind.BangToken && t2 == SyntaxKind.EqualsEqualsToken) return true;
        if (t1 == SyntaxKind.EqualsToken && t2 == SyntaxKind.EqualsToken) return true;
        if (t1 == SyntaxKind.EqualsToken && t2 == SyntaxKind.EqualsEqualsToken) return true;
        if (t1 == SyntaxKind.LessToken && t2 == SyntaxKind.EqualsToken) return true;
        if (t1 == SyntaxKind.LessToken && t2 == SyntaxKind.EqualsEqualsToken) return true;
        if (t1 == SyntaxKind.GreaterToken && t2 == SyntaxKind.EqualsToken) return true;
        if (t1 == SyntaxKind.GreaterToken && t2 == SyntaxKind.EqualsEqualsToken) return true;
        if (t1 == SyntaxKind.AmpersandToken && t2 == SyntaxKind.AmpersandToken) return true;
        if (t1 == SyntaxKind.AmpersandToken && t2 == SyntaxKind.AmpersandAmpersandToken) return true;
        if (t1 == SyntaxKind.AmpersandAmpersandToken && t2 == SyntaxKind.AmpersandAmpersandToken) return true;
        if (t1 == SyntaxKind.PipeToken && t2 == SyntaxKind.PipeToken) return true;
        if (t1 == SyntaxKind.PipeToken && t2 == SyntaxKind.PipePipeToken) return true;
        if (t1 == SyntaxKind.PipePipeToken && t2 == SyntaxKind.PipePipeToken) return true;

        return false;
    }
}
