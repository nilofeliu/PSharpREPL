using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Kind;

namespace PSharp.CodeAnalysis.Binding.Statements
{
    internal sealed class BoundConditionalGotoStatement : BoundStatement
    {
        public BoundConditionalGotoStatement(BoundLabel label, BoundExpression condition, bool jumpIfTrue = true)
        {
            Label = label;
            Condition = condition;
            JumpIfTrue = jumpIfTrue;
        }

        public override BoundNodeKind Kind => BoundNodeKind.ConditionalGotoStatement;

        public BoundLabel Label { get; }
        public BoundExpression Condition { get; }
        public bool JumpIfTrue { get; }
    }
}

