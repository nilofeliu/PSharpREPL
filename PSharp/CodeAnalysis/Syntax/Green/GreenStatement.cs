using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green;

internal abstract class GreenStatement : GreenNode
{
    protected GreenStatement(SyntaxKind kind) : base(kind)
    {
    }
}
