using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.InternalSyntax;

internal sealed class GreenTriviaList : GreenNode
{
    internal readonly SyntaxTrivia[] TriviaArray;   // accessible to GreenToken

    public GreenTriviaList(SyntaxTrivia[] trivia, SyntaxKind kind = SyntaxKind.TriviaList)
            : base(kind)
    {
        TriviaArray = trivia ?? Array.Empty<SyntaxTrivia>();
        FullWidth = TriviaArray.Sum(t => t.Width);
    }

    public override int SlotCount => TriviaArray.Length;

    public override GreenNode? GetSlot(int index)
    {
        // If you eventually make SyntaxTrivia a GreenNode, you can return it here.
        // For now, return null.
        return null;
    }

    protected override GreenNode CreateWithDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        // Diagnostics on trivia lists are rare; just return this.
        return this;
    }

    public override string ToFullString()
    {
        return string.Concat(TriviaArray.Select(t => t.Text));
    }
}