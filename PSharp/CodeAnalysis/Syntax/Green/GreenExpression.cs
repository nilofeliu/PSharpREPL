using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green;

public abstract class GreenExpression : GreenNode
{
    protected GreenExpression(SyntaxKind kind) : base(kind)
    {
    }
}
