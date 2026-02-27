using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenLongLiteralExpression : GreenExpression
    {
        public GreenToken LongLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => LongLiteralToken,
            _ => null
        };

        public GreenLongLiteralExpression(
            SyntaxKind kind,
            GreenToken longLiteralToken
        )
            : base(kind)
        {
            LongLiteralToken = longLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.LongLiteralExpression;

        public object Value
            => LongLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenLongLiteralExpression(Kind, LongLiteralToken);
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
