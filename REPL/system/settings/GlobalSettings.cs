namespace REPL.system.settings
{
    internal class AppSettings
    {
        private static readonly AppSettings _instance = new();
        internal static AppSettings Instance => _instance;

        internal static string AppName { get; set; } = "REPL Terminal";
        internal static string AppVersion { get; set; } = "alpha 0.0.1";
        internal static string AppCreationDate { get; set; } = "2026-01-12";
        internal static string AppDeveloper { get; set; } = "Nilo Correia Martinez";
        
        public AppSettings()
        {
            
        }
    }
}
