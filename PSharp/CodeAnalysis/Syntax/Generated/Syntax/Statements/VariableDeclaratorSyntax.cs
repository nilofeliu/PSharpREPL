using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class VariableDeclaratorSyntax : StatementSyntax
    {
        private readonly GreenVariableDeclarator _green;

        private EqualsValueClauseSyntax? _initializer;

        internal VariableDeclaratorSyntax(GreenVariableDeclarator green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.VariableDeclarator;

        public SyntaxToken Identifier
            => new SyntaxToken(_green.Identifier, this, GetChildPosition(0));

        public EqualsValueClauseSyntax? Initializer
        {
            get
            {
                if (_initializer == null)
                    _initializer = (EqualsValueClauseSyntax)RedNodeFactory.CreateRed(_green.Initializer, this, GetChildPosition(1));
                return _initializer;
            }
        }

    }
}
