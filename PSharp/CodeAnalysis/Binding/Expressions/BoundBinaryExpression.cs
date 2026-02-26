using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Expressions
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public override TypeSymbol Type => Op.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
        public BoundExpression Left { get; }
        public BoundBinaryOperator Op { get; }
        public BoundExpression Right { get; }

        public BoundBinaryExpression(BoundExpression left,
            BoundBinaryOperator op,
            BoundExpression right)
        {
            Left = left;
            Op = op;
            Right = right;
        }
    }
}

