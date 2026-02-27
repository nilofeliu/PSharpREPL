using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenBitwiseAndExpression : GreenExpression, IBinaryExpression
    {
        public GreenExpression Left { get; }
        public GreenToken OperatorToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => OperatorToken,
            2 => Right,
            _ => null
        };

        public GreenBitwiseAndExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken operatorToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.BitwiseAndExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenBitwiseAndExpression(Kind, Left, OperatorToken, Right);
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
