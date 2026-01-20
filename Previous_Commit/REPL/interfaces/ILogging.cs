namespace REPL.interfaces
{
    internal interface ILogging
    {
        internal DateTime LogTime { get; }
        internal string LogLevel { get; }
        internal string Message { get; }
        internal string Source { get; }
    }
}
