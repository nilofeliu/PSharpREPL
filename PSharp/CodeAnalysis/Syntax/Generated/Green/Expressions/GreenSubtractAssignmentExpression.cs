using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenSubtractAssignmentExpression : GreenExpression
    {
        public GreenToken IdentifierToken { get; }
        public GreenToken MinusEqualsToken { get; }
        public GreenExpression Expression { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => IdentifierToken,
            1 => MinusEqualsToken,
            2 => Expression,
            _ => null
        };

        public GreenSubtractAssignmentExpression(
            SyntaxKind kind,
            GreenToken identifierToken,
            GreenToken minusEqualsToken,
            GreenExpression expression
        )
            : base(kind)
        {
            IdentifierToken = identifierToken;
            MinusEqualsToken = minusEqualsToken;
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.SubtractAssignmentExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenSubtractAssignmentExpression(Kind, IdentifierToken, MinusEqualsToken, Expression);
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
