namespace REPL.interfaces
{
    internal interface IAuthorization
    {
        internal IAuthentication Authentication { get; }
        internal List<string> Roles { get; }
        internal List<string> Permissions { get; }
        internal bool IsAuthorized(string action);
    }
}
