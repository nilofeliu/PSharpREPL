using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenLessThanOrEqualExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken LessThanEqualsToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => LessThanEqualsToken,
            2 => Right,
            _ => null
        };

        public GreenLessThanOrEqualExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken lessThanEqualsToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            LessThanEqualsToken = lessThanEqualsToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.LessThanOrEqualExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenLessThanOrEqualExpression(Kind, Left, LessThanEqualsToken, Right);
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
