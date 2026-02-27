using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;

namespace PSharp.CodeAnalysis.Syntax.Green;

public abstract class GreenStatement : GreenNode
{
    protected GreenStatement(SyntaxKind kind) : base(kind)
    {
    }
}
