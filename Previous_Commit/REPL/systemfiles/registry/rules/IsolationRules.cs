namespace REPL.systemfiles.registry.rules;

public class IsolationRules
{
    public bool AllowCrossEnvironmentAccess { get; set; }
    public List<string> AllowedExternalPaths { get; set; }
}
