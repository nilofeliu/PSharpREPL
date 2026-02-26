using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Expressions
{
    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public override TypeSymbol Type => Op.Type;
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
        public BoundUnaryOperator Op { get; }
        public BoundExpression Operand { get; }

        public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
        {
            Op = op;
            Operand = operand;
        }
    }
}

