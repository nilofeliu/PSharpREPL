
namespace REPL.systemfiles.registry.interfaces

{
    // Base interface for all registry entries
    public interface IRegistryType
    {
        string Key { get; }
        RegistryKind  Kind { get; } 
        RegistryPath Path { get; }
        Guid Id { get; }
        DateTime LastAccessed { get; }
        void UpdateAccess();
    }
}