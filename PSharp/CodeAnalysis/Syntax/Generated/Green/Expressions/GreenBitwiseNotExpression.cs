using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenBitwiseNotExpression : GreenExpression, IUnaryExpression
    {
        public GreenToken OperatorToken { get; }
        public GreenExpression Operand { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => OperatorToken,
            1 => Operand,
            _ => null
        };

        public GreenBitwiseNotExpression(
            SyntaxKind kind,
            GreenToken operatorToken,
            GreenExpression operand
        )
            : base(kind)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }

        public override SyntaxKind Kind => SyntaxKind.BitwiseNotExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenBitwiseNotExpression(Kind, OperatorToken, Operand);
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
