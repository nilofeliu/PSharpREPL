using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenGreaterThanExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken GreaterThanToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => GreaterThanToken,
            2 => Right,
            _ => null
        };

        public GreenGreaterThanExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken greaterThanToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            GreaterThanToken = greaterThanToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.GreaterThanExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenGreaterThanExpression(Kind, Left, GreaterThanToken, Right);
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
