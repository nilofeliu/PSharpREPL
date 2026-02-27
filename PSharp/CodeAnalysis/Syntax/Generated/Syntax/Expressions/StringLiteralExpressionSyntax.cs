using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class StringLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenStringLiteralExpression _green;

        internal StringLiteralExpressionSyntax(GreenStringLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.StringLiteralExpression;

        public SyntaxToken StringLiteralToken
            => new SyntaxToken(_green.StringLiteralToken, this, GetChildPosition(0));

    }
}
