using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class SwitchStatementSyntax : StatementSyntax
    {
        private readonly GreenSwitchStatement _green;

        private ExpressionSyntax _pattern;
        private DefaultSwitchLabelSyntax? _defaultCase;

        internal SwitchStatementSyntax(GreenSwitchStatement green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.SwitchStatement;

        public SyntaxToken SwitchKeyword
            => new SyntaxToken(_green.SwitchKeyword, this, GetChildPosition(0));

        public ExpressionSyntax Pattern
        {
            get
            {
                if (_pattern == null)
                    _pattern = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Pattern, this, GetChildPosition(1));
                return _pattern;
            }
        }

        public SyntaxToken ColonToken
            => new SyntaxToken(_green.ColonToken, this, GetChildPosition(2));

        public List<CaseSwitchLabelSyntax> Cases
        {
            get
            {
                var list = new List<CaseSwitchLabelSyntax>();
                int pos = GetChildPosition(3);
                foreach (var child in _green.Cases)
                {
                    list.Add((CaseSwitchLabelSyntax)RedNodeFactory.CreateRed(child, this, pos));
                    pos += child.FullWidth;
                }
                return list;
            }
        }

        public DefaultSwitchLabelSyntax? DefaultCase
        {
            get
            {
                if (_defaultCase == null)
                    _defaultCase = (DefaultSwitchLabelSyntax)RedNodeFactory.CreateRed(_green.DefaultCase, this, GetChildPosition(4));
                return _defaultCase;
            }
        }

        public SyntaxToken EndToken
            => new SyntaxToken(_green.EndToken, this, GetChildPosition(5));

    }
}
