using REPL.systemfiles.registry.interfaces;

namespace REPL.systemfiles.registry.regType;

public class ApplicationType : IRegistryType
{
    public string Key { get; set; }
    public Guid Id { get; set; }
    public DateTime LastAccessed { get; private set; }

    public string Name { get; set; }
    RegistryKind IRegistryType.Kind { get; }
    RegistryPath IRegistryType.Path { get; }
    public string DefinitionPath { get; set; }
    public string SyntaxPath { get; set; }
    public string HelpPath { get; set; }
    public bool IsSystemCommand { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
       

    public void UpdateAccess()
    {
        LastAccessed = DateTime.UtcNow;
    }
}
