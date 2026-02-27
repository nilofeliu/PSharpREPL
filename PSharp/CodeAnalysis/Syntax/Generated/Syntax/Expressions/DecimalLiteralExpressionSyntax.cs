using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class DecimalLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenDecimalLiteralExpression _green;

        internal DecimalLiteralExpressionSyntax(GreenDecimalLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.DecimalLiteralExpression;

        public SyntaxToken DecimalLiteralToken
            => new SyntaxToken(_green.DecimalLiteralToken, this, GetChildPosition(0));

    }
}
