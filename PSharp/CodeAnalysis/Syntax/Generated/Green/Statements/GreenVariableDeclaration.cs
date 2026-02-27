using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenVariableDeclaration : GreenStatement
    {
        public GreenToken Keyword { get; }
        public GreenToken? Type { get; }
        public GreenNodeList Variables { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Keyword,
            1 => Type,
            2 => Variables,
            _ => null
        };

        public GreenVariableDeclaration(
            SyntaxKind kind,
            GreenToken keyword,
            GreenToken? type,
            GreenNodeList variables
        )
            : base(kind)
        {
            Keyword = keyword;
            Type = type;
            Variables = variables;
        }

        public override SyntaxKind Kind => SyntaxKind.VariableDeclaration;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenVariableDeclaration(Kind, Keyword, Type, Variables);
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
