using System;
using System.Collections.Generic;

namespace REPL.language.Syntax.ListCommands
{
    internal static class CommandSyntax
    {

        internal static List<string> GetCommands()
        {
            return IOCommands
                .Concat(TerminalCommands)
                .Concat(AuthCommands)
                .Concat(ToolsCommands)
                .ToList();
        }

        private static readonly List<string> IOCommands = new List<string>
        {
            "read",
            "write",
            "append",
            "mv",
            "cp",
            "mkdir",
            "rm",
            "del"
        };

        private static readonly List<string> TerminalCommands = new List<string>
        {
            "pwd",
            "cd",
            "ls",
            "clear",
            "cls",
            "help",
            "exit",
            "version",
            "where"
        };

        private static readonly List<string> AuthCommands = new List<string>
        {
            "login",
            "logout",
            "register",
            "changepassword",
            "resetpassword"
        };

        private static readonly List<string> ToolsCommands = new List<string>
        {
            "env",
            "code",
            "c",
            "dotnet",
            "git",
            "nano"
        };
    }
}
