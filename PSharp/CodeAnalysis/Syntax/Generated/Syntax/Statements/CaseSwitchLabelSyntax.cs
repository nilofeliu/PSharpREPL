using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements
{
    public sealed class CaseSwitchLabelSyntax : StatementSyntax
    {
        private readonly GreenCaseSwitchLabel _green;

        private ExpressionSyntax? _expression;
        private StatementSyntax? _body;

        internal CaseSwitchLabelSyntax(GreenCaseSwitchLabel green, SyntaxNode? parent, int position)
            : base(parent, green, position)
        {
            _green = green;
        }

        public override SyntaxKind Kind => SyntaxKind.CaseSwitchLabel;

        public SyntaxToken CaseKeyword
            => new SyntaxToken(_green.CaseKeyword, this, GetChildPosition(0));

        public ExpressionSyntax? Expression
        {
            get
            {
                if (_expression == null)
                    _expression = (ExpressionSyntax)RedNodeFactory.CreateRed(_green.Expression, this, GetChildPosition(1));
                return _expression;
            }
        }

        public SyntaxToken CaseColonToken
            => new SyntaxToken(_green.CaseColonToken, this, GetChildPosition(2));

        public StatementSyntax? Body
        {
            get
            {
                if (_body == null)
                    _body = (StatementSyntax)RedNodeFactory.CreateRed(_green.Body, this, GetChildPosition(3));
                return _body;
            }
        }

    }
}
