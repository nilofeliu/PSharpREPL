using Microsoft.Win32;
using REPL.systemfiles.registry.regType;
using System.Collections.Concurrent;
using REPL.systemfiles.registry.interfaces;

namespace REPL.systemfiles.registry.interfaces;

public sealed class RegistryDictionary
    {
        public readonly ConcurrentDictionary<string, IRegistryType> LocalMachine = new();
        public readonly ConcurrentDictionary<string, IRegistryType> LocalSystem = new();
        public readonly ConcurrentDictionary<string, IRegistryType> LocalHost = new();
        public readonly ConcurrentDictionary<string, IRegistryType> LocalPath = new();
        public readonly ConcurrentDictionary<string, IRegistryType> LocalApplications = new();
        public readonly ConcurrentDictionary<string, IRegistryType> LocalSettings = new();
        public readonly ConcurrentDictionary<string, IRegistryType> LocalUsers = new();

        public void ClearAll()
        {
            LocalMachine.Clear();
            LocalSystem.Clear();
            LocalHost.Clear();
            LocalPath.Clear();
            LocalApplications.Clear();
            LocalSettings.Clear();
            LocalUsers.Clear();
        }

    public void Add(IRegistryType entry)
    {
        var registryPath = GetDictionary(entry.Path);
        registryPath[entry.Key] = entry;
    }

    public bool Remove(IRegistryType entry)
    {
        var registryPath = GetDictionary(entry.Path);
        return registryPath.TryRemove(entry.Key, out _);
    }

    public void Clear(IRegistryType entry)
    {
        var registryPath = GetDictionary(entry.Path);
        registryPath.Clear();
    }


    public IRegistryType GetField(string key, RegistryKind kind)
    {
        var dictionaryName = RegistryHelper.GetRegistryPath(kind);
        return GetIRegistryType(key, dictionaryName);
    }

    internal IRegistryType GetIRegistryType(string key, RegistryPath path)
    {

            var dict = GetDictionary(path);
            var output = dict.TryGetValue(key, out var entry) ? entry : null;
            return output;                       
    }


    internal ConcurrentDictionary<string, IRegistryType> GetDictionary(RegistryPath dictionaryName)
    {
        return dictionaryName switch
        {
            RegistryPath.LocalMachine => LocalMachine,
            RegistryPath.LocalSystem => LocalSystem,
            RegistryPath.LocalHost => LocalHost,
            RegistryPath.LocalPath => LocalPath,
            RegistryPath.LocalApplications => LocalApplications,
            RegistryPath.LocalSettings => LocalSettings,
            RegistryPath.LocalUsers => LocalUsers,
            _ => throw new ArgumentException($"Unknown dictionary: {dictionaryName}")
        };
    }

    internal static class RegistryHelper
    {
        public static RegistryPath GetRegistryPath(IRegistryType entry)
        {
            return entry switch
            {
                UserType => RegistryPath.LocalUsers,
                EnvironmentType => RegistryPath.LocalEnvironment,
                PackageType => RegistryPath.LocalSystem,
                ConfigurationType => RegistryPath.LocalSystem,
                CommandType => RegistryPath.LocalApplications,
                ApplicationType => RegistryPath.LocalApplications,
                PathType => RegistryPath.LocalPath,
                SessionType => RegistryPath.LocalHost,
                SettingsType => RegistryPath.LocalSettings,
                _ => throw new ArgumentException("Unknown IRegistryType implementation")
            };
        }


        public static RegistryPath GetRegistryPath(RegistryKind entry)
        {

            return entry switch
            {
                RegistryKind.UserType => RegistryPath.LocalUsers,
                RegistryKind.EnvironmentType => RegistryPath.LocalEnvironment,
                RegistryKind.PackageType => RegistryPath.LocalSystem,
                RegistryKind.ConfigurationType => RegistryPath.LocalSystem,
                RegistryKind.CommandType => RegistryPath.LocalApplications,
                RegistryKind.ApplicationType => RegistryPath.LocalApplications,
                RegistryKind.PathType => RegistryPath.LocalPath,
                RegistryKind.SessionType => RegistryPath.LocalHost,
                RegistryKind.SettingsType => RegistryPath.LocalSettings,
                _ => throw new ArgumentException("Unknown IRegistryType implementation")
            };
        }

        public static RegistryKind GetKindName(IRegistryType entry)
        {
            return entry switch
            {
                UserType => RegistryKind.UserType,
                EnvironmentType => RegistryKind.EnvironmentType,
                PackageType => RegistryKind.PackageType,
                ConfigurationType => RegistryKind.ConfigurationType,
                CommandType => RegistryKind.CommandType,
                ApplicationType => RegistryKind.ApplicationType,
                PathType => RegistryKind.PathType,
                SessionType => RegistryKind.SessionType,
                SettingsType => RegistryKind.SettingsType,
                _ => throw new ArgumentException("Unknown IRegistryType implementation")
            };
        }
    }

}
