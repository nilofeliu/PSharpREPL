using REPL.systemfiles.registry.interfaces;

namespace REPL.systemfiles.registry.regType;

// User-level configuration entry
public class ConfigurationType : IRegistryType
{
    public string Key { get; set; }
    public Guid Id { get; set; }
    public DateTime LastAccessed { get; private set; }
    RegistryKind IRegistryType.Kind { get; }
    RegistryPath IRegistryType.Path { get; }
    public Guid UserId { get; set; }
    public Dictionary<string, string> Colors { get; set; }
    public Dictionary<string, string> Shortcuts { get; set; }
    public PromptConfig PromptConfiguration { get; set; }
    public string ConfigFilePath { get; set; }

    public void UpdateAccess()
    {
        LastAccessed = DateTime.UtcNow;
    }
}
