using REPL.language.ast;

namespace REPL.language.Syntax.expressions;

public sealed class CommandExpressionSyntax : ExpressionSyntax
{
    public override SyntaxKind Kind => SyntaxKind.CommandToken;
    public SyntaxToken Token { get; }
    public ExpressionSyntax Expression { get; }
    public CommandExpressionSyntax(SyntaxToken cmdToken, ExpressionSyntax expression)
    {
        Token = cmdToken;
        Expression = expression;
    }
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Token;
    }
}
