using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenDoubleLiteralExpression : GreenExpression
    {
        public GreenToken DoubleLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => DoubleLiteralToken,
            _ => null
        };

        public GreenDoubleLiteralExpression(
            SyntaxKind kind,
            GreenToken doubleLiteralToken
        )
            : base(kind)
        {
            DoubleLiteralToken = doubleLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.DoubleLiteralExpression;

        public object Value
            => DoubleLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenDoubleLiteralExpression(Kind, DoubleLiteralToken);
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
