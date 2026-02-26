using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes.Statements;

namespace PSharp.CodeAnalysis.Syntax.Nodes
{
    public sealed class CompilationUnitSyntax : SyntaxNode
    {
        public CompilationUnitSyntax(StatementSyntax statement, SyntaxToken endOfFileToken)
        {
            Statement = statement;
            EndOfFileToken = endOfFileToken;
        }

        public override SyntaxKind Kind => SyntaxKind.CompilationUnit;
        public StatementSyntax Statement { get; }
        public SyntaxToken EndOfFileToken { get; }

    }

}
