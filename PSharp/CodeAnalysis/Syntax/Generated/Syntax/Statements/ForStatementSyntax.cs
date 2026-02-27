using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class ForStatementSyntax : StatementSyntax
    {
        private readonly GreenForStatement _green;

        private ExpressionSyntax _lowerBound;
        private ExpressionSyntax _upperBound;
        private StatementSyntax _body;

        internal ForStatementSyntax(GreenForStatement green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.ForStatement;

        public SyntaxToken Keyword
            => new SyntaxToken(_green.Keyword, this, GetChildPosition(0));

        public SyntaxToken Identifier
            => new SyntaxToken(_green.Identifier, this, GetChildPosition(1));

        public SyntaxToken EqualsToken
            => new SyntaxToken(_green.EqualsToken, this, GetChildPosition(2));

        public ExpressionSyntax LowerBound
        {
            get
            {
                if (_lowerBound == null)
                    _lowerBound = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.LowerBound, this, GetChildPosition(3));
                return _lowerBound;
            }
        }

        public SyntaxToken ToKeyword
            => new SyntaxToken(_green.ToKeyword, this, GetChildPosition(4));

        public ExpressionSyntax UpperBound
        {
            get
            {
                if (_upperBound == null)
                    _upperBound = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.UpperBound, this, GetChildPosition(5));
                return _upperBound;
            }
        }

        public SyntaxToken ColonToken
            => new SyntaxToken(_green.ColonToken, this, GetChildPosition(6));

        public StatementSyntax Body
        {
            get
            {
                if (_body == null)
                    _body = (StatementSyntax)RedNodeFactory.CreateRed(_green.Body, this, GetChildPosition(7));
                return _body;
            }
        }

        public SyntaxToken EndToken
            => new SyntaxToken(_green.EndToken, this, GetChildPosition(8));

    }
}
