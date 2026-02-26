using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class WhileStatementSyntax : StatementSyntax
    {
        public WhileStatementSyntax(SyntaxToken whileKeyword, ExpressionSyntax condition, SyntaxToken colonToken, StatementSyntax body, SyntaxToken endKeyword)
        {
            WhileKeyword = whileKeyword;
            Condition = condition;
            ColonToken = colonToken;
            Body = body;
            EndKeyword = endKeyword;
        }

        public override SyntaxKind Kind => SyntaxKind.WhileStatement;
        public SyntaxToken WhileKeyword { get; }
        public ExpressionSyntax Condition { get; }
        public SyntaxToken ColonToken { get; }
        public StatementSyntax Body { get; }
        public SyntaxToken EndKeyword { get; }
    }
}
