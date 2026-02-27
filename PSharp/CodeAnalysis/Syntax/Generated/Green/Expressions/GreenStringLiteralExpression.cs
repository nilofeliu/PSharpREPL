using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenStringLiteralExpression : GreenExpression, ILiteralExpression
    {
        public GreenToken LiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => LiteralToken,
            _ => null
        };

        public GreenStringLiteralExpression(
            SyntaxKind kind,
            GreenToken literalToken
        )
            : base(kind)
        {
            LiteralToken = literalToken;
        }

        public override SyntaxKind Kind => SyntaxKind.StringLiteralExpression;

        public object Value
            => LiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenStringLiteralExpression(Kind, LiteralToken);
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
