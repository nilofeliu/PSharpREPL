using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    internal class DoWhileStatementSyntax : StatementSyntax
    {

        public DoWhileStatementSyntax(SyntaxToken doKeyword, SyntaxToken colonToken, BlockStatementSyntax body, ExpressionSyntax condition)
        {
            DoKeyword = doKeyword;
            ColonToken = colonToken;
            Body = body;
            Condition = condition;
        }

        public override SyntaxKind Kind => SyntaxKind.DoWhileStatement;
        public SyntaxToken DoKeyword { get; }
        public SyntaxToken ColonToken { get; }
        public BlockStatementSyntax Body { get; }
        public ExpressionSyntax Condition { get; }
    }
}