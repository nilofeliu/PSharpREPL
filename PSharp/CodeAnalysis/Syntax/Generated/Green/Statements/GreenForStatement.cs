using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Green.Statements
{
    internal sealed class GreenForStatement : GreenStatement, ILoopStatement
    {
        public GreenToken Keyword { get; }
        public GreenToken Identifier { get; }
        public GreenToken EqualsToken { get; }
        public GreenExpression LowerBound { get; }
        public GreenToken ToKeyword { get; }
        public GreenExpression UpperBound { get; }
        public GreenToken ColonToken { get; }
        public GreenStatement Body { get; }
        public GreenToken EndToken { get; }

        public override int SlotCount => 9;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Keyword,
            1 => Identifier,
            2 => EqualsToken,
            3 => LowerBound,
            4 => ToKeyword,
            5 => UpperBound,
            6 => ColonToken,
            7 => Body,
            8 => EndToken,
            _ => null
        };

        public GreenForStatement(
            SyntaxKind kind,
            GreenToken keyword,
            GreenToken identifier,
            GreenToken equalsToken,
            GreenExpression lowerBound,
            GreenToken toKeyword,
            GreenExpression upperBound,
            GreenToken colonToken,
            GreenStatement body,
            GreenToken endToken
        )
            : base(kind)
        {
            Keyword = keyword;
            Identifier = identifier;
            EqualsToken = equalsToken;
            LowerBound = lowerBound;
            ToKeyword = toKeyword;
            UpperBound = upperBound;
            ColonToken = colonToken;
            Body = body;
            EndToken = endToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ForStatement;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenForStatement(Kind, Keyword, Identifier, EqualsToken, LowerBound, ToKeyword, UpperBound, ColonToken, Body, EndToken);
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
