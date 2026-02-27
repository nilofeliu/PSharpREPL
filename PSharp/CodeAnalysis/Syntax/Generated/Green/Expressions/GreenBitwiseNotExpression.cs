using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenBitwiseNotExpression : GreenExpression
    {
        public GreenToken TildeToken { get; }
        public GreenExpression Operand { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => TildeToken,
            1 => Operand,
            _ => null
        };

        public GreenBitwiseNotExpression(
            SyntaxKind kind,
            GreenToken tildeToken,
            GreenExpression operand
        )
            : base(kind)
        {
            TildeToken = tildeToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.BitwiseNotExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenBitwiseNotExpression(Kind, TildeToken, Operand);
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
