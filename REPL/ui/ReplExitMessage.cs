using static REPL.settings.AppSettings;

namespace REPL.ui
{
    internal class ReplExitMessage
    {

        internal static void PrintExitMessage()
        {
            Console.WriteLine();
            Console.WriteLine($"==============================================");
            Console.WriteLine($"       Thank you for using {AppName}       ");
            Console.WriteLine($"==============================================");
            Console.WriteLine();
        }
    }
}