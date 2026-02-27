using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenCaseSwitchLabel : GreenStatement
    {
        public GreenToken CaseKeyword { get; }
        public GreenExpression? Expression { get; }
        public GreenToken CaseColonToken { get; }
        public GreenStatement? Body { get; }

        public override int SlotCount => 4;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => CaseKeyword,
            1 => Expression,
            2 => CaseColonToken,
            3 => Body,
            _ => null
        };

        public GreenCaseSwitchLabel(
            SyntaxKind kind,
            GreenToken caseKeyword,
            GreenExpression? expression,
            GreenToken caseColonToken,
            GreenStatement? body
        )
            : base(kind)
        {
            CaseKeyword = caseKeyword;
            Expression = expression;
            CaseColonToken = caseColonToken;
            Body = body;
        }

        public override SyntaxKind Kind => SyntaxKind.CaseSwitchLabel;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenCaseSwitchLabel(Kind, CaseKeyword, Expression, CaseColonToken, Body);
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
