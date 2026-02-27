using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class EqualsValueClauseSyntax : StatementSyntax
    {
        private readonly GreenEqualsValueClause _green;

        private ExpressionSyntax _value;

        internal EqualsValueClauseSyntax(GreenEqualsValueClause green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.EqualsValueClause;

        public SyntaxToken EqualsToken
            => new SyntaxToken(_green.EqualsToken, this, GetChildPosition(0));

        public ExpressionSyntax Value
        {
            get
            {
                if (_value == null)
                    _value = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Value, this, GetChildPosition(1));
                return _value;
            }
        }

    }
}
