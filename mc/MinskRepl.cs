using Minsk.CodeAnalysis.Syntax;
using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Compilations;
using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Text;

namespace PSharp;

internal sealed class PSharpRepl : Repl
{
    private Compilation _previous;
    private bool _showTree;
    private bool _showProgram;

    private bool _inBlockComment = false;

    private readonly Dictionary<VariableSymbol, object> _variables = new Dictionary<VariableSymbol, object>();

    //protected override void RenderLine(string line)
    //{
    //    Console.WriteLine(line);
    //}


    protected override void RenderLine(string line)
    {
        // If we're inside a block comment, check if this line ends it
        if (_inBlockComment)
        {
            var endIndex = line.IndexOf("*/");
            if (endIndex >= 0)
            {
                // Print everything up to and including */ in green
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(line.Substring(0, endIndex + 2));
                Console.ResetColor();
                _inBlockComment = false;
                // Render the rest of the line normally
                line = line.Substring(endIndex + 2);
                if (string.IsNullOrEmpty(line)) return;
            }
            else
            {
                // Entire line is inside block comment
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(line);
                Console.ResetColor();
                return;
            }
        }

        var tokens = SyntaxTree.ParseTokens(line);
        foreach (var token in tokens)
        {
            foreach (var trivia in token.LeadingTrivia)
            {
                if (trivia.Kind == SyntaxKind.SingleLineCommentTrivia ||
                    trivia.Kind == SyntaxKind.MultiLineCommentTrivia)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(trivia.Text);
                Console.ResetColor();
            }

            var isSystemKeyword = SyntaxFacts.IsSystemKeyword(token.Kind);
            var isControlKeyword = SyntaxFacts.IsControlKeyword(token.Kind);
            var isNumber = token.Kind == SyntaxKind.NumericLiteralToken ||
                token.Kind == SyntaxKind.IntegerLiteralToken ||
                token.Kind == SyntaxKind.LongLiteralToken ||
                token.Kind == SyntaxKind.DoubleLiteralToken ||
                token.Kind == SyntaxKind.DecimalLiteralToken;
            var isIdentifier = token.Kind == SyntaxKind.IdentifierToken;
            var isString = token.Kind == SyntaxKind.StringLiteralToken;

            if (isSystemKeyword)
                Console.ForegroundColor = ConsoleColor.Blue;
            else if (isControlKeyword)
                Console.ForegroundColor = ConsoleColor.Magenta;
            else if (isIdentifier)
                Console.ForegroundColor = ConsoleColor.White;
            else if (isNumber)
                Console.ForegroundColor = ConsoleColor.Cyan;
            else if (isString)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            else
                Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write(token.Text);
            Console.ResetColor();

            foreach (var trivia in token.TrailingTrivia)
            {
                if (trivia.Kind == SyntaxKind.SingleLineCommentTrivia ||
                    trivia.Kind == SyntaxKind.MultiLineCommentTrivia)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(trivia.Text);
                Console.ResetColor();
            }
        }

        // Check if this line started a block comment that wasn't closed
        if (line.Contains("/*") && !line.Contains("*/"))
            _inBlockComment = true;
    }


    protected override void EvaluateMetaCommand(string input)
    {
        switch (input)
        {
            case "#exit":
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
                return;
            case "#showTree":
                _showTree = !_showTree;
                Console.WriteLine(_showTree ? "Showing parse trees." : "Not showing parse trees.");
                break;
            case "#showProgram":
                _showProgram = !_showProgram;
                Console.WriteLine(_showProgram ? "Showing bound tree." : "Not showing bound tree.");
                break;
            case "#cls":
                Console.Clear();
                break;
            case "#generateSyntax":
                RunCodeGenerator();
                break;
            case "#generateParser":
                RunParserGenerator();
                break;
            case "#reset":
                _previous = null;
                _variables.Clear();
                break;
            default:
                base.EvaluateMetaCommand(input);
                break;
        }
    }



    protected override bool IsCompleteSubmission(string text)
    {
        if (string.IsNullOrEmpty(text))
            return true;

        var lastTwoLinesAreBlank = text.Split(Environment.NewLine)
            .Reverse()
            .TakeWhile(s => string.IsNullOrEmpty(s))
            .Take(2)
            .Count() == 2;

        if (lastTwoLinesAreBlank)
            return true;

        // Count unmatched /* */
        var openComments = text.Split("/*").Length - 1;
        var closeComments = text.Split("*/").Length - 1;
        if (openComments > closeComments)
            return false;


        var syntaxTree = SyntaxTree.Parse(text);

        // Use Statement because we need to exclude the EndOfFileToken.
        if (syntaxTree.Root.Statement.GetLastToken().IsMissing)
            return false;

        return true;
    }

    protected override void EvaluateSubmission(string text)
    {
        var syntaxTree = SyntaxTree.Parse(text);
        var compilation = _previous == null
                            ? new Compilation(syntaxTree)
                            : _previous.ContinueWith(syntaxTree);

        if (_showTree)
            syntaxTree.Root.WriteTo(Console.Out);

        if (_showProgram)
            compilation.EmitTree(Console.Out);

        var result = compilation.Evaluate(_variables);

        if (!result.Diagnostics.Any())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result.Value);
            Console.ResetColor();
            _previous = compilation;
        }
        else
        {
            var textLength = syntaxTree.Text.Length;

            foreach (var diagnostic in result.Diagnostics)
            {
                var lineIndex = syntaxTree.Text.GetLineIndex(diagnostic.Span.Start);
                var line = syntaxTree.Text.Lines[lineIndex];
                var lineNumber = lineIndex + 1;
                var character = diagnostic.Span.Start - line.Start + 1;

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"({lineNumber}, {character}): ");
                Console.WriteLine(diagnostic);
                Console.ResetColor();

                var lineStart = line.Start;
                var lineEnd = Math.Min(line.End, textLength);
                var diagStart = Math.Clamp(diagnostic.Span.Start, lineStart, lineEnd);
                var diagEnd = Math.Clamp(diagnostic.Span.End, lineStart, lineEnd);

                var prefix = syntaxTree.Text.ToString(TextSpan.FromBounds(lineStart, diagStart));
                var error = syntaxTree.Text.ToString(TextSpan.FromBounds(diagStart, diagEnd));
                var suffix = syntaxTree.Text.ToString(TextSpan.FromBounds(diagEnd, lineEnd));

                Console.Write("    ");
                Console.Write(prefix);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(error);
                Console.ResetColor();
                Console.Write(suffix);
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }


}