using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenCoalesceExpression : GreenExpression
    {
        public GreenExpression Left { get; }
        public GreenToken QuestionQuestionToken { get; }
        public GreenExpression Right { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Left,
            1 => QuestionQuestionToken,
            2 => Right,
            _ => null
        };

        public GreenCoalesceExpression(
            SyntaxKind kind,
            GreenExpression left,
            GreenToken questionQuestionToken,
            GreenExpression right
        )
            : base(kind)
        {
            Left = left;
            QuestionQuestionToken = questionQuestionToken;
            Right = right;
        }

        public override SyntaxKind Kind => SyntaxKind.CoalesceExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenCoalesceExpression(Kind, Left, QuestionQuestionToken, Right);
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
