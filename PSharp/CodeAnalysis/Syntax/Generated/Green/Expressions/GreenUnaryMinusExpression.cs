using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenUnaryMinusExpression : GreenExpression
    {
        public GreenToken MinusToken { get; }
        public GreenExpression Operand { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => MinusToken,
            1 => Operand,
            _ => null
        };

        public GreenUnaryMinusExpression(
            SyntaxKind kind,
            GreenToken minusToken,
            GreenExpression operand
        )
            : base(kind)
        {
            MinusToken = minusToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.UnaryMinusExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenUnaryMinusExpression(Kind, MinusToken, Operand);
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
