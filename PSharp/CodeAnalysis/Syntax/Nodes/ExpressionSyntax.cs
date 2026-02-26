using PSharp.CodeAnalysis.Syntax.Green;

namespace PSharp.CodeAnalysis.Syntax.Nodes
{
    public abstract class ExpressionSyntax : SyntaxNode
    {
        protected ExpressionSyntax()
        {
        }
        protected ExpressionSyntax(SyntaxNode? parent, GreenNode green, int position) : base(parent, green, position)
        {
        }
    }

}