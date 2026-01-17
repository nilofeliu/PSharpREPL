using REPL.systemfiles.registry.interfaces;


namespace REPL.systemfiles.registry.regType;

// User registry entry
public class UserType : IRegistryType
{
    public string Key { get; set; }
    public Guid Id { get; set; }
    public DateTime LastAccessed { get; private set; }
    RegistryKind IRegistryType.Kind  { get; }
    RegistryPath IRegistryType.Path  { get;}
    public string Username { get; set; }
    public string HomeDirectory { get; set; }
    public List<string> Roles { get; set; }
    public Dictionary<string, bool> Permissions { get; set; }
    public Guid ConfigId { get; set; }

   
   

    public void UpdateAccess()
    {
        LastAccessed = DateTime.UtcNow;
    }
}
