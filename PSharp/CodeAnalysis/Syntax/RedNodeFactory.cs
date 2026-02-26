using PSharp.CodeAnalysis.Syntax.Green;


namespace PSharp.CodeAnalysis.Syntax
{
    internal static partial class RedNodeFactory
    {
        public static SyntaxNode CreateRed(GreenNode green, SyntaxNode? parent, int position)
            => throw new NotImplementedException($"SyntaxFactory not yet generated for kind: {green.Kind}");
    }
}
