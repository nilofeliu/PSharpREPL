using REPL.systemfiles.registry;
using REPL.systemfiles.registry.interfaces;

namespace REPL.systemfiles.registry.regType;

// System path registry entry
public class PathType : IRegistryType
{
    public string Key { get; set; }
    public Guid Id { get; set; }
    public DateTime LastAccessed { get; private set; }
    RegistryKind IRegistryType.Kind { get; }
    RegistryPath IRegistryType.Path { get; }
    public string PathName { get; set; }
    public string FullPath { get; set; }
    public SystemPathType LocalPathType { get; set; }

    public void UpdateAccess()
    {
        LastAccessed = DateTime.UtcNow;
    }
}
