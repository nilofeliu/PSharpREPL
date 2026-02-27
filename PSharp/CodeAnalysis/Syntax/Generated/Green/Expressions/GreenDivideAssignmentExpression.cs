using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenDivideAssignmentExpression : GreenExpression
    {
        public GreenToken IdentifierToken { get; }
        public GreenToken SlashEqualsToken { get; }
        public GreenExpression Expression { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => IdentifierToken,
            1 => SlashEqualsToken,
            2 => Expression,
            _ => null
        };

        public GreenDivideAssignmentExpression(
            SyntaxKind kind,
            GreenToken identifierToken,
            GreenToken slashEqualsToken,
            GreenExpression expression
        )
            : base(kind)
        {
            IdentifierToken = identifierToken;
            SlashEqualsToken = slashEqualsToken;
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.DivideAssignmentExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenDivideAssignmentExpression(Kind, IdentifierToken, SlashEqualsToken, Expression);
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
