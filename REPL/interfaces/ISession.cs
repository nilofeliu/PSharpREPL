namespace REPL.interfaces
{
    internal interface ISession
    {
        internal IUserData User { get; }
        internal bool IsActive { get; }
        internal DateTime SessionTimeStart { get; }
        internal DateTime SessionTimeEnd { get; }
        internal DateTime SessionTimeout { get; }

    }
}
