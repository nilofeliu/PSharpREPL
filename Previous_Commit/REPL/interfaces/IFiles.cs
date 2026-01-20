namespace REPL.interfaces
{
    internal interface IFiles
    {
        internal string FilePath { get; }
        internal string FileName { get; }
        internal long FileSize { get; }
        internal Dictionary<IUserData, IPermission> Permissions { get; }
        internal DateTime CreatedAt { get; }
        internal DateTime ModifiedAt { get; }
        internal DateTime ModifiedBy { get; }
        internal DateTime ModifiedOn { get; }
    }
}
