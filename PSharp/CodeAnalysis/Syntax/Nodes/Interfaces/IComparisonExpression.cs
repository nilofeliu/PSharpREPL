namespace PSharp.CodeAnalysis.Syntax.Nodes.Interfaces
{
    public interface IComparisonExpression
    {
        ExpressionSyntax Left { get; }
        SyntaxToken OperatorToken { get; }
        ExpressionSyntax Right { get; }
    }
}
