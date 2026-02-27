using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenUIntLiteralExpression : GreenExpression
    {
        public GreenToken UIntLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => UIntLiteralToken,
            _ => null
        };

        public GreenUIntLiteralExpression(
            SyntaxKind kind,
            GreenToken uIntLiteralToken
        )
            : base(kind)
        {
            UIntLiteralToken = uIntLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.UIntLiteralExpression;

        public object Value
            => UIntLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenUIntLiteralExpression(Kind, UIntLiteralToken);
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
