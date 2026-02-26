using PSharp.CodeAnalysis.Text;

namespace PSharp.CodeAnalysis
{
    public readonly struct Location
    {
        public TextSpan Span { get; }
        public Location(TextSpan span) => Span = span;
    }
}