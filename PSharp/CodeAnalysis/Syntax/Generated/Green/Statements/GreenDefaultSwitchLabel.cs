using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenDefaultSwitchLabel : GreenStatement, ISwitchLabel
    {
        public GreenToken DefaultKeyword { get; }
        public GreenToken CaseColonToken { get; }
        public GreenStatement? Body { get; }

        public override int SlotCount => 3;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => DefaultKeyword,
            1 => CaseColonToken,
            2 => Body,
            _ => null
        };

        public GreenDefaultSwitchLabel(
            SyntaxKind kind,
            GreenToken defaultKeyword,
            GreenToken caseColonToken,
            GreenStatement? body
        )
            : base(kind)
        {
            DefaultKeyword = defaultKeyword;
            CaseColonToken = caseColonToken;
            Body = body;
        }

        public override SyntaxKind Kind => SyntaxKind.DefaultSwitchLabel;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenDefaultSwitchLabel(Kind, DefaultKeyword, CaseColonToken, Body);
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
