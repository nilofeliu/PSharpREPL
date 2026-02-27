using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenLessThanExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken LessThanToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => LessThanToken,
            2 => Right,
            _ => null
        };

        public GreenLessThanExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken lessThanToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            LessThanToken = lessThanToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.LessThanExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenLessThanExpression(Kind, Left, LessThanToken, Right);
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
