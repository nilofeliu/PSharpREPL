using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green;

internal abstract class GreenExpression : GreenNode
{
    protected GreenExpression(SyntaxKind kind) : base(kind)
    {
    }
}
