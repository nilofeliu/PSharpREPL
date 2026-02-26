using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Kind;

namespace PSharp.CodeAnalysis.Binding.Statements
{
    internal class BoundDoWhileStatement : BoundStatement
    {
        public BoundDoWhileStatement(BoundExpression condition, BoundStatement body)
        {
            Condition = condition;
            Body = body;
        }

        public override BoundNodeKind Kind => BoundNodeKind.DoWhileStatement;
        public BoundExpression Condition { get; }
        public BoundStatement Body { get; }
    }
}