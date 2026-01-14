using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPL.system.settings
{
    internal class SessionSettings
    {
        private static readonly SessionSettings _instance = new();
        internal static SessionSettings Instance => _instance;
    }
}
