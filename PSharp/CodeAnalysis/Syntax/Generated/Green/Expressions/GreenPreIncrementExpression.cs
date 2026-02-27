using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenPreIncrementExpression : GreenExpression
    {
        public GreenToken PlusPlusToken { get; }
        public GreenExpression Operand { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => PlusPlusToken,
            1 => Operand,
            _ => null
        };

        public GreenPreIncrementExpression(
            SyntaxKind kind,
            GreenToken plusPlusToken,
            GreenExpression operand
        )
            : base(kind)
        {
            PlusPlusToken = plusPlusToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.PreIncrementExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenPreIncrementExpression(Kind, PlusPlusToken, Operand);
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
