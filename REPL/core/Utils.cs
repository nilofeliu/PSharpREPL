using REPL.language;
using REPL.language.ast;
using REPL.systemfiles.commands;
using REPL.systemfiles.diagnostics;
using REPL.utils;
using System;
using System.Collections.Immutable;

namespace REPL.core;

internal static class Utils
{

    // To be removed later when adding the error to Diagnostics.
    internal static void PrintCommandAnalysis(string command)
    {
        if (CommandSyntax.GetValidCommands().Contains(command.ToLower()))
        {
            ConsoleColored.WriteLine("Command", ConsoleColor.White, $" {command} ", ConsoleColor.Red, "not implemented yet.", ConsoleColor.White);
        }
        else
        {
            ConsoleColored.WriteLine("Invalid command :", ConsoleColor.White, $"{command}", ConsoleColor.Red);
        }
    }

    internal static void PrintExceptions(List<Exception> exceptions)
    {
        if (exceptions.Count == 0)
            return;
        Console.WriteLine("Exceptions:");
        foreach (var ex in exceptions)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }

    internal static void PrintCommands()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("#exit - Exit the application");
        Console.WriteLine("#showtree - Toggle showing parse trees");
        Console.WriteLine("#showtokens - Toggle showing lexical tokens");
        Console.WriteLine("#cls - Clear the console");
        Console.WriteLine("#help - Show this help message");
        Console.WriteLine("Type any expression to evaluate it.");
    }


    internal static void PrintSyntaxTree(SyntaxTree syntaxTree, EvaluationResult result)
    {

        //PrintDiagnostics(result.Diagnostics, line);
        Console.WriteLine();


        foreach (var diagnostic in result.Diagnostics)
        {
            var lineIndex = syntaxTree.Text.GetLineIndex(diagnostic.Span.Start);
            var line = syntaxTree.Text.Lines[lineIndex];
            var lineNumber = lineIndex + 1;
            var character = diagnostic.Span.Start - line.Start + 1;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"({lineNumber}, {character}): ");
            Console.WriteLine(diagnostic);
            Console.ResetColor();

            try
            {

                var prefixSpan = TextSpan.FromBounds(line.Start, diagnostic.Span.Start);
                var suffixSpan = TextSpan.FromBounds(diagnostic.Span.End, line.End);

                var prefix = syntaxTree.Text.ToString(prefixSpan);
                var error = syntaxTree.Text.ToString(diagnostic.Span);
                var suffix = syntaxTree.Text.ToString(suffixSpan);

                Console.Write("    ");
                Console.Write(prefix);

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(error);
                Console.ResetColor();

                Console.WriteLine(suffix);

                Console.WriteLine();

            }
            catch (Exception e)
            {
                throw;
            }
        }

    }

    internal static void PrintDiagnostics(ImmutableArray<Diagnostic> diagnostics, string input)
    {

        Console.WriteLine();
        List<Exception> exceptions = new();

        foreach (var diagnostic in diagnostics)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(diagnostic);
            Console.ResetColor();

            var prefix = "";
            var error = "";
            var suffix = "";

            try
            {
                prefix = input.Substring(0, diagnostic.Span.Start);

                error = input.Substring(diagnostic.Span.Start, diagnostic.Span.Length);

                suffix = input.Substring(diagnostic.Span.End);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }

            Console.Write("    ");
            Console.Write(prefix);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(error);
            Console.ResetColor();

            Console.WriteLine(suffix);

            Console.WriteLine();
        }
    }
}