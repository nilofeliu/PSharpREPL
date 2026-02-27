using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenCoalesceAssignmentExpression : GreenExpression
    {
        public GreenToken IdentifierToken { get; }
        public GreenToken QuestionQuestionEqualsToken { get; }
        public GreenExpression Expression { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => IdentifierToken,
            1 => QuestionQuestionEqualsToken,
            2 => Expression,
            _ => null
        };

        public GreenCoalesceAssignmentExpression(
            SyntaxKind kind,
            GreenToken identifierToken,
            GreenToken questionQuestionEqualsToken,
            GreenExpression expression
        )
            : base(kind)
        {
            IdentifierToken = identifierToken;
            QuestionQuestionEqualsToken = questionQuestionEqualsToken;
            Expression = expression;
        }

        public override SyntaxKind Kind => SyntaxKind.CoalesceAssignmentExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenCoalesceAssignmentExpression(Kind, IdentifierToken, QuestionQuestionEqualsToken, Expression);
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
