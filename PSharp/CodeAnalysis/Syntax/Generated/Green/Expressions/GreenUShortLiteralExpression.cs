using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenUShortLiteralExpression : GreenExpression
    {
        public GreenToken UShortLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => UShortLiteralToken,
            _ => null
        };

        public GreenUShortLiteralExpression(
            SyntaxKind kind,
            GreenToken uShortLiteralToken
        )
            : base(kind)
        {
            UShortLiteralToken = uShortLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.UShortLiteralExpression;

        public object Value
            => UShortLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenUShortLiteralExpression(Kind, UShortLiteralToken);
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
