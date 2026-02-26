using PSharp.CodeAnalysis.Syntax.Green;

namespace PSharp.CodeAnalysis.Syntax.Nodes
{
    public abstract class StatementSyntax : SyntaxNode
    {
        protected StatementSyntax()
        {
        }
        protected StatementSyntax(SyntaxNode? parent, GreenNode green, int position) : base(parent, green, position)
        {
        }
    }

}
