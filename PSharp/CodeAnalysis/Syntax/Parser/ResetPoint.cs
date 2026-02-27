using PSharp.CodeAnalysis.Syntax.Green;

namespace PSharp.CodeAnalysis.Syntax.Parser;

/// <summary>
/// Represents a saved parser state for backtracking (reset point).
/// </summary>
internal struct ResetPoint
{
    public readonly int ResetCount;
    public readonly LexerMode Mode;
    public readonly int Position;           // Absolute token position (firstToken + offset)
    public readonly SyntaxNode PrevTokenTrailingTrivia;

    public ResetPoint(int resetCount, LexerMode mode, int position, SyntaxNode prevTokenTrailingTrivia)
    {
        ResetCount = resetCount;
        Mode = mode;
        Position = position;
        PrevTokenTrailingTrivia = prevTokenTrailingTrivia;
    }
}

internal struct GreenResetPoint
{
    public readonly int ResetCount;
    public readonly LexerMode Mode;
    public readonly int Position;           // Absolute token position (firstToken + offset)
    public readonly GreenNode PrevTokenTrailingTrivia;

    public GreenResetPoint(int resetCount, LexerMode mode, int position, GreenNode prevTokenTrailingTrivia)
    {
        ResetCount = resetCount;
        Mode = mode;
        Position = position;
        PrevTokenTrailingTrivia = prevTokenTrailingTrivia;
    }
}