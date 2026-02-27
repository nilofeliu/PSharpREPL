using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenIntLiteralExpression : GreenExpression
    {
        public GreenToken IntLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => IntLiteralToken,
            _ => null
        };

        public GreenIntLiteralExpression(
            SyntaxKind kind,
            GreenToken intLiteralToken
        )
            : base(kind)
        {
            IntLiteralToken = intLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.IntLiteralExpression;

        public object Value
            => IntLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenIntLiteralExpression(Kind, IntLiteralToken);
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
