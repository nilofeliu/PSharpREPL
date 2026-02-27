using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class PostIncrementExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenPostIncrementExpression _green;

        private ExpressionSyntax _operand;

        internal PostIncrementExpressionSyntax(GreenPostIncrementExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.PostIncrementExpression;

        public ExpressionSyntax Operand
        {
            get
            {
                if (_operand == null)
                    _operand = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Operand, this, GetChildPosition(0));
                return _operand;
            }
        }

        public SyntaxToken PlusPlusToken
            => new SyntaxToken(_green.PlusPlusToken, this, GetChildPosition(1));

    }
}
