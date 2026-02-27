using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class FloatLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenFloatLiteralExpression _green;

        internal FloatLiteralExpressionSyntax(GreenFloatLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.FloatLiteralExpression;

        public SyntaxToken FloatLiteralToken
            => new SyntaxToken(_green.FloatLiteralToken, this, GetChildPosition(0));

    }
}
