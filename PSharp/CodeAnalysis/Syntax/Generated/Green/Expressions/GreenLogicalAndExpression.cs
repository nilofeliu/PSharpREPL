using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenLogicalAndExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken AmpersandAmpersandToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => AmpersandAmpersandToken,
            2 => Right,
            _ => null
        };

        public GreenLogicalAndExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken ampersandAmpersandToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            AmpersandAmpersandToken = ampersandAmpersandToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.LogicalAndExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenLogicalAndExpression(Kind, Left, AmpersandAmpersandToken, Right);
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
