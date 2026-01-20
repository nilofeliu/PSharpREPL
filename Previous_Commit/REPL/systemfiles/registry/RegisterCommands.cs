using REPL.systemfiles.registry.interfaces;
using System.Collections.Concurrent;

namespace REPL.systemfiles.registry;


internal class RegisterCommands
{
    public RegisterCommands() 
    {
        var commandList = ReturnCommands();
        foreach (var command in commandList)
        {

        }

    }

    private List<IRegistryType> ReturnCommands()
    {
        return new List<IRegistryType>();
    }
}