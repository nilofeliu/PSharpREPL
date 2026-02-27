using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class LongLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenLongLiteralExpression _green;

        internal LongLiteralExpressionSyntax(GreenLongLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.LongLiteralExpression;

        public SyntaxToken LongLiteralToken
            => new SyntaxToken(_green.LongLiteralToken, this, GetChildPosition(0));

    }
}
