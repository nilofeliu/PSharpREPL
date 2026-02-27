using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenFloatLiteralExpression : GreenExpression
    {
        public GreenToken FloatLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => FloatLiteralToken,
            _ => null
        };

        public GreenFloatLiteralExpression(
            SyntaxKind kind,
            GreenToken floatLiteralToken
        )
            : base(kind)
        {
            FloatLiteralToken = floatLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.FloatLiteralExpression;

        public object Value
            => FloatLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenFloatLiteralExpression(Kind, FloatLiteralToken);
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
