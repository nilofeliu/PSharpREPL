using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class AndAssignmentExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenAndAssignmentExpression _green;

        private ExpressionSyntax _expression;

        internal AndAssignmentExpressionSyntax(GreenAndAssignmentExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.AndAssignmentExpression;

        public SyntaxToken IdentifierToken
            => new SyntaxToken(_green.IdentifierToken, this, GetChildPosition(0));

        public SyntaxToken AmpersandEqualsToken
            => new SyntaxToken(_green.AmpersandEqualsToken, this, GetChildPosition(1));

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
