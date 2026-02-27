using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenNullLiteralExpression : GreenExpression
    {
        public GreenToken NullLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => NullLiteralToken,
            _ => null
        };

        public GreenNullLiteralExpression(
            SyntaxKind kind,
            GreenToken nullLiteralToken
        )
            : base(kind)
        {
            NullLiteralToken = nullLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NullLiteralExpression;

        public object Value
            => NullLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenNullLiteralExpression(Kind, NullLiteralToken);
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
