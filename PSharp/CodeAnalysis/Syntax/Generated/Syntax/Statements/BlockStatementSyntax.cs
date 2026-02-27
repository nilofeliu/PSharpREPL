using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class BlockStatementSyntax : StatementSyntax
    {
        private readonly GreenBlockStatement _green;

        internal BlockStatementSyntax(GreenBlockStatement green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.Block;

        public SyntaxToken OpenBraceToken
            => new SyntaxToken(_green.OpenBraceToken, this, GetChildPosition(0));

        public List<StatementSyntax> Statements
        {
            get
            {
                var list = new List<StatementSyntax>();
                int pos = GetChildPosition(1);
                foreach (var child in _green.Statements)
                {
                    list.Add((StatementSyntax)RedNodeFactory.CreateRed(child, this, pos));
                    pos += child.FullWidth;
                }
                return list;
            }
        }

        public SyntaxToken ClosedBraceToken
            => new SyntaxToken(_green.ClosedBraceToken, this, GetChildPosition(2));

    }
}
