using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.InternalSyntax
{
    public sealed class SyntaxTrivia
    {
        public SyntaxKind Kind { get; }
        public string Text { get; }
        public int Width => Text.Length;

        public SyntaxTrivia(SyntaxKind kind, string text)
        {
            Kind = kind;
            Text = text;
        }

        public override string ToString() => Text;
    }
}