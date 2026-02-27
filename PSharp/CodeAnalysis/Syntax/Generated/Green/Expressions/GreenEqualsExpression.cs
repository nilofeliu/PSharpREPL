using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenEqualsExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken EqualsEqualsToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => EqualsEqualsToken,
            2 => Right,
            _ => null
        };

        public GreenEqualsExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken equalsEqualsToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            EqualsEqualsToken = equalsEqualsToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.EqualsExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenEqualsExpression(Kind, Left, EqualsEqualsToken, Right);
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
