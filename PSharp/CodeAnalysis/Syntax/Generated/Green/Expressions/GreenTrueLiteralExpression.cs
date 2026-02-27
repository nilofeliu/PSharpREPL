using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenTrueLiteralExpression : GreenExpression
    {
        public GreenToken TrueLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => TrueLiteralToken,
            _ => null
        };

        public GreenTrueLiteralExpression(
            SyntaxKind kind,
            GreenToken trueLiteralToken
        )
            : base(kind)
        {
            TrueLiteralToken = trueLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.TrueLiteralExpression;

        public object Value
            => TrueLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenTrueLiteralExpression(Kind, TrueLiteralToken);
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
