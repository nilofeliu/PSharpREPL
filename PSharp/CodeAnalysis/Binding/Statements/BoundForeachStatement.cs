using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Statements;

internal sealed class BoundForeachStatement : BoundStatement
{
    public BoundForeachStatement(VariableSymbol variable, BoundExpression collection, BoundStatement body)
    {
        Variable = variable;
        Collection = collection;
        Body = body;
    }

    public override BoundNodeKind Kind => BoundNodeKind.ForeachStatement;
    public VariableSymbol Variable { get; }
    public BoundExpression Collection { get; }
    public BoundStatement Body { get; }
}