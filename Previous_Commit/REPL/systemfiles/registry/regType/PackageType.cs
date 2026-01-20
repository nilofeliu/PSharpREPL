using REPL.systemfiles.registry.interfaces;

namespace REPL.systemfiles.registry.regType;

// Command registry entry
public class PackageType : IRegistryType
{
    public string Key => throw new NotImplementedException();

    public Guid Id => throw new NotImplementedException();
    RegistryKind IRegistryType.Kind { get; }
    RegistryPath IRegistryType.Path { get; }
    public DateTime LastAccessed => throw new NotImplementedException();

    public void UpdateAccess()
    {
        throw new NotImplementedException();
    }
}


