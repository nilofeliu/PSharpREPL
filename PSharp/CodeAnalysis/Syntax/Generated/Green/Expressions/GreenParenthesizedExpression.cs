using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenParenthesizedExpression : GreenExpression
    {
        public GreenToken OpenParenthesisToken { get; }
        public GreenExpression Expression { get; }
        public GreenToken CloseParenthesisToken { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => OpenParenthesisToken,
            1 => Expression,
            2 => CloseParenthesisToken,
            _ => null
        };

        public GreenParenthesizedExpression(
            SyntaxKind kind,
            GreenToken openParenthesisToken,
            GreenExpression expression,
            GreenToken closeParenthesisToken
        )
            : base(kind)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenParenthesizedExpression(Kind, OpenParenthesisToken, Expression, CloseParenthesisToken);
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
