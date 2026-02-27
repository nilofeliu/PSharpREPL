using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenWhileStatement : GreenStatement, ILoopStatement
    {
        public GreenToken WhileKeyword { get; }
        public GreenExpression Condition { get; }
        public GreenToken ColonToken { get; }
        public GreenStatement Body { get; }
        public GreenToken EndKeyword { get; }

        public override int SlotCount => 5;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => WhileKeyword,
            1 => Condition,
            2 => ColonToken,
            3 => Body,
            4 => EndKeyword,
            _ => null
        };

        public GreenWhileStatement(
            SyntaxKind kind,
            GreenToken whileKeyword,
            GreenExpression condition,
            GreenToken colonToken,
            GreenStatement body,
            GreenToken endKeyword
        )
            : base(kind)
        {
            WhileKeyword = whileKeyword;
            Condition = condition;
            ColonToken = colonToken;
            Body = body;
            EndKeyword = endKeyword;
        }

        public override SyntaxKind Kind => SyntaxKind.WhileStatement;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenWhileStatement(Kind, WhileKeyword, Condition, ColonToken, Body, EndKeyword);
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
