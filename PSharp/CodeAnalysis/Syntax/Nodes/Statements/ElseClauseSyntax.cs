using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class ElseClauseSyntax : StatementSyntax
    {
        public ElseClauseSyntax(SyntaxToken elseKeyword, SyntaxToken elseColonToken,  StatementSyntax elseStatement)
        {
            ElseKeyword = elseKeyword;
            ElseColonToken = elseColonToken;
            ElseStatement = elseStatement;
        }

        public override SyntaxKind Kind => SyntaxKind.ElseClause;
        public SyntaxToken ElseKeyword { get; }
        public SyntaxToken ElseColonToken { get; }
        public StatementSyntax ElseStatement { get; }
    }

}
