using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenPreDecrementExpression : GreenExpression
    {
        public GreenToken MinusMinusToken { get; }
        public GreenExpression Operand { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => MinusMinusToken,
            1 => Operand,
            _ => null
        };

        public GreenPreDecrementExpression(
            SyntaxKind kind,
            GreenToken minusMinusToken,
            GreenExpression operand
        )
            : base(kind)
        {
            MinusMinusToken = minusMinusToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.PreDecrementExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenPreDecrementExpression(Kind, MinusMinusToken, Operand);
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
