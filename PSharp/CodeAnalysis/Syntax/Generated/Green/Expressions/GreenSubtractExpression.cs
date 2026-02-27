using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenSubtractExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken MinusToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => MinusToken,
            2 => Right,
            _ => null
        };

        public GreenSubtractExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken minusToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            MinusToken = minusToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.SubtractExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenSubtractExpression(Kind, Left, MinusToken, Right);
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
