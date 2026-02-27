using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Nodes
{
    public sealed class ExpressionStatementSyntax : StatementSyntax
    {
        private readonly GreenExpressionStatement _green;
        private ExpressionSyntax? _expression;

        internal ExpressionStatementSyntax(GreenExpressionStatement green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.ExpressionStatement;

        public ExpressionSyntax Expression
            => _expression ??= (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Expression, this, GetChildPosition(0));
    }
}