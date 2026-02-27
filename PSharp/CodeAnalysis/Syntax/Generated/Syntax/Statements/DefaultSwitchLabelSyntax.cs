using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class DefaultSwitchLabelSyntax : StatementSyntax
    {
        private readonly GreenDefaultSwitchLabel _green;

        private StatementSyntax? _body;

        internal DefaultSwitchLabelSyntax(GreenDefaultSwitchLabel green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.DefaultSwitchLabel;

        public SyntaxToken DefaultKeyword
            => new SyntaxToken(_green.DefaultKeyword, this, GetChildPosition(0));

        public SyntaxToken CaseColonToken
            => new SyntaxToken(_green.CaseColonToken, this, GetChildPosition(1));

        public StatementSyntax? Body
        {
            get
            {
                if (_body == null)
                    _body = (StatementSyntax)RedNodeFactory.CreateRed(_green.Body, this, GetChildPosition(2));
                return _body;
            }
        }

    }
}
