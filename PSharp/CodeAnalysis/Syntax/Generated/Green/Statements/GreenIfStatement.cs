using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenIfStatement : GreenStatement
    {
        public GreenToken IfKeyword { get; }
        public GreenExpression Condition { get; }
        public GreenToken ColonToken { get; }
        public GreenStatement ThenStatement { get; }
        public GreenElseClause? ElseClause { get; }
        public GreenToken? EndKeyword { get; }

        public override int SlotCount => 6;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => IfKeyword,
            1 => Condition,
            2 => ColonToken,
            3 => ThenStatement,
            4 => ElseClause,
            5 => EndKeyword,
            _ => null
        };

        public GreenIfStatement(
            SyntaxKind kind,
            GreenToken ifKeyword,
            GreenExpression condition,
            GreenToken colonToken,
            GreenStatement thenStatement,
            GreenElseClause? elseClause,
            GreenToken? endKeyword
        )
            : base(kind)
        {
            IfKeyword = ifKeyword;
            Condition = condition;
            ColonToken = colonToken;
            ThenStatement = thenStatement;
            ElseClause = elseClause;
            EndKeyword = endKeyword;
        }

        public override SyntaxKind Kind => SyntaxKind.IfStatement;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenIfStatement(Kind, IfKeyword, Condition, ColonToken, ThenStatement, ElseClause, EndKeyword);
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
