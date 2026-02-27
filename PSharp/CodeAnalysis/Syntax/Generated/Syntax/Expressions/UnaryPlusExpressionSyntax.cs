using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class UnaryPlusExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenUnaryPlusExpression _green;

        private ExpressionSyntax _operand;

        internal UnaryPlusExpressionSyntax(GreenUnaryPlusExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.UnaryPlusExpression;

        public SyntaxToken OperatorToken
            => new SyntaxToken(_green.OperatorToken, this, GetChildPosition(0));

        public ExpressionSyntax Operand
        {
            get
            {
                if (_operand == null)
                    _operand = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Operand, this, GetChildPosition(1));
                return _operand;
            }
        }

    }
}
