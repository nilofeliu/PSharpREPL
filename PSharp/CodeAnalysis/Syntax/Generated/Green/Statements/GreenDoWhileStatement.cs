using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenDoWhileStatement : GreenStatement
    {
        public GreenToken DoKeyword { get; }
        public GreenToken ColonToken { get; }
        public GreenBlockStatement Body { get; }
        public GreenExpression Condition { get; }

        public override int SlotCount => 4;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => DoKeyword,
            1 => ColonToken,
            2 => Body,
            3 => Condition,
            _ => null
        };

        public GreenDoWhileStatement(
            SyntaxKind kind,
            GreenToken doKeyword,
            GreenToken colonToken,
            GreenBlockStatement body,
            GreenExpression condition
        )
            : base(kind)
        {
            DoKeyword = doKeyword;
            ColonToken = colonToken;
            Body = body;
            Condition = condition;
        }

        public override SyntaxKind Kind => SyntaxKind.DoWhileStatement;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenDoWhileStatement(Kind, DoKeyword, ColonToken, Body, Condition);
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
