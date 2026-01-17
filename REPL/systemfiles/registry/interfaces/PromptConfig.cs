namespace REPL.systemfiles.registry.interfaces;

public sealed class PromptConfig
{
    public bool ShowTime { get; set; }
    public bool ShowDate { get; set; }
    public bool ShowUsername { get; set; }
    public bool ShowHostname { get; set; }
    public bool ShowDomain { get; set; }
    public bool ShowEnvironment { get; set; }
    public Dictionary<string, ConsoleColor> ElementColors { get; set; }
}
