using REPL.language.ast;
using REPL.systemfiles.commands;
using REPL.systemfiles.diagnostics;
using REPL.utils;
using System.Collections.Immutable;

namespace REPL.core;

internal static class Utils
{
    internal static string ReturnCommandLine(string input)
    {
        if (input[0] == '.')
            return input.Substring(1); // Remove the leading '.'
        else
            return input;
    }

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

    internal static void PrintTree(SyntaxNode node, string indent = "", bool isLast = true)
    {
        // |__
        // |--
        // |

        try
        {
            var marker = isLast ? "└──" : "├──";
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write($"{indent}{marker}{node.Kind}");

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write($" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "   " : "│  ";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        //throw;
        }


        var lastChild = node.GetChildren().LastOrDefault();

        foreach (var child in node.GetChildren())
        {
            PrintTree(child, indent, child == lastChild);
        }

        Console.ResetColor();
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