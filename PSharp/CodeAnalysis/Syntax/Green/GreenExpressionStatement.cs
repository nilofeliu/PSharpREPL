using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green;

public sealed class GreenExpressionStatement : GreenStatement
{
    public GreenExpressionStatement(SyntaxKind kind, GreenExpression expression) : base(kind)
    {
        Expression = expression;
    }
    public override int SlotCount => 1;
    public override GreenNode? GetSlot(int index) => index switch
    {
        0 => Expression,
        _ => null
    };
    public GreenExpression Expression { get; }

    public override string ToFullString()
    {
        throw new NotImplementedException();
    }

    protected override GreenNode CreateWithDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        throw new NotImplementedException();
    }
}