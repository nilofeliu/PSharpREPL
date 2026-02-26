using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Kind;

namespace PSharp.CodeAnalysis.Binding.Statements;

internal sealed class BoundExpressionStatement : BoundStatement
{
    public BoundExpressionStatement(BoundExpression expression)
    {
        Expression = expression;
    }

    public override BoundNodeKind Kind => BoundNodeKind.ExpressionStatement;
    public BoundExpression Expression { get; }

}