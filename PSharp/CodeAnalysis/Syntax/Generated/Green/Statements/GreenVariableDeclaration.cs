using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;

internal sealed class GreenVariableDeclaration : GreenStatement
{
    public GreenToken Keyword { get; }
    public GreenToken Identifier { get; }
    public GreenToken EqualsToken { get; }
    public GreenExpression Initializer { get; }

    public override int SlotCount => 4;
    public override GreenNode? GetSlot(int index) => index switch
    {
        0 => Keyword,
        1 => Identifier,
        2 => EqualsToken,
        3 => Initializer,
        _ => null
    };

    public GreenVariableDeclaration(
        SyntaxKind kind,
        GreenToken keyword,
        GreenToken identifier,
        GreenToken equalsToken,
        GreenExpression initializer
    ) : base(kind)
    {
        Keyword = keyword;
        Identifier = identifier;
        EqualsToken = equalsToken;
        Initializer = initializer;
    }

    public override SyntaxKind Kind => SyntaxKind.LocalDeclarationStatement;

    protected override GreenNode CreateWithDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        var node = new GreenVariableDeclaration(Kind, Keyword, Identifier, EqualsToken, Initializer);
        node.Diagnostics = diagnostics;
        return node;
    }

    public override string ToFullString()
    {
        var sb = new System.Text.StringBuilder();
        foreach (var child in GetChildren())
            sb.Append(child.ToFullString());
        return sb.ToString();
    }
}