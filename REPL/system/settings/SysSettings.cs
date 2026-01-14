using System.Net;
using System.Net.NetworkInformation;

namespace REPL.system.settings
{
    internal class SysSettings
    {
        private static readonly SysSettings _instance = new();
        internal static SysSettings Instance => _instance;

        private PhysicalAddress _macAddress;
        private IPAddress _localHost;
        private Version _osVersion;
        private string _hostUser;
        private string _hostDomain;

        public PhysicalAddress MacAdress
        {
            get => _macAddress;
        }

        public IPAddress LocalHost
        {
            get => _localHost;
        }

        public Version OSVersion
        {
            get => _osVersion;
        }

        public string HostUser
        {
            get => _hostUser;
        }

        public string HostDomain
        {
            get => _hostDomain;
        }

        public SysSettings()
        {
            _hostDomain = GetHostDomain();
            _hostUser = GetOSUser();
            _osVersion = GetOSVersion();
            _localHost = GetLocalIP();
            _macAddress = GetMacAddress();
        }

        private System.String GetHostDomain()
        {
            return System.Environment.UserDomainName;
        }

        private System.Net.NetworkInformation.PhysicalAddress GetMacAddress()
        {
            foreach (var nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    return nic.GetPhysicalAddress();
            }
            return System.Net.NetworkInformation.PhysicalAddress.None;
        }

        private System.Net.IPAddress GetLocalIP()
        {
            foreach (var ip in System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip;
            }
            return System.Net.IPAddress.None;
        }

        private System.Version GetOSVersion()
        {
            return System.Environment.OSVersion.Version;
        }

        private System.String GetOSUser()
        {
            return System.Environment.UserName;
        }


    }
}
