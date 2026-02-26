using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Text;
using System.IO;

namespace PSharp.CodeAnalysis;

public sealed class SyntaxToken : SyntaxNode
{
    internal GreenToken Green { get; }
    public SyntaxNode? Parent { get; }
    public int Position { get; }

    internal SyntaxToken(GreenToken green, SyntaxNode? parent, int position)
    {
        Green = green;
        Parent = parent;
        Position = position;
    }

    public override SyntaxKind Kind => Green.Kind;

    // ── Spans ────────────────────────────────────────────────────────────────

    public override TextSpan Span =>
        new TextSpan(Position + Green.LeadingWidth, Green.Width);

    public TextSpan FullSpan =>
        new TextSpan(Position, Green.FullWidth);

    /// <summary>Slight perf shortcut over Span.Start</summary>
    public int SpanStart => Position + Green.LeadingWidth;

    internal int EndPosition => Position + Green.FullWidth;

    // ── Text & Value ─────────────────────────────────────────────────────────

    public string Text => Green.Text;

    public object Value => Green.Value;

    /// <summary>String representation of the value, falls back to Text.</summary>
    public string ValueText => Green.Value?.ToString() ?? Green.Text ?? string.Empty;

    public override string ToString() => Green.Text ?? string.Empty;

    public string ToFullString() => Green.ToFullString();

    // ── Widths ───────────────────────────────────────────────────────────────

    internal int Width => Green.Width;
    internal int FullWidth => Green.FullWidth;
    internal int LeadingWidth => Green.LeadingWidth;
    internal int TrailingWidth => Green.TrailingWidth;

    // ── Trivia ───────────────────────────────────────────────────────────────

    public SyntaxTrivia[] LeadingTrivia => Green.GetLeadingTriviaArray();
    public SyntaxTrivia[] TrailingTrivia => Green.GetTrailingTriviaArray();

    public bool HasLeadingTrivia => LeadingTrivia.Length > 0;
    public bool HasTrailingTrivia => TrailingTrivia.Length > 0;

    public IEnumerable<SyntaxTrivia> GetAllTrivia()
    {
        if (HasLeadingTrivia)
        {
            foreach (var t in LeadingTrivia) yield return t;
        }
        if (HasTrailingTrivia)
        {
            foreach (var t in TrailingTrivia) yield return t;
        }
    }

    // ── With-trivia helpers ──────────────────────────────────────────────────

    public SyntaxToken WithLeadingTrivia(params SyntaxTrivia[] trivia)
    {
        var newGreen = Green.WithLeadingTrivia(trivia);
        return new SyntaxToken(newGreen, Parent, Position);
    }

    public SyntaxToken WithTrailingTrivia(params SyntaxTrivia[] trivia)
    {
        var newGreen = Green.WithTrailingTrivia(trivia);
        return new SyntaxToken(newGreen, Parent, Position);
    }

    public SyntaxToken WithTriviaFrom(SyntaxToken token)
        => WithLeadingTrivia(token.LeadingTrivia).WithTrailingTrivia(token.TrailingTrivia);

    // ── Flags ────────────────────────────────────────────────────────────────

    public bool IsMissing => Green.IsMissing;

    public bool ContainsDiagnostics => Green.ContainsDiagnostics;

    // ── Diagnostics ──────────────────────────────────────────────────────────

    public IEnumerable<Diagnostic> GetDiagnostics()
    {
        if (!Green.ContainsDiagnostics || Green.Diagnostics == null)
            yield break;
        foreach (var info in Green.Diagnostics)
            yield return new Diagnostic(info.OverrideSpan ?? Span, info.Message, info.Code, info.Severity);
    }

    public SyntaxToken WithDiagnostics(params DiagnosticInfo[] diagnostics)
    {
        var newGreen = (GreenToken)Green.WithDiagnostics(diagnostics);
        return new SyntaxToken(newGreen, Parent, Position);
    }

    // ── Equivalence ──────────────────────────────────────────────────────────

    public bool IsEquivalentTo(SyntaxToken other)
        => Green == other.Green ||
           Green != null && other.Green != null && Green.IsEquivalentTo(other.Green);

    // ── Equality ─────────────────────────────────────────────────────────────

    //public static bool operator ==(SyntaxToken left, SyntaxToken right)
    //{
    //    if (left is null) return right is null;
    //    return left.Equals(right);
    //}
    //public static bool operator !=(SyntaxToken left, SyntaxToken right) => !(left == right);

    //public bool Equals(SyntaxToken other)
    //    => ReferenceEquals(Green, other.Green) &&
    //       ReferenceEquals(Parent, other.Parent) &&
    //       Position == other.Position;

    //public override bool Equals(object? obj)
    //    => obj is SyntaxToken other && Equals(other);

    //public override int GetHashCode()
    //    => HashCode.Combine(Green, Parent, Position);
}


//public sealed class SyntaxToken : SyntaxNode
//{
//    internal GreenToken Green { get; }

//    public SyntaxNode? Parent { get; }
//    public int Position { get; }

//    internal SyntaxToken(GreenToken green, SyntaxNode? parent, int position)
//    {
//        Green = green;
//        Parent = parent;
//        Position = position;
//    }

//    public override SyntaxKind Kind => Green.Kind;

//    // Span of the token itself — no trivia
//    public override TextSpan Span =>
//        new TextSpan(Position + Green.LeadingWidth, Green.Width);

//    // Full span including surrounding trivia
//    public TextSpan FullSpan =>
//        new TextSpan(Position, Green.FullWidth);

//    public string Text => Green.Text;
//    public object Value => Green.Value;
//    public SyntaxTrivia[] LeadingTrivia => Green.GetLeadingTriviaArray();
//    public SyntaxTrivia[] TrailingTrivia => Green.GetTrailingTriviaArray();


//    // IsMissing is an explicit flag set at construction time on GreenToken — never derived
//    public bool IsMissing => Green.IsMissing;

//    public IEnumerable<Diagnostic> GetDiagnostics()
//    {
//        if (!Green.ContainsDiagnostics || Green.Diagnostics == null)
//            yield break;

//        foreach (var info in Green.Diagnostics)
//            yield return new Diagnostic(info.OverrideSpan ?? Span, info.Message, info.Code, info.Severity);
//    }

//    public SyntaxToken WithDiagnostics(params DiagnosticInfo[] diagnostics)
//    {
//        var newGreen = (GreenToken)Green.WithDiagnostics(diagnostics);
//        return new SyntaxToken(newGreen, Parent, Position);
//    }
//}