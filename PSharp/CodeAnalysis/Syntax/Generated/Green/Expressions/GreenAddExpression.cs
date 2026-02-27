using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenAddExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken PlusToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => PlusToken,
            2 => Right,
            _ => null
        };

        public GreenAddExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken plusToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            PlusToken = plusToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.AddExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenAddExpression(Kind, Left, PlusToken, Right);
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
