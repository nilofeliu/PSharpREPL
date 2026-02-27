using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class TrueLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenTrueLiteralExpression _green;

        internal TrueLiteralExpressionSyntax(GreenTrueLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.TrueLiteralExpression;

        public SyntaxToken LiteralToken
            => new SyntaxToken(_green.LiteralToken, this, GetChildPosition(0));

    }
}
