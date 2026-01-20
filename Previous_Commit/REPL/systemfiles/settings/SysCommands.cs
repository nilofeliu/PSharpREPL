namespace REPL.systemfiles.settings
{
    internal class SysInfo
    {

        private static readonly SysInfo _instance = new();
        internal static SysInfo Instance => _instance;

        SysSettings SystemSettings = SysSettings.Instance;

        internal void ShowSysData()
        {
            Console.WriteLine("-------- System Information --------");
            Console.WriteLine($"OS Version: {SystemSettings.OSVersion} ");
            Console.WriteLine($"Host User: {SystemSettings.HostUser} ");
            Console.WriteLine($"Host Domain: {SystemSettings.HostDomain} ");
            Console.WriteLine($"Local IP: {SystemSettings.LocalHost} ");
            Console.WriteLine($"MAC Address: {SystemSettings.MacAdress} ");
            Console.WriteLine("------------------------------------");

        }
    }
}

