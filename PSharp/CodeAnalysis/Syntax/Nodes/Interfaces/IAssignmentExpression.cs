namespace PSharp.CodeAnalysis.Syntax.Nodes.Interfaces
{
    public interface IAssignmentExpression
    {
        SyntaxToken IdentifierToken { get; }
        SyntaxToken OperatorToken { get; }
        ExpressionSyntax Expression { get; }
    }
}
