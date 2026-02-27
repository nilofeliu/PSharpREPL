using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Expressions
{
    public sealed class CharacterLiteralExpressionSyntax : ExpressionSyntax
    {
        private readonly GreenCharacterLiteralExpression _green;

        internal CharacterLiteralExpressionSyntax(GreenCharacterLiteralExpression green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.CharacterLiteralExpression;

        public SyntaxToken CharacterLiteralToken
            => new SyntaxToken(_green.CharacterLiteralToken, this, GetChildPosition(0));

    }
}
