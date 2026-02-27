using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;

public sealed class VariableDeclarationSyntax : StatementSyntax
{
    private readonly GreenVariableDeclaration _green;

    internal VariableDeclarationSyntax(GreenVariableDeclaration green, SyntaxNode? parent, int position)
        : base(parent, green, position)
    {
        _green = green;
    }

    public override SyntaxKind Kind => SyntaxKind.LocalDeclarationStatement;

    public SyntaxToken Keyword
        => new SyntaxToken(_green.Keyword, this, GetChildPosition(0));

    public SyntaxToken Identifier
        => new SyntaxToken(_green.Identifier, this, GetChildPosition(1));

    public SyntaxToken EqualsToken
        => new SyntaxToken(_green.EqualsToken, this, GetChildPosition(2));

    private ExpressionSyntax? _initializer;
    public ExpressionSyntax Initializer
        => _initializer ??= (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Initializer, this, GetChildPosition(3));
}