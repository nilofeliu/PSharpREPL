using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenAndAssignmentExpression : GreenExpression
    {
        public GreenToken IdentifierToken { get; }
        public GreenToken AmpersandEqualsToken { get; }
        public GreenExpression Expression { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => IdentifierToken,
            1 => AmpersandEqualsToken,
            2 => Expression,
            _ => null
        };

        public GreenAndAssignmentExpression(
            SyntaxKind kind,
            GreenToken identifierToken,
            GreenToken ampersandEqualsToken,
            GreenExpression expression
        )
            : base(kind)
        {
            IdentifierToken = identifierToken;
            AmpersandEqualsToken = ampersandEqualsToken;
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.AndAssignmentExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenAndAssignmentExpression(Kind, IdentifierToken, AmpersandEqualsToken, Expression);
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
