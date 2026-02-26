namespace PSharp.CodeAnalysis.Syntax.Nodes.Interfaces
{
    public interface ILogicalExpression
    {
        ExpressionSyntax Left { get; }
        SyntaxToken OperatorToken { get; }
        ExpressionSyntax Right { get; }
    }
}
