using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenMultiplyExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken StarToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => StarToken,
            2 => Right,
            _ => null
        };

        public GreenMultiplyExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken starToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            StarToken = starToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.MultiplyExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenMultiplyExpression(Kind, Left, StarToken, Right);
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
