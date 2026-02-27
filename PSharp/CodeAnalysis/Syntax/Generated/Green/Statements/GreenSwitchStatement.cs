using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenSwitchStatement : GreenStatement
    {
        public GreenToken SwitchKeyword { get; }
        public GreenExpression Pattern { get; }
        public GreenToken ColonToken { get; }
        public GreenNodeList? Cases { get; }
        public GreenDefaultSwitchLabel? DefaultCase { get; }
        public GreenToken EndToken { get; }

        public override int SlotCount => 6;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => SwitchKeyword,
            1 => Pattern,
            2 => ColonToken,
            3 => Cases,
            4 => DefaultCase,
            5 => EndToken,
            _ => null
        };

        public GreenSwitchStatement(
            SyntaxKind kind,
            GreenToken switchKeyword,
            GreenExpression pattern,
            GreenToken colonToken,
            GreenNodeList? cases,
            GreenDefaultSwitchLabel? defaultCase,
            GreenToken endToken
        )
            : base(kind)
        {
            SwitchKeyword = switchKeyword;
            Pattern = pattern;
            ColonToken = colonToken;
            Cases = cases;
            DefaultCase = defaultCase;
            EndToken = endToken;
        }

        public override SyntaxKind Kind => SyntaxKind.SwitchStatement;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenSwitchStatement(Kind, SwitchKeyword, Pattern, ColonToken, Cases, DefaultCase, EndToken);
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
