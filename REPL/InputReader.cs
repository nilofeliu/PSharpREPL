using REPL.system.environments;
using REPL.system.settings;
using REPL.system.users;
using REPL.ui;
using REPL.utils;

namespace REPL
{
    public class InputReader
    {

        private SysSettings SystemSettings = SysSettings.Instance;
        private PromptStream _promptStream;
        internal SysCommands SystemCommands;
        internal PromptStream Prompt => _promptStream;

        public InputReader()
        {
            VirtualEnv mainEnv = new VirtualEnv();
            UserData userData = new UserData();
            SystemCommands = new SysCommands();
            _promptStream = new PromptStream(userData, mainEnv);
        }

        public void Start()
        {
            PromptReader();
        }

        private void PromptReader()
        {

            //Console.WriteLine("Welcome to the REPL Terminal. Type 'exit' to quit.");
            ReplWelcomeMessage.PrintWelcomeMessage();
            while (true)
            {
                Prompt.Write();
                string line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;
                if (line.Trim().Equals("sys", StringComparison.OrdinalIgnoreCase))
                    SystemCommands.ShowSysData();
                    

                // Direct to InputStream — that's it
                var input = new InputStream(line);
                if (input.PeekChar(0) == '.')
                {
                    string commandStream = input.ReadCommand();
                    ConsoleColored.Write($"Command");
                    ConsoleColored.Write($" {commandStream} ", ConsoleColor.Red);
                    ConsoleColored.Write($"not implemented yet.\n");
                    continue;
                }

                // Send straight to lexer
                //var lexer = new Lexer(input);

                // Process tokens...
                //ProcessTokens(lexer);
            }
            ReplExitMessage.PrintExitMessage();
        }
    }
}
