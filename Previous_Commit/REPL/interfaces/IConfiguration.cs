namespace REPL.interfaces
{
    internal interface IConfiguration
    {
        internal string ConfigName { get; }
        internal string ConfigValue { get; }
        internal DateTime LastUpdated { get; }
    }
}
