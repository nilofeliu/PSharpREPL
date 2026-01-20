namespace REPL.systemfiles.settings
{
    internal static class SysPath
    { 
        internal static readonly string SystemPath = AppContext.BaseDirectory;
        internal static readonly string CommandPath = Path.Combine(SystemPath, @"system\commands");
        internal static readonly string UserPath = Path.Combine(SystemPath, @"users");

    }
}

