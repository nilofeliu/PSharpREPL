using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenBlockStatement : GreenStatement
    {
        public GreenToken OpenBraceToken { get; }
        public GreenNodeList Statements { get; }
        public GreenToken ClosedBraceToken { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => OpenBraceToken,
            1 => Statements,
            2 => ClosedBraceToken,
            _ => null
        };

        public GreenBlockStatement(
            SyntaxKind kind,
            GreenToken openBraceToken,
            GreenNodeList statements,
            GreenToken closedBraceToken
        )
            : base(kind)
        {
            OpenBraceToken = openBraceToken;
            Statements = statements;
            ClosedBraceToken = closedBraceToken;
        }

        public override SyntaxKind Kind => SyntaxKind.Block;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenBlockStatement(Kind, OpenBraceToken, Statements, ClosedBraceToken);
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
