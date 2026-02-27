using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenBitwiseAndExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken AmpersandToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => AmpersandToken,
            2 => Right,
            _ => null
        };

        public GreenBitwiseAndExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken ampersandToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            AmpersandToken = ampersandToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BitwiseAndExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenBitwiseAndExpression(Kind, Left, AmpersandToken, Right);
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
