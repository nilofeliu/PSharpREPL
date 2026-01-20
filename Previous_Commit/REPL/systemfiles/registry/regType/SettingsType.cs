using REPL.systemfiles.registry.interfaces;

namespace REPL.systemfiles.registry.regType;

// System-level settings entry
public class SettingsType : IRegistryType
{
    public string Key { get; set; }
    public Guid Id { get; set; }
    public DateTime LastAccessed { get; private set; }
    RegistryKind IRegistryType.Kind { get; }
    RegistryPath IRegistryType.Path { get; }
    public Dictionary<string, object> SystemSettings { get; set; }
    public string SettingsFilePath { get; set; }

    public void UpdateAccess()
    {
        LastAccessed = DateTime.UtcNow;
    }
}
