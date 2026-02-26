using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Expressions
{
    internal sealed class BoundVariableExpression : BoundExpression
    {
        private VariableSymbol _variable;

        public BoundVariableExpression(VariableSymbol variable)
        {
            _variable = variable;
        }

        public VariableSymbol Variable => _variable;
        public override TypeSymbol Type => _variable.Type;
        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;
    }
}