using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenExclusiveOrAssignmentExpression : GreenExpression, IAssignmentExpression
    {
        public GreenToken IdentifierToken { get; }
        public GreenToken OperatorToken { get; }
        public GreenExpression Expression { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => IdentifierToken,
            1 => OperatorToken,
            2 => Expression,
            _ => null
        };

        public GreenExclusiveOrAssignmentExpression(
            SyntaxKind kind,
            GreenToken identifierToken,
            GreenToken operatorToken,
            GreenExpression expression
        )
            : base(kind)
        {
            IdentifierToken = identifierToken;
            OperatorToken = operatorToken;
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.ExclusiveOrAssignmentExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenExclusiveOrAssignmentExpression(Kind, IdentifierToken, OperatorToken, Expression);
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
