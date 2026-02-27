using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class WhileStatementSyntax : StatementSyntax
    {
        private readonly GreenWhileStatement _green;

        private ExpressionSyntax _condition;
        private StatementSyntax _body;

        internal WhileStatementSyntax(GreenWhileStatement green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.WhileStatement;

        public SyntaxToken WhileKeyword
            => new SyntaxToken(_green.WhileKeyword, this, GetChildPosition(0));

        public ExpressionSyntax Condition
        {
            get
            {
                if (_condition == null)
                    _condition = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Condition, this, GetChildPosition(1));
                return _condition;
            }
        }

        public SyntaxToken ColonToken
            => new SyntaxToken(_green.ColonToken, this, GetChildPosition(2));

        public StatementSyntax Body
        {
            get
            {
                if (_body == null)
                    _body = (StatementSyntax)RedNodeFactory.CreateRed(_green.Body, this, GetChildPosition(3));
                return _body;
            }
        }

        public SyntaxToken EndKeyword
            => new SyntaxToken(_green.EndKeyword, this, GetChildPosition(4));

    }
}
