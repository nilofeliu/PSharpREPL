using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;
using PSharp.CodeAnalysis.Syntax.Nodes.Statements;
using System;


namespace PSharp.CodeAnalysis.Syntax.Nodes
{
    public interface IBinaryExpression
    {
        ExpressionSyntax Left { get; }
        SyntaxToken OperatorToken { get; }
        ExpressionSyntax Right { get; }
    }

    public interface IComparisonExpression
    {
        ExpressionSyntax Left { get; }
        SyntaxToken OperatorToken { get; }
        ExpressionSyntax Right { get; }
    }

    public interface ILogicalExpression
    {
        ExpressionSyntax Left { get; }
        SyntaxToken OperatorToken { get; }
        ExpressionSyntax Right { get; }
    }

    public interface IUnaryExpression
    {
        SyntaxToken OperatorToken { get; }
        ExpressionSyntax Operand { get; }
    }

    public interface ILiteralExpression
    {
        SyntaxToken LiteralToken { get; }
        object Value { get; }
    }

    public interface IAssignmentExpression
    {
        SyntaxToken IdentifierToken { get; }
        SyntaxToken OperatorToken { get; }
        ExpressionSyntax Expression { get; }
    }
}
//Now statements:


namespace Minsk.CodeAnalysis.Syntax.Nodes.Statements
{
    public interface ILoopStatement
    {
        StatementSyntax Body { get; }
    }
    public interface IJumpStatement
    {
        SyntaxToken Keyword { get; }
    }
    public interface ISwitchLabel
    {
        SyntaxToken Keyword { get; }
        SyntaxToken ColonToken { get; }
        StatementSyntax? Body { get; }
    }
}