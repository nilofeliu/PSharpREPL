using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.Kind;

using PSharp.CodeAnalysis.Syntax.Green;

namespace PSharp.CodeAnalysis.Syntax.Nodes
{
    public sealed class CompilationUnitSyntax : SyntaxNode
    {
        private readonly GreenCompilationUnit _green;
        private StatementSyntax? _statement;

        internal CompilationUnitSyntax(GreenCompilationUnit green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.CompilationUnit;

        public StatementSyntax Statement
        {
            get
            {
                if (_statement == null)
                    _statement = (StatementSyntax)RedNodeFactory.CreateRed(_green.Statement, this, GetChildPosition(0));
                return _statement;
            }
        }

        public SyntaxToken EndOfFileToken
            => new SyntaxToken(_green.EndOfFileToken, this, GetChildPosition(1));
    }
}

