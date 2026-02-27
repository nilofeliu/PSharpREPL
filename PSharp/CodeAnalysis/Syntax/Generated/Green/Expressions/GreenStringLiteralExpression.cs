using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenStringLiteralExpression : GreenExpression
    {
        public GreenToken StringLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => StringLiteralToken,
            _ => null
        };

        public GreenStringLiteralExpression(
            SyntaxKind kind,
            GreenToken stringLiteralToken
        )
            : base(kind)
        {
            StringLiteralToken = stringLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.StringLiteralExpression;

        public object Value
            => StringLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenStringLiteralExpression(Kind, StringLiteralToken);
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
