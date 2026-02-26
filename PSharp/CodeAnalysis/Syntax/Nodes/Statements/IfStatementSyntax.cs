using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements;



public sealed class IfStatementSyntax : StatementSyntax
{
    public IfStatementSyntax(SyntaxToken ifKeyword,
        ExpressionSyntax condition,
        SyntaxToken colonToken,
        StatementSyntax thenStatement,
        ElseClauseSyntax elseClause,
        SyntaxToken endKeyword)
    {
        IfKeyword = ifKeyword;
        Condition = condition;
        ColonToken = colonToken;
        ThenStatement = thenStatement;
        ElseClause = elseClause;
        EndKeyword = endKeyword;
    }

    public override SyntaxKind Kind => SyntaxKind.IfStatement;
    public SyntaxToken IfKeyword { get; }
    public ExpressionSyntax Condition { get; }
    public SyntaxToken ColonToken { get; }
    public StatementSyntax ThenStatement { get; }
    public ElseClauseSyntax ElseClause { get; }
    public SyntaxToken? EndKeyword { get; }
}
       