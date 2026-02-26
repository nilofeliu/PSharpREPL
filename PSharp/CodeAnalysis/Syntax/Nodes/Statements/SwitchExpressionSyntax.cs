using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements;

public sealed class SwitchStatementSyntax : StatementSyntax
{
    public SwitchStatementSyntax(
        SyntaxToken switchKeyword,
        ExpressionSyntax pattern,
        SyntaxToken colonToken,
        ImmutableArray<SwitchCaseStatementSyntax> cases,
        SwitchCaseStatementSyntax defaultCase, SyntaxToken endToken)
    {
        SwitchKeyword = switchKeyword;
        Pattern = pattern;
        ColonToken = colonToken;
        Cases = cases;
        DefaultCase = defaultCase;
        EndToken = endToken;
    }

    public override SyntaxKind Kind => SyntaxKind.SwitchStatement;
    public SyntaxToken SwitchKeyword { get; }
    public ExpressionSyntax Pattern { get; }  // ← Added
    public SyntaxToken ColonToken { get; }
    public ImmutableArray<SwitchCaseStatementSyntax>? Cases { get; }
    public SwitchCaseStatementSyntax? DefaultCase { get; }
    public SyntaxToken EndToken { get; }
}