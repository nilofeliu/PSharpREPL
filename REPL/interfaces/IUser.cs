using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPL.interfaces
{

    internal interface IUser
    {
        internal IUserData UserInfo { get; }
        internal bool IsLoggedIn { get; }
    }
}
