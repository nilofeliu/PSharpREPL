using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenShortLiteralExpression : GreenExpression
    {
        public GreenToken ShortLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => ShortLiteralToken,
            _ => null
        };

        public GreenShortLiteralExpression(
            SyntaxKind kind,
            GreenToken shortLiteralToken
        )
            : base(kind)
        {
            ShortLiteralToken = shortLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ShortLiteralExpression;

        public object Value
            => ShortLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenShortLiteralExpression(Kind, ShortLiteralToken);
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
