using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class ElseClauseSyntax : StatementSyntax
    {
        private readonly GreenElseClause _green;

        private StatementSyntax _elseStatement;

        internal ElseClauseSyntax(GreenElseClause green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.ElseClause;

        public SyntaxToken ElseKeyword
            => new SyntaxToken(_green.ElseKeyword, this, GetChildPosition(0));

        public SyntaxToken ElseColonToken
            => new SyntaxToken(_green.ElseColonToken, this, GetChildPosition(1));

        public StatementSyntax ElseStatement
        {
            get
            {
                if (_elseStatement == null)
                    _elseStatement = (StatementSyntax)RedNodeFactory.CreateRed(_green.ElseStatement, this, GetChildPosition(2));
                return _elseStatement;
            }
        }

    }
}
