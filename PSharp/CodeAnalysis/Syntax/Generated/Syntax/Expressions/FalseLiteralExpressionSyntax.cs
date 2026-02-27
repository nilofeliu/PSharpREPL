using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class FalseLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenFalseLiteralExpression _green;

        internal FalseLiteralExpressionSyntax(GreenFalseLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.FalseLiteralExpression;

        public SyntaxToken FalseLiteralToken
            => new SyntaxToken(_green.FalseLiteralToken, this, GetChildPosition(0));

    }
}
