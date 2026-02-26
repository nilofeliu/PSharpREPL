using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements;

public sealed class SwitchCaseStatementSyntax : StatementSyntax
{
    public SwitchCaseStatementSyntax(
        SyntaxToken caseKeyword,
        ExpressionSyntax? caseMatch,
        SyntaxToken caseColonToken,
        StatementSyntax? body)
    {
        CaseKeyword = caseKeyword;
        Expression = caseMatch;
        CaseColonToken = caseColonToken;
        Body = body;
    }

    public override SyntaxKind Kind =>
     CaseKeyword.Kind == SyntaxKind.CaseKeyword
         ? SyntaxKind.CaseSwitchLabel
         : SyntaxKind.DefaultSwitchLabel;
    public SyntaxToken CaseKeyword { get; }
    public ExpressionSyntax? Expression { get; }
    public SyntaxToken CaseColonToken { get; }
    public StatementSyntax? Body { get; }
}
