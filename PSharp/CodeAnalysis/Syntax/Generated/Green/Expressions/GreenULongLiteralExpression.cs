using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenULongLiteralExpression : GreenExpression
    {
        public GreenToken ULongLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => ULongLiteralToken,
            _ => null
        };

        public GreenULongLiteralExpression(
            SyntaxKind kind,
            GreenToken uLongLiteralToken
        )
            : base(kind)
        {
            ULongLiteralToken = uLongLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ULongLiteralExpression;

        public object Value
            => ULongLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenULongLiteralExpression(Kind, ULongLiteralToken);
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
