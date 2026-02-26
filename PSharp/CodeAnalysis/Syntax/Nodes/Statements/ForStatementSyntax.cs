using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class ForStatementSyntax : StatementSyntax
    {
        public ForStatementSyntax(SyntaxToken keyword, SyntaxToken identifier,
            SyntaxToken equalsToken, ExpressionSyntax lowerBound,
            SyntaxToken toKeyword, ExpressionSyntax upperBound,
            SyntaxToken colonToken, StatementSyntax body,
            SyntaxToken endToken)
        {
            Keyword = keyword;
            Identifier = identifier;
            EqualsToken = equalsToken;
            LowerBound = lowerBound;
            ToKeyword = toKeyword;
            UpperBound = upperBound;
            ColonToken = colonToken;
            Body = body;
            EndToken = endToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ForStatement;

        public SyntaxToken Keyword { get; }
        public SyntaxToken Identifier { get; }
        public SyntaxToken EqualsToken { get; }
        public ExpressionSyntax LowerBound { get; }
        public SyntaxToken ToKeyword { get; }
        public ExpressionSyntax UpperBound { get; }
        public SyntaxToken ColonToken { get; }
        public StatementSyntax Body { get; }
        public SyntaxToken EndToken { get; }
    }
}
