using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenDefaultLiteralExpression : GreenExpression
    {
        public GreenToken DefaultLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => DefaultLiteralToken,
            _ => null
        };

        public GreenDefaultLiteralExpression(
            SyntaxKind kind,
            GreenToken defaultLiteralToken
        )
            : base(kind)
        {
            DefaultLiteralToken = defaultLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.DefaultLiteralExpression;

        public object Value
            => DefaultLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenDefaultLiteralExpression(Kind, DefaultLiteralToken);
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
