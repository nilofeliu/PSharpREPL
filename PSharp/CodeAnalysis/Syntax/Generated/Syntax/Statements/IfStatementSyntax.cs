using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class IfStatementSyntax : StatementSyntax
    {
        private readonly GreenIfStatement _green;

        private ExpressionSyntax _condition;
        private StatementSyntax _thenStatement;
        private ElseClauseSyntax? _elseClause;

        internal IfStatementSyntax(GreenIfStatement green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.IfStatement;

        public SyntaxToken IfKeyword
            => new SyntaxToken(_green.IfKeyword, this, GetChildPosition(0));

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

        public StatementSyntax ThenStatement
        {
            get
            {
                if (_thenStatement == null)
                    _thenStatement = (StatementSyntax)RedNodeFactory.CreateRed(_green.ThenStatement, this, GetChildPosition(3));
                return _thenStatement;
            }
        }

        public ElseClauseSyntax? ElseClause
        {
            get
            {
                if (_elseClause == null)
                    _elseClause = (ElseClauseSyntax)RedNodeFactory.CreateRed(_green.ElseClause, this, GetChildPosition(4));
                return _elseClause;
            }
        }

        public SyntaxToken? EndKeyword
            => new SyntaxToken(_green.EndKeyword, this, GetChildPosition(5));

    }
}
