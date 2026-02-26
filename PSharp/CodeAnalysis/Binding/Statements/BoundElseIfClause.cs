using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Syntax.Kind;

internal sealed class BoundElseIfClause : SyntaxNode
{
    public BoundElseIfClause(BoundExpression condition, BoundStatement statement)
    {
        Condition = condition;
        Statement = statement;
    }
    public override SyntaxKind Kind => SyntaxKind.ElseIfClause;
    public BoundExpression Condition { get; }
    public BoundStatement Statement { get; }

}