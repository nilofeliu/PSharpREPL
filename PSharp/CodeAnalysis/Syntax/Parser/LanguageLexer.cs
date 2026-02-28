using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Text;
using System.Globalization;
using System.Text;

namespace PSharp.CodeAnalysis.Syntax.Parser;

internal sealed class Lexer : AbstractLexer
{
    private SyntaxKind _kind;
    private object _value;

    private readonly List<DiagnosticInfo> _pendingDiagnostics = new();

    public LexerMode Mode { get; set; } = LexerMode.Normal;

    internal Lexer(SourceText text) : base(text)
    {
    }

    private char Current => PeekChar(0);
    private char Lookahead => PeekChar(1);

    //public GreenToken Lex(LexerMode mode)
    //{
    //    Mode = mode;
    //    return Lex(); // calls the parameterless version which now returns GreenToken
    //}

    public GreenToken Lex()
    {
        var leadingTrivia = ScanSyntaxTrivia(isTrailing: false);
        StartLexeme();
        _kind = SyntaxKind.BadToken;
        _value = null;

        switch (Mode)
        {
            case LexerMode.Normal:
                ScanSyntaxToken();
                break;
            // future cases for other modes
            default:
                ScanSyntaxToken();
                break;
        }

        var text = GetText();
        var trailingTrivia = ScanSyntaxTrivia(isTrailing: true);
        var diagnostics = _pendingDiagnostics.Count > 0 ?
            _pendingDiagnostics.ToArray() : null;
        _pendingDiagnostics.Clear();

        var green =  GreenNodeFactory.Token(_kind, text, _value, leadingTrivia, trailingTrivia, diagnostics: diagnostics);

       // var red = new SyntaxToken(green, null, TextWindow.LexemeStartPosition);
        return green;
    }

    private void ScanTokenInMode()
    {
        switch (Mode)
        {
            case LexerMode.InterpolatedString:
                // TODO: implement interpolated string scanning
                break;
            case LexerMode.VerbatimString:
                // TODO: implement verbatim string scanning
                break;
            default:
                // fallback to normal (should not happen)
                ScanSyntaxToken();
                break;
        }
    }



    //private SyntaxTrivia[] ScanSyntaxTrivia(bool isTrailing)
    //{
    //    var trivia = new List<SyntaxTrivia>();
    //    while (true)
    //    {
    //        var start = TextWindow.Position;
    //        if (Current == ' ')
    //        {
    //            while (Current == ' ')
    //                AdvanceChar();
    //            var text = TextWindow.GetText(start, TextWindow.Position - start);
    //            trivia.Add(new SyntaxTrivia(SyntaxKind.WhiteSpaceTrivia, text));
    //        }
    //        else if (Current == '\t')
    //        {
    //            while (Current == '\t')
    //                AdvanceChar();
    //            var text = TextWindow.GetText(start, TextWindow.Position - start);
    //            trivia.Add(new SyntaxTrivia(SyntaxKind.TabTrivia, text));
    //        }
    //        else if (Current == '\r' || Current == '\n')
    //        {
    //            if (Current == '\r' && Lookahead == '\n')
    //                AdvanceChar(2);
    //            else
    //                AdvanceChar();
    //            var text = TextWindow.GetText(start, TextWindow.Position - start);
    //            trivia.Add(new SyntaxTrivia(SyntaxKind.NewLineTrivia, text));
    //            if (isTrailing)
    //                break;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    return trivia.ToArray();
    //}

    private SyntaxTrivia[] ScanSyntaxTrivia(bool isTrailing)
    {
        var builder = new List<SyntaxTrivia>();
        while (true)
        {
            var start = TextWindow.Position;
            var kind = ScanSingleTrivia(); // returns SyntaxKind.WhitespaceTrivia, etc., or SyntaxKind.None if no trivia
            if (kind == SyntaxKind.None)
                break;
            var text = TextWindow.GetText(start, TextWindow.Position - start);
            builder.Add(new SyntaxTrivia(kind, text));
            if (isTrailing && kind == SyntaxKind.NewLineTrivia)
                break; // trailing trivia stops at first newline
        }
        return builder.ToArray();
    }

    private void ScanSyntaxToken()
    {
        switch (true)
        {
            case true when LexEndOfFile():
            case true when LexNumber():
            case true when LexStringToken():
            case true when LexIdentifierOrKeyword():
            case true when LexOperatorOrToken():
                break;
            default:
                _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_BadCharacter, DiagnosticSeverity.Error, Current));
                _value = Current.ToString();  // ← add this line (optional but useful)
                AdvanceChar();
                break;
        }
    }

    private bool LexEndOfFile()
    {
        if (Current == '\0')
        {
            _kind = SyntaxKind.EndOfFileToken;
            _value = null;  // ← add this line
            return true;
        }
        return false;
    }

    private bool LexStringToken()
    {
        if (Current == '"')
        {
            ScanStringLiteral();
            return true;
        }
        return false;
    }

    private bool LexNumber()
    {
        if (char.IsDigit(Current))
        {
            ScanNumericLiteral();
            return true;
        }
        return false;
    }

    private bool LexIdentifierOrKeyword()
    {
        if (char.IsLetter(Current) || Current == '_')
        {
            ScanIdentifierOrKeyword();
            return true;
        }
        return false;
    }

    private bool LexOperatorOrToken()
    {

        var twoChar = Current.ToString() + Lookahead.ToString();
        if (SyntaxFacts.GetTokenIndex().TryGetValue(twoChar, out var kind))
            return ScanTwoCharOperator(kind);

        var oneChar = Current.ToString();
        if (SyntaxFacts.GetTokenIndex().TryGetValue(oneChar, out kind))
            return ScanSingleCharOperator(kind);

        return false;
    }

    private bool ScanSingleCharOperator(SyntaxKind kind)
    {
        _kind = kind;
        AdvanceChar();
        return true;
    }

    private bool ScanTwoCharOperator(SyntaxKind kind)
    {
        _kind = kind;
        AdvanceChar(2);
        return true;
    }

    private SyntaxKind ScanSingleTrivia()
    {

        if (Current == ' ')
        {
            while (Current == ' ')
                AdvanceChar();
            return SyntaxKind.WhiteSpaceTrivia;
        }
        if (Current == '\t')
        {
            while (Current == '\t')
                AdvanceChar();
            return SyntaxKind.TabTrivia;
        }
        if (Current == '\r' || Current == '\n')
        {
            if (Current == '\r' && Lookahead == '\n')
                AdvanceChar(2);
            else
                AdvanceChar();
            return SyntaxKind.NewLineTrivia;
        }
        if (Current == '/' && Lookahead == '/')
        {
            // Single-line comment
            AdvanceChar(2); // consume "//"
            while (Current != '\0' && Current != '\r' && Current != '\n')
                AdvanceChar();
            // Do not consume newline; it will be scanned as separate trivia on next call.
            return SyntaxKind.SingleLineCommentTrivia;
        }
        if (Current == '/' && Lookahead == '*')
        {
            // Multi-line comment
            AdvanceChar(2); // consume "/*"
            while (true)
            {

                if (Current == '\0')
                {
                    // Unterminated comment
                    _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_UnterminatedComment, DiagnosticSeverity.Error));
                    break;
                }
                if (Current == '*' && Lookahead == '/')
                {
                    AdvanceChar(2); // consume "*/"
                    break;
                }
                AdvanceChar();
            }
            return SyntaxKind.MultiLineCommentTrivia;
        }
        return SyntaxKind.None;
    }
    private void ScanNumericLiteral()
    {
        int start = TextWindow.Position;

        // ---- 1. Scan integer part (with possible base prefix) ----
        bool isHex = false;
        bool isBinary = false;

        if (Current == '0' && (Lookahead == 'x' || Lookahead == 'X'))
        {
            isHex = true;
            AdvanceChar(2); // consume "0x"
            ScanHexDigits();
        }
        else if (Current == '0' && (Lookahead == 'b' || Lookahead == 'B'))
        {
            isBinary = true;
            AdvanceChar(2); // consume "0b"
            ScanBinaryDigits();
        }
        else
        {
            ScanDecimalDigits();
        }

        // ---- 2. Scan fractional part (if any) ----
        bool hasFractional = false;
        if (Current == '.' && Lookahead != '.' && Lookahead != '\0') // avoid range operator '..'
        {
            // If we already have a hex or binary number, a dot is not allowed (invalid)
            if (isHex || isBinary)
            {
                _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_InvalidNumber, DiagnosticSeverity.Error, "Fractional part not allowed in hex/binary literal"));
                // We still consume the dot and following digits to keep advancing, but treat as error.
            }
            hasFractional = true;
            AdvanceChar(); // consume '.'

            if (!char.IsDigit(Current))
            {
                _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_InvalidNumber, DiagnosticSeverity.Error, "Expected digits after decimal point"));
            }
            else
            {
                ScanDecimalDigits(); // digits after decimal (always decimal)
            }
        }

        // ---- 3. Scan exponent part (if any) ----
        bool hasExponent = false;
        if (!isHex && !isBinary && (Current == 'e' || Current == 'E'))
        {
            hasExponent = true;
            AdvanceChar(); // consume 'e' / 'E'

            if (Current == '+' || Current == '-')
                AdvanceChar();

            if (!char.IsDigit(Current))
            {
                _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_InvalidNumber, DiagnosticSeverity.Error, "Expected digits after exponent"));
            }
            else
            {
                ScanDecimalDigits();
            }
        }

        // ---- 4. Scan suffix (e.g., f, l, m, u, etc.) ----
        SyntaxKind suffixKind = ScanNumericSuffix();

        // ---- 5. Determine the intended kind based on syntax and suffix ----
        // If suffix is explicitly given, use it; otherwise infer from presence of fractional/exponent.
        SyntaxKind intendedKind;
        if (suffixKind != SyntaxKind.BadToken)
        {
            intendedKind = suffixKind;
        }
        else if (hasFractional || hasExponent)
        {
            // Default floating-point type (e.g., double)
            intendedKind = SyntaxKind.DoubleLiteralToken;
        }
        //else if (isHex)
        //{
        //    intendedKind = SyntaxKind.HexIntegerLiteralToken;
        //}
        else if (isBinary)
        {
            intendedKind = SyntaxKind.BinaryIntegerLiteralToken;
        }
        else
        {
            intendedKind = SyntaxKind.IntLiteralToken; // will be refined after parsing
        }

        // ---- 6. Extract the full lexeme and parse the value ----
        string text = TextWindow.GetText(start, TextWindow.Position - start);
        object value = ParseNumericValue(text, intendedKind);

        // If parsing failed, add a diagnostic and set a default value.
        if (value == null)
        {
            _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_InvalidNumber, DiagnosticSeverity.Error, text));
            value = 0; // fallback
                       // Keep the intended kind as is, or set to BadToken? Usually keep kind but value is 0.
        }

        // For integer literals without suffix, we may have parsed as int, but if it overflowed int,
        // ParseNumericValue will have returned a long (or null if both fail). We need to adjust the kind accordingly.
        if (intendedKind == SyntaxKind.IntLiteralToken && value is long)
        {
            _kind = SyntaxKind.LongLiteralToken;
        }
        else if (intendedKind == SyntaxKind.IntLiteralToken && value is int)
        {
            _kind = SyntaxKind.IntLiteralToken;
        }
        //else if (intendedKind == SyntaxKind.HexIntegerLiteralToken && value is long)
        //{
        //    _kind = SyntaxKind.HexLongLiteralToken; // define if needed
        //}
        // Similar for binary, etc. You may define separate kinds for hex/long if desired.
        else
        {
            _kind = intendedKind;
        }

        _value = value;
    }

    private void ScanDecimalDigits()
    {
        while (char.IsDigit(Current) || Current == '_')
        {
            if (Current == '_')
            {
                AdvanceChar(); // skip underscores, they are allowed as separators
                continue;
            }
            AdvanceChar();
        }
    }

    private void ScanHexDigits()
    {
        while (IsHexDigit(Current) || Current == '_')
        {
            if (Current == '_')
            {
                AdvanceChar();
                continue;
            }
            AdvanceChar();
        }
    }

    private void ScanBinaryDigits()
    {
        while (Current == '0' || Current == '1' || Current == '_')
        {
            if (Current == '_')
            {
                AdvanceChar();
                continue;
            }
            AdvanceChar();
        }
    }

    private static bool IsHexDigit(char c) =>
        c >= '0' && c <= '9' ||
        c >= 'a' && c <= 'f' ||
        c >= 'A' && c <= 'F';

    private SyntaxKind ScanNumericSuffix()
    {
        // Handle common suffixes. This can be extended as needed.
        if (Current == 'f' || Current == 'F')
        {
            AdvanceChar();
            return SyntaxKind.FloatLiteralToken;
        }
        if (Current == 'd' || Current == 'D')
        {
            AdvanceChar();
            return SyntaxKind.DoubleLiteralToken;
        }
        if (Current == 'm' || Current == 'M')
        {
            AdvanceChar();
            return SyntaxKind.DecimalLiteralToken;
        }
        if (Current == 'l' || Current == 'L')
        {
            AdvanceChar();
            // Could be long or unsigned long; you might need to look for a preceding 'u' or handle separately.
            return SyntaxKind.LongLiteralToken;
        }
        if (Current == 'u' || Current == 'U')
        {
            AdvanceChar();
            // Check if next is l/L for unsigned long
            if (Current == 'l' || Current == 'L')
            {
                AdvanceChar();
                return SyntaxKind.ULongLiteralToken;
            }
            return SyntaxKind.UIntLiteralToken;
        }
        return SyntaxKind.BadToken;
    }

    private object ParseNumericValue(string text, SyntaxKind kind)
    {
        try
        {
            switch (kind)
            {
                case SyntaxKind.IntLiteralToken:
                    // Try int first, then long
                    if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int intVal))
                        return intVal;
                    if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out long longVal))
                        return longVal;
                    return null; // overflow or format error

                case SyntaxKind.LongLiteralToken:
                    if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out long lVal))
                        return lVal;
                    return null;

                case SyntaxKind.UIntLiteralToken:
                    if (uint.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out uint uiVal))
                        return uiVal;
                    return null;

                case SyntaxKind.ULongLiteralToken:
                    if (ulong.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong ulVal))
                        return ulVal;
                    return null;

                //case SyntaxKind.HexIntegerLiteralToken:
                //    // Remove "0x" prefix and parse as hex
                //    string hexText = text.Substring(2).Replace("_", "");
                //    if (int.TryParse(hexText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hexInt))
                //        return hexInt;
                //    if (long.TryParse(hexText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long hexLong))
                //        return hexLong;
                //    return null;

                case SyntaxKind.BinaryIntegerLiteralToken:
                    string binText = text.Substring(2).Replace("_", "");
                    // Convert binary string manually or use Convert.ToInt32 with base 2
                    try
                    {
                        return Convert.ToInt32(binText, 2);
                    }
                    catch
                    {
                        try
                        {
                            return Convert.ToInt64(binText, 2);
                        }
                        catch
                        {
                            return null;
                        }
                    }

                case SyntaxKind.FloatLiteralToken:
                    if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float fVal))
                        return fVal;
                    return null;

                case SyntaxKind.DoubleLiteralToken:
                    if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double dVal))
                        return dVal;
                    return null;

                case SyntaxKind.DecimalLiteralToken:
                    if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal decVal))
                        return decVal;
                    return null;

                default:
                    return null;
            }
        }
        catch (OverflowException)
        {
            return null;
        }
        catch (FormatException)
        {
            return null;
        }
    }

    private void ScanStringLiteral()
    {
        int start = TextWindow.Position;

        // Consume the opening quote
        AdvanceChar(); // move past the opening "

        // Build the string content while handling escape sequences
        var contentBuilder = new StringBuilder();
        bool isVerbatim = false; // If your language supports @"..." style strings
        bool isInterpolated = false; // If your language supports $"..." style strings

        while (true)
        {
            if (Current == '\0')
            {
                // Unterminated string - end of file reached before closing quote
                _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_UnterminatedStringLiteral, DiagnosticSeverity.Error));
                break;
            }

            if (Current == '"')
            {
                // Check for escaped quote in regular strings ("")
                if (!isVerbatim && Lookahead == '"')
                {
                    // Double quote escape: ""
                    contentBuilder.Append('"');
                    AdvanceChar(2); // consume both quotes
                    continue;
                }
                else
                {
                    // Closing quote
                    AdvanceChar(); // consume the closing "
                    break;
                }
            }

            if (Current == '\\' && !isVerbatim)
            {
                // Handle escape sequences in regular strings
                AdvanceChar(); // consume the backslash

                if (Current == '\0')
                {
                    _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_UnterminatedStringLiteral, DiagnosticSeverity.Error));
                    break;
                }

                char escapedChar = ScanEscapeSequence();
                contentBuilder.Append(escapedChar);
            }
            else if (Current == '\r' || Current == '\n')
            {
                // Newlines in regular strings are not allowed unless verbatim
                if (!isVerbatim)
                {
                    _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_NewlineInStringLiteral, DiagnosticSeverity.Error));
                }

                // Still need to consume the newline properly
                if (Current == '\r' && Lookahead == '\n')
                    AdvanceChar(2);
                else
                    AdvanceChar();

                if (!isVerbatim)
                    break; // Stop scanning on newline in regular strings (error recovery)
                else
                    contentBuilder.Append('\n'); // In verbatim strings, preserve newlines
            }
            else
            {
                // Normal character
                contentBuilder.Append(Current);
                AdvanceChar();
            }
        }

        // Extract the full lexeme text (including quotes)
        string lexemeText = TextWindow.GetText(start, TextWindow.Position - start);

        // Set the token kind
        if (isInterpolated)
            _kind = SyntaxKind.InterpolatedStringLiteralToken;
        else if (isVerbatim)
            _kind = SyntaxKind.VerbatimStringLiteralToken;
        else
            _kind = SyntaxKind.StringLiteralToken;

        // Store the actual string value (content without quotes, with escapes processed)
        _value = contentBuilder.ToString();
    }

    private char ScanEscapeSequence()
    {
        // Handle standard escape sequences
        switch (Current)
        {
            case '\'':
                AdvanceChar();
                return '\'';
            case '"':
                AdvanceChar();
                return '"';
            case '\\':
                AdvanceChar();
                return '\\';
            case '0':
                AdvanceChar();
                return '\0';
            case 'a':
                AdvanceChar();
                return '\a';
            case 'b':
                AdvanceChar();
                return '\b';
            case 'f':
                AdvanceChar();
                return '\f';
            case 'n':
                AdvanceChar();
                return '\n';
            case 'r':
                AdvanceChar();
                return '\r';
            case 't':
                AdvanceChar();
                return '\t';
            case 'v':
                AdvanceChar();
                return '\v';

            case 'x': // Hexadecimal escape \xNN
                AdvanceChar(); // consume 'x'
                return ScanHexEscape(2); // At least 2 digits

            case 'u': // Unicode escape \uNNNN
                AdvanceChar(); // consume 'u'
                return ScanHexEscape(4);

            case 'U': // Unicode escape \UNNNNNNNN
                AdvanceChar(); // consume 'U'
                return ScanHexEscape(8);

            default:
                // Invalid escape sequence
                _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_InvalidEscapeSequence, DiagnosticSeverity.Error, Current));
                char invalidChar = Current;
                AdvanceChar();
                return invalidChar; // Return the character as-is for error recovery
        }
    }

    private char ScanHexEscape(int expectedDigits)
    {
        int start = TextWindow.Position;
        int digitCount = 0;
        int value = 0;

        // Read up to expectedDigits hex digits
        while (digitCount < expectedDigits && IsHexDigit(Current))
        {
            char c = Current;
            int digitValue;

            if (c >= '0' && c <= '9')
                digitValue = c - '0';
            else if (c >= 'a' && c <= 'f')
                digitValue = c - 'a' + 10;
            else // 'A' to 'F'
                digitValue = c - 'A' + 10;

            value = value * 16 + digitValue;
            AdvanceChar();
            digitCount++;
        }

        if (digitCount == 0)
        {
            // No hex digits found
            _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_InvalidEscapeSequence, DiagnosticSeverity.Error));
            return '\0';
        }

        if (digitCount < expectedDigits)
        {
            // Fewer digits than expected
            _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_InvalidEscapeSequence, DiagnosticSeverity.Error));
        }

        return (char)value;
    }

    private void ScanIdentifierOrKeyword()
    {
        int start = TextWindow.Position;
        bool isVerbatim = false;

        if (Current == '@')
        {
            isVerbatim = true;
            AdvanceChar();
            start = TextWindow.Position;
        }

        if (!IsIdentifierStartCharacter(Current))
        {
            _kind = SyntaxKind.BadToken;
            _value = null;
            return;
        }

        var builder = new StringBuilder();

        while (true)
        {
            if (!isVerbatim && Current == '\\' && (Lookahead == 'u' || Lookahead == 'U'))
            {
                builder.Append(ScanUnicodeEscape());
            }
            else if (IsIdentifierPartCharacter(Current))
            {
                builder.Append(Current);
                AdvanceChar();
            }
            else
            {
                break;
            }
        }

        string text = builder.ToString();
        _kind = SyntaxFacts.GetKeywordKind(text);

        if (_kind != SyntaxKind.IdentifierToken && isVerbatim)
            _kind = SyntaxKind.IdentifierToken;

        _value = _kind == SyntaxKind.IdentifierToken ? text : null;
    }

    private static bool IsIdentifierStartCharacter(char c)
    {
        // Lu, Ll, Lt, Lm, Lo, Nl (letters and letter numbers), plus underscore
        return c == '_' || char.IsLetter(c) || char.GetUnicodeCategory(c) == UnicodeCategory.LetterNumber;
    }

    private static bool IsIdentifierPartCharacter(char c)
    {
        // Start characters plus Nd (decimal digit numbers), Pc (connector punctuation), etc.
        return IsIdentifierStartCharacter(c) || char.IsDigit(c) || char.GetUnicodeCategory(c) == UnicodeCategory.ConnectorPunctuation;
    }

    private char ScanUnicodeEscape()
    {
        AdvanceChar(); // consume backslash
        char type = Current; // 'u' or 'U'
        AdvanceChar(); // consume 'u' or 'U'

        int digitCount = type == 'u' ? 4 : 8;
        int value = 0;

        for (int i = 0; i < digitCount; i++)
        {
            if (!IsHexDigit(Current))
            {
                // Invalid escape – add diagnostic
                _pendingDiagnostics.Add(new DiagnosticInfo(ErrorCode.ERR_InvalidEscapeSequence, DiagnosticSeverity.Error, Current));
                return '\0';
            }

            value = (value << 4) + HexValue(Current);
            AdvanceChar();
        }

        return (char)value;
    }

    private static int HexValue(char c) => c switch
    {
        >= '0' and <= '9' => c - '0',
        >= 'a' and <= 'f' => c - 'a' + 10,
        >= 'A' and <= 'F' => c - 'A' + 10,
        _ => 0
    };

}

/// <summary>
/// Defines the lexer modes that control how tokens are scanned.
/// </summary>

public enum LexerMode
{
    Normal,
    InterpolatedString,
    VerbatimString
    // add others as needed
}