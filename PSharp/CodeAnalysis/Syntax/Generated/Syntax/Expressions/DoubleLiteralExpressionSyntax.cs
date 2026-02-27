using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class DoubleLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenDoubleLiteralExpression _green;

        internal DoubleLiteralExpressionSyntax(GreenDoubleLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.DoubleLiteralExpression;

        public SyntaxToken DoubleLiteralToken
            => new SyntaxToken(_green.DoubleLiteralToken, this, GetChildPosition(0));

    }
}
