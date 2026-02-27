using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class ShortLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenShortLiteralExpression _green;

        internal ShortLiteralExpressionSyntax(GreenShortLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.ShortLiteralExpression;

        public SyntaxToken ShortLiteralToken
            => new SyntaxToken(_green.ShortLiteralToken, this, GetChildPosition(0));

    }
}
