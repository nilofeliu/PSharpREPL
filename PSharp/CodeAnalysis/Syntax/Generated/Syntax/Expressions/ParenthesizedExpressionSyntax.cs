using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenParenthesizedExpression _green;

        private ExpressionSyntax _expression;

        internal ParenthesizedExpressionSyntax(GreenParenthesizedExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        public SyntaxToken OpenParenthesisToken
            => new SyntaxToken(_green.OpenParenthesisToken, this, GetChildPosition(0));

        public ExpressionSyntax Expression
        {
            get
            {
                if (_expression == null)
                    _expression = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Expression, this, GetChildPosition(1));
                return _expression;
            }
        }

        public SyntaxToken CloseParenthesisToken
            => new SyntaxToken(_green.CloseParenthesisToken, this, GetChildPosition(2));

    }
}
