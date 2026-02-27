using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenElseClause : GreenStatement
    {
        public GreenToken ElseKeyword { get; }
        public GreenToken ElseColonToken { get; }
        public GreenStatement ElseStatement { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => ElseKeyword,
            1 => ElseColonToken,
            2 => ElseStatement,
            _ => null
        };

        public GreenElseClause(
            SyntaxKind kind,
            GreenToken elseKeyword,
            GreenToken elseColonToken,
            GreenStatement elseStatement
        )
            : base(kind)
        {
            ElseKeyword = elseKeyword;
            ElseColonToken = elseColonToken;
            ElseStatement = elseStatement;
        }

        public override SyntaxKind Kind => SyntaxKind.ElseClause;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenElseClause(Kind, ElseKeyword, ElseColonToken, ElseStatement);
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
