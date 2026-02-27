using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenByteLiteralExpression : GreenExpression
    {
        public GreenToken ByteLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => ByteLiteralToken,
            _ => null
        };

        public GreenByteLiteralExpression(
            SyntaxKind kind,
            GreenToken byteLiteralToken
        )
            : base(kind)
        {
            ByteLiteralToken = byteLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ByteLiteralExpression;

        public object Value
            => ByteLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenByteLiteralExpression(Kind, ByteLiteralToken);
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
