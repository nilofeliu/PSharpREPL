using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenGreaterThanOrEqualExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken GreaterThanEqualsToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => GreaterThanEqualsToken,
            2 => Right,
            _ => null
        };

        public GreenGreaterThanOrEqualExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken greaterThanEqualsToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            GreaterThanEqualsToken = greaterThanEqualsToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.GreaterThanOrEqualExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenGreaterThanOrEqualExpression(Kind, Left, GreaterThanEqualsToken, Right);
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
