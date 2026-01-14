using static REPL.system.settings.AppSettings;

namespace REPL.ui
{
    internal static class ReplWelcomeMessage
    {
        internal static void PrintWelcomeMessage()
        {
            Console.WriteLine($"==============================================");
            Console.WriteLine($"        Interactive {AppName}        ");
            Console.WriteLine($"==============================================");
            Console.WriteLine($" Version   : {AppVersion}");
            Console.WriteLine($" Created   : {AppCreationDate}");
            Console.WriteLine($" Developer : {AppDeveloper}");
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine($" Type 'help' to list available commands.");
            Console.WriteLine($" Type 'exit' to leave the terminal.");
            Console.WriteLine();
        }
    }
}