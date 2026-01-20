using REPL.language.ast;

namespace REPL.language.Syntax.expressions;

public sealed class ExceptionExpressionSyntax : ExpressionSyntax
{
    public ExceptionExpressionSyntax(SyntaxToken badToken)
    {
        IdentifierToken = badToken;
    }

    public SyntaxToken IdentifierToken { get; }

    public override SyntaxKind Kind => SyntaxKind.BadExpression;


}