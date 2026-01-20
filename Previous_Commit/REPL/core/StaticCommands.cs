using REPL.systemfiles.settings;
using REPL.ui;
using static REPL.core.Utils;

namespace REPL.core;

internal static class StaticCommands
{
    internal static SysInfo SystemCommands = new();

    internal static void Run(string line)
    {
        if (line.StartsWith(".", StringComparison.OrdinalIgnoreCase))
        {
            string commandLine = ReturnCommandLine(line);

            switch (commandLine.ToLower())
            {
                case "exit":
                case "quit":
                    Console.WriteLine("Exiting...");
                    ReplExitMessage.PrintExitMessage();
                    return;
                case "cls":
                    Console.Clear();
                    return;
                case "help":
                    PrintCommands();
                    return;
                case "sys":
                    SystemCommands.ShowSysData();
                    return;
                default:
                    PrintCommandAnalysis(commandLine);
                    return;
            }
        }
    }
}
