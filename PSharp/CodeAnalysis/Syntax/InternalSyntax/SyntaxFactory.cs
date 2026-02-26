using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.InternalSyntax;

/// <summary>
/// Factory methods for creating green syntax nodes and tokens.
/// </summary>
public static class SyntaxFactory
{
    // ----- Generic token creation -----

    /// <summary>
    /// Creates a green token with the specified kind, text, and optional value.
    /// </summary>
    public static GreenToken Token(
        SyntaxKind kind,
        string text,
        object? value = null,
        SyntaxTrivia[]? leadingTrivia = null,
        SyntaxTrivia[]? trailingTrivia = null,
        bool isMissing = false,
        DiagnosticInfo[]? diagnostics = null,
        int position = 0,
        int index = -1)
    {
        return new GreenToken(kind, text, value, leadingTrivia, trailingTrivia, isMissing, diagnostics, position, index);
    }

    /// <summary>
    /// Creates a missing token of the specified kind (empty text, isMissing = true).
    /// </summary>
    public static GreenToken MissingToken(
        SyntaxKind kind,
        SyntaxTrivia[]? leadingTrivia = null,
        SyntaxTrivia[]? trailingTrivia = null,
        DiagnosticInfo[]? diagnostics = null,
        int position = 0,
        int index = -1)
    {
        return new GreenToken(kind, string.Empty, null, leadingTrivia, trailingTrivia, isMissing: true, diagnostics, position, index);
    }

    // ----- Common token kinds -----

    public static GreenToken Identifier(
        string text,
        object? value = null,
        SyntaxTrivia[]? leadingTrivia = null,
        SyntaxTrivia[]? trailingTrivia = null,
        bool isMissing = false,
        DiagnosticInfo[]? diagnostics = null,
        int position = 0,
        int index = -1)
    {
        return Token(SyntaxKind.IdentifierToken, text, value, leadingTrivia, trailingTrivia, isMissing, diagnostics, position, index);
    }

    public static GreenToken NumericLiteral(
        string text,
        object value,
        SyntaxTrivia[]? leadingTrivia = null,
        SyntaxTrivia[]? trailingTrivia = null,
        bool isMissing = false,
        DiagnosticInfo[]? diagnostics = null,
        int position = 0,
        int index = -1)
    {
        // The kind will be determined by the lexer; here we assume a generic numeric token,
        // but you can add specific ones (Int32LiteralToken, etc.) as needed.
        return Token(SyntaxKind.NumericLiteralToken, text, value, leadingTrivia, trailingTrivia, isMissing, diagnostics, position, index);
    }

    public static GreenToken StringLiteral(
        string text,
        object value,
        SyntaxTrivia[]? leadingTrivia = null,
        SyntaxTrivia[]? trailingTrivia = null,
        bool isMissing = false,
        DiagnosticInfo[]? diagnostics = null,
        int position = 0,
        int index = -1)
    {
        return Token(SyntaxKind.StringLiteralToken, text, value, leadingTrivia, trailingTrivia, isMissing, diagnostics, position, index);
    }

    // Add other token kinds as needed (keywords, operators, etc.)

    // ----- Trivia list creation (internal use) -----

    /// <summary>
    /// Creates a green trivia list from an array of syntax trivia.
    /// </summary>
    internal static GreenTriviaList TriviaList(SyntaxTrivia[] trivia)
    {
        return new GreenTriviaList(trivia);
    }

    // ----- Future: syntax node factories -----
    // When you add green syntax nodes for expressions, statements, etc.,
    // you can extend this class with corresponding factory methods.
}
