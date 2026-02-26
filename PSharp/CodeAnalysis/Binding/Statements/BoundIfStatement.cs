using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Kind;

namespace PSharp.CodeAnalysis.Binding.Statements;

internal sealed class BoundIfStatement : BoundStatement
{
    public BoundIfStatement(
        BoundExpression condition,
        BoundStatement thenStatement,
        BoundStatement elseStatement)
    {
        Condition = condition;
        ThenStatement = thenStatement;
        ElseStatement = elseStatement;
    }

    public override BoundNodeKind Kind => BoundNodeKind.IfStatement;
    public BoundExpression Condition { get; }
    public BoundStatement ThenStatement { get; }
    public BoundStatement ElseStatement { get; }
}
