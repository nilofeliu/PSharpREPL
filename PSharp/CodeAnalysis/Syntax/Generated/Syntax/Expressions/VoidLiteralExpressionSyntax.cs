using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class VoidLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenVoidLiteralExpression _green;

        internal VoidLiteralExpressionSyntax(GreenVoidLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.VoidLiteralExpression;

        public SyntaxToken VoidLiteralToken
            => new SyntaxToken(_green.VoidLiteralToken, this, GetChildPosition(0));

    }
}
