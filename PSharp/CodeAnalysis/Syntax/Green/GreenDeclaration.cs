using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green;

internal abstract class GreenDeclaration : GreenNode
{
    protected GreenDeclaration(SyntaxKind kind) : base(kind)
    {
    }
}