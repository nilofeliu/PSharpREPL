using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenLogicalNotExpression : GreenExpression
    {
        public GreenToken BangToken { get; }
        public GreenExpression Operand { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => BangToken,
            1 => Operand,
            _ => null
        };

        public GreenLogicalNotExpression(
            SyntaxKind kind,
            GreenToken bangToken,
            GreenExpression operand
        )
            : base(kind)
        {
            BangToken = bangToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.LogicalNotExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenLogicalNotExpression(Kind, BangToken, Operand);
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
