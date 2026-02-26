using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Statements;

internal sealed class BoundForStatement : BoundStatement
{
    public BoundForStatement(VariableSymbol variable, BoundExpression lowerBound, BoundExpression upperBound, BoundStatement body)
    {
        Variable = variable;
        LowerBound = lowerBound;
        UpperBound = upperBound;
        Body = body;
    }

    public override BoundNodeKind Kind => BoundNodeKind.ForStatement;

    public VariableSymbol Variable { get; }
    public BoundExpression LowerBound { get; }
    public BoundExpression UpperBound { get; }
    public BoundStatement Body { get; }
}
