using REPL.systemfiles.registry.interfaces;
using REPL.systemfiles.registry.rules;

namespace REPL.systemfiles.registry.regType;

// Environment registry entry (namespace isolation for scripts)
public class EnvironmentType : IRegistryType
{
    public string Key { get; set; }
    public Guid Id { get; set; }
    public DateTime LastAccessed { get; private set; }
    RegistryKind IRegistryType.Kind { get; }
    RegistryPath IRegistryType.Path { get; }
    public Guid OwnerId { get; set; }
    public List<Guid> SharedWithUsers { get; set; }
    public string Name { get; set; }
    public string RootPath { get; set; }
    public IsolationRules Rules { get; set; }
    public Dictionary<Guid, SharingPermissions> UserPermissions { get; set; }
    public PromptConfig PromptConfiguration { get; set; }

    public void UpdateAccess()
    {
        LastAccessed = DateTime.UtcNow;
    }
}
