using REPL.systemfiles.settings;
using REPL.ui;
using static REPL.core.Utils;

namespace REPL.core;

internal static class StaticCommands
{
    internal static SysInfo SystemCommands = new();

    private static string ReturnCommandLine(string input)
    {
        if (input[0] == '.')
            return input.Substring(1); // Remove the leading '.'
        else
            return input;
    }

    internal static int Run(string line)
    {
        int output = 0;

        if (line.StartsWith(".", StringComparison.OrdinalIgnoreCase))
        {
            string commandLine = ReturnCommandLine(line);
            

            switch (commandLine.ToLower())
            {
                case "exit":
                case "quit":
                    Console.WriteLine("Exiting...");
                    ReplExitMessage.PrintExitMessage();
                    output = 1;
                    break;
                case "cls":
                    Console.Clear();
                    break; ;
                case "help":
                    PrintCommands();
                    break;
                case "sys":
                    SystemCommands.ShowSysData();
                    break;
                default:
                    PrintCommandAnalysis(commandLine);
                    break;
            }
        }
        return output;
    }
}
