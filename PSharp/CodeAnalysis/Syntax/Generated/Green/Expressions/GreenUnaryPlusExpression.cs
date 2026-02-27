using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenUnaryPlusExpression : GreenExpression
    {
        public GreenToken PlusToken { get; }
        public GreenExpression Operand { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => PlusToken,
            1 => Operand,
            _ => null
        };

        public GreenUnaryPlusExpression(
            SyntaxKind kind,
            GreenToken plusToken,
            GreenExpression operand
        )
            : base(kind)
        {
            PlusToken = plusToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.UnaryPlusExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenUnaryPlusExpression(Kind, PlusToken, Operand);
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
