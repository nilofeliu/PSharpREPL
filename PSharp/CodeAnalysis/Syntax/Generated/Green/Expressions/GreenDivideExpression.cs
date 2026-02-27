using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenDivideExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken SlashToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => SlashToken,
            2 => Right,
            _ => null
        };

        public GreenDivideExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken slashToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            SlashToken = slashToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.DivideExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenDivideExpression(Kind, Left, SlashToken, Right);
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
