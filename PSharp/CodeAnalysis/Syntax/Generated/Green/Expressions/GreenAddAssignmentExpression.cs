using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenAddAssignmentExpression : GreenExpression
    {
        public GreenToken IdentifierToken { get; }
        public GreenToken PlusEqualsToken { get; }
        public GreenExpression Expression { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => IdentifierToken,
            1 => PlusEqualsToken,
            2 => Expression,
            _ => null
        };

        public GreenAddAssignmentExpression(
            SyntaxKind kind,
            GreenToken identifierToken,
            GreenToken plusEqualsToken,
            GreenExpression expression
        )
            : base(kind)
        {
            IdentifierToken = identifierToken;
            PlusEqualsToken = plusEqualsToken;
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.AddAssignmentExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenAddAssignmentExpression(Kind, IdentifierToken, PlusEqualsToken, Expression);
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
