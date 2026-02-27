using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class DoWhileStatementSyntax : StatementSyntax
    {
        private readonly GreenDoWhileStatement _green;

        private BlockStatementSyntax _body;
        private ExpressionSyntax _condition;

        internal DoWhileStatementSyntax(GreenDoWhileStatement green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.DoStatement;

        public SyntaxToken DoKeyword
            => new SyntaxToken(_green.DoKeyword, this, GetChildPosition(0));

        public SyntaxToken ColonToken
            => new SyntaxToken(_green.ColonToken, this, GetChildPosition(1));

        public BlockStatementSyntax Body
        {
            get
            {
                if (_body == null)
                    _body = (BlockStatementSyntax)RedNodeFactory.CreateRed(_green.Body, this, GetChildPosition(2));
                return _body;
            }
        }

        public ExpressionSyntax Condition
        {
            get
            {
                if (_condition == null)
                    _condition = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Condition, this, GetChildPosition(3));
                return _condition;
            }
        }

    }
}
