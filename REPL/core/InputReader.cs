using REPL.language;
using REPL.systemfiles.environments;
using REPL.systemfiles.settings;
using REPL.systemfiles.users;
using REPL.ui;
using static REPL.core.Utils;

namespace REPL.core;

public class InputReader
{

    private SysSettings SystemSettings = SysSettings.Instance;
    private PromptStream _promptStream;
        internal PromptStream Prompt => _promptStream;

    private Dictionary<VariableSymbol, object> variables = new();

    private bool showTree = true;
    private bool commandMode = false;

    public InputReader()
    {
        VirtualEnv mainEnv = new VirtualEnv();
        UserData userData = new UserData();
        _promptStream = new PromptStream(userData, mainEnv);
    }

    public void Start()
    {
        RunREPL();
    }

    private void RunREPL()
    {

        //Console.WriteLine("Welcome to the REPL Terminal. Type 'exit' to quit.");
        ReplWelcomeMessage.PrintWelcomeMessage();
        while (true)
        {
            Prompt.Write();
            string? line = Console.ReadLine();


            if (string.IsNullOrEmpty(line))
            {
                    continue;
            }

            var syntaxTree = SyntaxTree.Parse(line);
            //Utils.PrintTree(syntaxTree.Root);
            var interpreter = new Interpreter(syntaxTree);
            var result = interpreter.Evaluate(variables);


            if (!result.Diagnostics.Any())
            {
                Console.WriteLine($"{result.Value}");
            }
            else
            {
                PrintDiagnostics(result.Diagnostics, line);
            }

            if (showTree)
            {
                Utils.PrintTree(syntaxTree.Root);
            }


        }
    }
}
