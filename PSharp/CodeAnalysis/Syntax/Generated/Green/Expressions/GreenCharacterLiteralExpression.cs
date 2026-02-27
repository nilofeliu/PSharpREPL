using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Expressions
{
    internal sealed class GreenCharacterLiteralExpression : GreenExpression
    {
        public GreenToken CharacterLiteralToken { get; }

        public override int SlotCount => 1;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => CharacterLiteralToken,
            _ => null
        };

        public GreenCharacterLiteralExpression(
            SyntaxKind kind,
            GreenToken characterLiteralToken
        )
            : base(kind)
        {
            CharacterLiteralToken = characterLiteralToken;
        }

        public override SyntaxKind Kind => SyntaxKind.CharacterLiteralExpression;

        public object Value
            => CharacterLiteralToken.Value;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenCharacterLiteralExpression(Kind, CharacterLiteralToken);
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
