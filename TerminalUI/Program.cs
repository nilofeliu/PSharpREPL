using REPL.core;

namespace TerminalUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InputReader terminal = new();
            terminal.Start();
        }
    }

}
