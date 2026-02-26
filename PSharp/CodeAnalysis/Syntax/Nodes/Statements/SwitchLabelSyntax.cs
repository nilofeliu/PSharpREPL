using PSharp.CodeAnalysis;

namespace PSharp.CodeAnalysis.Syntax.Nodes.Statements;

public abstract class SwitchLabelSyntax : StatementSyntax
    {
        public abstract SyntaxToken Keyword { get; }
        public abstract SyntaxToken ColonToken { get; }
        public abstract StatementSyntax? Body { get; }
    }
