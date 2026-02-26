using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;
using System.Linq.Expressions;

namespace PSharp.CodeAnalysis.Binding.Expressions
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {
        private VariableSymbol _variable;
        private BoundExpression _expression;

        public BoundAssignmentExpression(VariableSymbol variable, BoundExpression expression)
        {
            _variable = variable;
            _expression = expression;
        }

        public VariableSymbol Variable => _variable;
        public BoundExpression Expression => _expression;
        public override TypeSymbol Type => Expression.Type;
        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    }


}