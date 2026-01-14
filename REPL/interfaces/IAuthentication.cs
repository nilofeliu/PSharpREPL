namespace REPL.interfaces
{
    internal interface IAuthentication
    {
        internal ISession Session { get; }
        internal string AuthToken { get; }
        internal DateTime AuthenticatedAt { get; }
        internal DateTime ExpiresAt { get; }
    }
}
