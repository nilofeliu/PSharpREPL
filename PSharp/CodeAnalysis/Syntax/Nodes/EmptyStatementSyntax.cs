using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Green;

namespace PSharp.CodeAnalysis.Syntax.Nodes;

public sealed class EmptyStatementSyntax : StatementSyntax
{
    private readonly GreenEmptyStatement _green;

    internal EmptyStatementSyntax(GreenEmptyStatement green, SyntaxNode? parent, int position)
        : base(parent, green, position)
    {
        _green = green;
    }

    public SyntaxToken Token
        => new SyntaxToken(_green.Token, this, GetChildPosition(0));

    public override SyntaxKind Kind => SyntaxKind.EmptyStatement;
}