using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class IntLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenIntLiteralExpression _green;

        internal IntLiteralExpressionSyntax(GreenIntLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.IntLiteralExpression;

        public SyntaxToken IntLiteralToken
            => new SyntaxToken(_green.IntLiteralToken, this, GetChildPosition(0));

    }
}
