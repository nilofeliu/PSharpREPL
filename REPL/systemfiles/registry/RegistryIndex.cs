//using REPL.systemfiles.registry.interfaces;
//using System.Collections.Concurrent;

//namespace REPL.systemfiles.registry;

//public sealed partial class RegistryIndex
//{
//    private static readonly Lazy<RegistryIndex> _instance = new Lazy<RegistryIndex>(() => new RegistryIndex());
//    private readonly RegistryDictionary _registry = new();

//    public static RegistryIndex Instance => _instance.Value;

//    private RegistryIndex() { }

//    public void Register(IRegistryType entry)
//    {
//        _registry.Add(entry);
//    }

//    public T Resolve<T>(string key, RegistryPath dictionaryName) where T : IRegistryType
//    {
//        var entry = _registry.GetField(key, dictionaryName);
//        if (entry is T typedEntry)
//        {
//            typedEntry.UpdateAccess();
//            return typedEntry;
//        }
//        return default;
//    }

//    public bool TryResolve<T>(string key, out T value, RegistryPath dictionaryName) where T : IRegistryType
//    {
//        var entry = _registry.GetField(key, dictionaryName);
//        if (entry is T typedEntry)
//        {
//            typedEntry.UpdateAccess();
//            value = typedEntry;
//            return true;
//        }
//        value = default;
//        return false;
//    }

//    public bool Contains(string key, RegistryPath dictionaryName)
//    {
//        return _registry.GetField(key, dictionaryName) != null;
//    }

//    public bool Remove(IRegistryType entry)
//    {
//        return _registry.Remove(entry);
//    }

//    public void ClearAll()
//    {
//        _registry.ClearAll();
//    }

//    public void Clear(IRegistryType entry)
//    {
//        _registry.Clear(entry);
//    }

//    public ConcurrentDictionary<string, IRegistryType> GetFullDictionary(string dictionaryName)
//    {
//        return _registry.ReturnDictionary(dictionaryName);
//    }

//    public IEnumerable<T> GetEntriesOfType<T>(string dictionaryName = "") where T : IRegistryType
//    {
//        if (!string.IsNullOrEmpty(dictionaryName))
//        {
//            var dict = _registry.ReturnDictionary(dictionaryName);
//            return dict.Values.OfType<T>();
//        }

//        // Get all entries from all dictionaries
//        var allEntries = new List<IRegistryType>();
//        var dictionaryNames = new[] { "LocalMachine", "LocalSystem", "LocalHost", "LocalPath",
//                                      "LocalApplications", "LocalSettings", "LocalUsers" };

//        foreach (var dictName in dictionaryNames)
//        {
//            var dict = _registry.ReturnDictionary(dictName);
//            allEntries.AddRange(dict.Values);
//        }

//        return allEntries.OfType<T>();
//    }

//}