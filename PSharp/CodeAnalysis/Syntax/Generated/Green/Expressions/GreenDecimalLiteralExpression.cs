using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenDecimalLiteralExpression : GreenExpression
    {
        public GreenToken DecimalLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => DecimalLiteralToken,
            _ => null
        };

        public GreenDecimalLiteralExpression(
            SyntaxKind kind,
            GreenToken decimalLiteralToken
        )
            : base(kind)
        {
            DecimalLiteralToken = decimalLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.DecimalLiteralExpression;

        public object Value
            => DecimalLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenDecimalLiteralExpression(Kind, DecimalLiteralToken);
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
