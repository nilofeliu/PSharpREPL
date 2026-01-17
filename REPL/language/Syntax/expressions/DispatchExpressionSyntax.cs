using REPL.language.ast;

namespace REPL.language.Syntax.expressions;

public sealed class DispatchExpressionSyntax : ExpressionSyntax
{
    public override SyntaxKind Kind => SyntaxKind.DispatchExpression;
    public SyntaxToken Token { get; }
    public ExpressionSyntax Expression { get; }

    public DispatchExpressionSyntax(SyntaxToken commandToken)
    {
        Token = commandToken;     
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Token;
    }
}
