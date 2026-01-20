using REPL.systemfiles.registry.interfaces;

namespace REPL.systemfiles.registry.regType;

public class SessionType : IRegistryType
{
    public string Key { get; set; }
    public Guid Id { get; set; }
    public DateTime LastAccessed { get; private set; }
    RegistryKind IRegistryType.Kind { get; }
    RegistryPath IRegistryType.Path { get; }
    public List<UserType> Users { get; set; } = new();
    public Guid CurrentUserId { get; set; }
    public Guid? PreviousUserId { get; set; }
    public DateTime StartTime { get; set; }
    public Guid ActiveEnvironmentId { get; set; }
    public Dictionary<string, object> SessionVariables { get; set; }
    public string WorkingDirectory { get; set; }

    public void UpdateAccess()
    {
        LastAccessed = DateTime.UtcNow;
    }

    public void AddUser(UserType user)
    {
        if (!Users.Any(u => u.Id == user.Id))
        {
            Users.Add(user);
        }
    }

    public void RemoveUser(Guid userId)
    {
        Users.RemoveAll(u => u.Id == userId);
    }
}