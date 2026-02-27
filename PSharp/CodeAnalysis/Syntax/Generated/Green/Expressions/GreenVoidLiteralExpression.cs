using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenVoidLiteralExpression : GreenExpression
    {
        public GreenToken VoidLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => VoidLiteralToken,
            _ => null
        };

        public GreenVoidLiteralExpression(
            SyntaxKind kind,
            GreenToken voidLiteralToken
        )
            : base(kind)
        {
            VoidLiteralToken = voidLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.VoidLiteralExpression;

        public object Value
            => VoidLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenVoidLiteralExpression(Kind, VoidLiteralToken);
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
