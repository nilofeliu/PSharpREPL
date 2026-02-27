using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenPostDecrementExpression : GreenExpression
    {
        public GreenExpression Operand { get; }
        public GreenToken MinusMinusToken { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Operand,
            1 => MinusMinusToken,
            _ => null
        };

        public GreenPostDecrementExpression(
            SyntaxKind kind,
            GreenExpression operand,
            GreenToken minusMinusToken
        )
            : base(kind)
        {
            Operand = operand;
            MinusMinusToken = minusMinusToken;
        }

        public override SyntaxKind Kind => SyntaxKind.PostDecrementExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenPostDecrementExpression(Kind, Operand, MinusMinusToken);
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
