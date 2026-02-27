using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenEqualsValueClause : GreenStatement
    {
        public GreenToken EqualsToken { get; }
        public GreenExpression Value { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => EqualsToken,
            1 => Value,
            _ => null
        };

        public GreenEqualsValueClause(
            SyntaxKind kind,
            GreenToken equalsToken,
            GreenExpression value
        )
            : base(kind)
        {
            EqualsToken = equalsToken;
            Value = value;
        }

        public override SyntaxKind Kind => SyntaxKind.EqualsValueClause;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenEqualsValueClause(Kind, EqualsToken, Value);
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
