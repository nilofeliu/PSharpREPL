using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenVariableDeclarator : GreenStatement
    {
        public GreenToken Identifier { get; }
        public GreenEqualsValueClause? Initializer { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Identifier,
            1 => Initializer,
            _ => null
        };

        public GreenVariableDeclarator(
            SyntaxKind kind,
            GreenToken identifier,
            GreenEqualsValueClause? initializer
        )
            : base(kind)
        {
            Identifier = identifier;
            Initializer = initializer;
        }

        public override SyntaxKind Kind => SyntaxKind.VariableDeclarator;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenVariableDeclarator(Kind, Identifier, Initializer);
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
