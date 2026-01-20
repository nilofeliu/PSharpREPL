namespace REPL.interfaces
{
    internal interface IPermission
    {
        internal bool read { get; }
        internal bool write { get; }
        internal bool execute { get; }
    }
}
