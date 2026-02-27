using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class LocalDeclarationStatementSyntax : StatementSyntax
    {
        private readonly GreenLocalDeclarationStatement _green;

        private VariableDeclarationSyntax _declaration;

        internal LocalDeclarationStatementSyntax(GreenLocalDeclarationStatement green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.LocalDeclarationStatement;

        public SyntaxToken Keyword
            => new SyntaxToken(_green.Keyword, this, GetChildPosition(0));

        public VariableDeclarationSyntax Declaration
        {
            get
            {
                if (_declaration == null)
                    _declaration = (VariableDeclarationSyntax)RedNodeFactory.CreateRed(_green.Declaration, this, GetChildPosition(1));
                return _declaration;
            }
        }

    }
}
