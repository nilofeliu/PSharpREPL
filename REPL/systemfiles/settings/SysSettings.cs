using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;


namespace REPL.systemfiles.settings
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

        private string GetHostDomain()
        {
            return Environment.UserDomainName;
        }

        private PhysicalAddress GetMacAddress()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                    return nic.GetPhysicalAddress();
            }
            return PhysicalAddress.None;
        }

               
        private IPAddress GetLocalIP()
        {
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip;
            }
            return IPAddress.None;
        }

        private Version GetOSVersion()
        {
            return Environment.OSVersion.Version;
        }

        private string GetOSUser()
        {
            return Environment.UserName;
        }


    }
}
