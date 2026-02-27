using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenExclusiveOrExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken CaretToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => CaretToken,
            2 => Right,
            _ => null
        };

        public GreenExclusiveOrExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken caretToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            CaretToken = caretToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.ExclusiveOrExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenExclusiveOrExpression(Kind, Left, CaretToken, Right);
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
