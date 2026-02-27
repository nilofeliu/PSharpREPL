using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class EqualsExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenEqualsExpression _green;

        private ExpressionSyntax _left;
        private ExpressionSyntax _right;

        internal EqualsExpressionSyntax(GreenEqualsExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.EqualsExpression;

        public ExpressionSyntax Left
        {
            get
            {
                if (_left == null)
                    _left = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Left, this, GetChildPosition(0));
                return _left;
            }
        }

        public SyntaxToken EqualsEqualsToken
            => new SyntaxToken(_green.EqualsEqualsToken, this, GetChildPosition(1));

        public ExpressionSyntax Right
        {
            get
            {
                if (_right == null)
                    _right = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Right, this, GetChildPosition(2));
                return _right;
            }
        }

    }
}
