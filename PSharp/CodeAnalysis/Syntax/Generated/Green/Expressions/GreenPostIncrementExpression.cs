using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenPostIncrementExpression : GreenExpression
    {
        public GreenExpression Operand { get; }
        public GreenToken PlusPlusToken { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Operand,
            1 => PlusPlusToken,
            _ => null
        };

        public GreenPostIncrementExpression(
            SyntaxKind kind,
            GreenExpression operand,
            GreenToken plusPlusToken
        )
            : base(kind)
        {
            Operand = operand;
            PlusPlusToken = plusPlusToken;
        }

        public override SyntaxKind Kind => SyntaxKind.PostIncrementExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenPostIncrementExpression(Kind, Operand, PlusPlusToken);
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
