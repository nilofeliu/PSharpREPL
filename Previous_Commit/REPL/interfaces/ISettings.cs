namespace REPL.interfaces
{
    internal interface ISettings
    {
        internal IConfiguration Configuration { get; }
        internal string SettingName { get; }
        internal string SettingValue { get; }
    }
}
