using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenFalseLiteralExpression : GreenExpression
    {
        public GreenToken FalseLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => FalseLiteralToken,
            _ => null
        };

        public GreenFalseLiteralExpression(
            SyntaxKind kind,
            GreenToken falseLiteralToken
        )
            : base(kind)
        {
            FalseLiteralToken = falseLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.FalseLiteralExpression;

        public object Value
            => FalseLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenFalseLiteralExpression(Kind, FalseLiteralToken);
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
