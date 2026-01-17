using REPL.systemfiles.settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPL.systemfiles.environments
{
    internal class VirtualEnv
    {
        public string EnvName { get; set; }

        private SysSettings SystemSettings = SysSettings.Instance;

        public VirtualEnv(string _name = "")
        {   
            if (string.IsNullOrEmpty(_name))
                EnvName = SystemSettings.HostDomain;
            else
                EnvName = _name;
        }

       
        public string GetEnvInfo()
        {
            return EnvName;
        }
    }
}
