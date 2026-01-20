using REPL.language;
using REPL.systemfiles.environments;
using REPL.systemfiles.settings;
using REPL.systemfiles.users;
using REPL.ui;
using System;
using System.Text;
using static REPL.core.Utils;

namespace REPL.core;

public class InputReader
{

    List<Exception> exceptions = new();

    private SysSettings SystemSettings = SysSettings.Instance;
    private PromptStream _promptStream;
    internal PromptStream Prompt => _promptStream;

    private Dictionary<VariableSymbol, object> variables = new();

    private bool showTree = true;
    private bool commandMode = false;

    StringBuilder textBuilder = new StringBuilder();

    public InputReader()
    {
        VirtualEnv mainEnv = new VirtualEnv();
        UserData userData = new UserData();
        _promptStream = new PromptStream(textBuilder, userData, mainEnv);
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

            string input = Console.ReadLine();
            var isBlank = string.IsNullOrWhiteSpace(input);


            if (textBuilder.Length == 0)
            {
                if (isBlank)
                {
                    ReplExitMessage.PrintExitMessage();
                    break;
                }
                if (StaticCommands.Run(input) == 1)
                {
                    return;
                }
            }

            textBuilder.AppendLine(input);
            var text = textBuilder.ToString();

            var syntaxTree = SyntaxTree.Parse(text);

            if (!isBlank && syntaxTree.Diagnostics.Any())
            {
                continue;
            }


            var interpreter = new Interpreter(syntaxTree);
            var result = interpreter.Evaluate(variables);


            if (showTree)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                syntaxTree.Root.WriteTo(Console.Out);
                Console.ResetColor();
            }


            try
            {
                if (!result.Diagnostics.Any())
                {
                    Console.WriteLine($"{result.Value}");
                }
                else
                {
                    Utils.PrintSyntaxTree(syntaxTree, result);
                }
                textBuilder.Clear();
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }

            if (exceptions.Any())
            {
                PrintExceptions(exceptions);
            }
            
        }
    }

    
}