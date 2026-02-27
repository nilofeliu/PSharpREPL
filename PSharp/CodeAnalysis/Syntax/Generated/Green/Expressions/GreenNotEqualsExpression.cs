using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenNotEqualsExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken BangEqualsToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => BangEqualsToken,
            2 => Right,
            _ => null
        };

        public GreenNotEqualsExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken bangEqualsToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            BangEqualsToken = bangEqualsToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.NotEqualsExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenNotEqualsExpression(Kind, Left, BangEqualsToken, Right);
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
