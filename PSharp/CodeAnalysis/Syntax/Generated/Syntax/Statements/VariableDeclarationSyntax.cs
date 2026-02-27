using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class VariableDeclarationSyntax : StatementSyntax
    {
        private readonly GreenVariableDeclaration _green;

        internal VariableDeclarationSyntax(GreenVariableDeclaration green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.VariableDeclaration;

        public SyntaxToken Keyword
            => new SyntaxToken(_green.Keyword, this, GetChildPosition(0));

        public SyntaxToken? Type
            => new SyntaxToken(_green.Type, this, GetChildPosition(1));

        public List<VariableDeclaratorSyntax> Variables
        {
            get
            {
                var list = new List<VariableDeclaratorSyntax>();
                int pos = GetChildPosition(2);
                foreach (var child in _green.Variables)
                {
                    list.Add((VariableDeclaratorSyntax)RedNodeFactory.CreateRed(child, this, pos));
                    pos += child.FullWidth;
                }
                return list;
            }
        }

    }
}
