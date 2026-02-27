using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenLogicalOrExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken PipePipeToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => PipePipeToken,
            2 => Right,
            _ => null
        };

        public GreenLogicalOrExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken pipePipeToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            PipePipeToken = pipePipeToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.LogicalOrExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenLogicalOrExpression(Kind, Left, PipePipeToken, Right);
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
