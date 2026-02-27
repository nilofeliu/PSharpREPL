using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenLocalDeclarationStatement : GreenStatement
    {
        public GreenToken Keyword { get; }
        public GreenVariableDeclaration Declaration { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Keyword,
            1 => Declaration,
            _ => null
        };

        public GreenLocalDeclarationStatement(
            SyntaxKind kind,
            GreenToken keyword,
            GreenVariableDeclaration declaration
        )
            : base(kind)
        {
            Keyword = keyword;
            Declaration = declaration;
        }

        public override SyntaxKind Kind => SyntaxKind.LocalDeclarationStatement;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenLocalDeclarationStatement(Kind, Keyword, Declaration);
            node.Diagnostics = diagnostics;
            return node;
        }

        public override string ToFullString()
        {
            var sb = new System.Text.StringBuilder();
            foreach (var child in GetChildren())
                sb.Append(child.ToFullString());
            return sb.ToString();
        }
    }
}
