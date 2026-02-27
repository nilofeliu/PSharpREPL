using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenSByteLiteralExpression : GreenExpression
    {
        public GreenToken SByteLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => SByteLiteralToken,
            _ => null
        };

        public GreenSByteLiteralExpression(
            SyntaxKind kind,
            GreenToken sByteLiteralToken
        )
            : base(kind)
        {
            SByteLiteralToken = sByteLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.SByteLiteralExpression;

        public object Value
            => SByteLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenSByteLiteralExpression(Kind, SByteLiteralToken);
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
