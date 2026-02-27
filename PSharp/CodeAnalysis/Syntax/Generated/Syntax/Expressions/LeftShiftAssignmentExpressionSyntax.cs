using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class LeftShiftAssignmentExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenLeftShiftAssignmentExpression _green;

        private ExpressionSyntax _expression;

        internal LeftShiftAssignmentExpressionSyntax(GreenLeftShiftAssignmentExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.LeftShiftAssignmentExpression;

        public SyntaxToken IdentifierToken
            => new SyntaxToken(_green.IdentifierToken, this, GetChildPosition(0));

        public SyntaxToken LessThanLessThanEqualsToken
            => new SyntaxToken(_green.LessThanLessThanEqualsToken, this, GetChildPosition(1));

        public ExpressionSyntax Expression
        {
            get
            {
                if (_expression == null)
                    _expression = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Expression, this, GetChildPosition(2));
                return _expression;
            }
        }

    }
}
