using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class SByteLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenSByteLiteralExpression _green;

        internal SByteLiteralExpressionSyntax(GreenSByteLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.SByteLiteralExpression;

        public SyntaxToken SByteLiteralToken
            => new SyntaxToken(_green.SByteLiteralToken, this, GetChildPosition(0));

    }
}
