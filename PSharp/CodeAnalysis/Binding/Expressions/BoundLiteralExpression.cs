using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Expressions
{
    internal sealed class BoundLiteralExpression : BoundExpression
    {
        public BoundLiteralExpression(object value)
        {
            Value = value;
        }

        public object Value { get; }
        public override TypeSymbol Type => Value.GetTypeSymbol();
        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
    }
}

