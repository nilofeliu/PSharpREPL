using REPL.systemfiles.settings;

namespace REPL.systemfiles.commands
{
    internal static class CommandSyntax
    {

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
            "where",
            "cmd",
            "sh",
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

        internal static List<string> GetValidCommands()
        {
            return IOCommands
                .Concat(TerminalCommands)
                .Concat(AuthCommands)
                .Concat(ToolsCommands)
                .ToList();
        }

        public static List<string> GetInstalledCommands()
        {
            var result = new List<string>();

            foreach (var dir in Directory.GetDirectories(SysPath.CommandPath))
            {
                if (Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    result.Add(Path.GetFileName(dir));
                }
            }

            return result;
        }

    }

    
}