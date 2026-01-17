using REPL.interfaces;
using System.Security.Principal;

namespace REPL.systemfiles.users
{
    internal class UserData : IUserData
    {  
        private readonly string _username;
        private readonly int _userId;
        private readonly int _userLevel;
        public string Username => _username;

        public int UserID => _userId;

        public int UserLevel => _userLevel;

        public UserData(string username, int userId, int userLevel)
        {
            _username = username;
            _userId = userId;
            _userLevel = userLevel;
        }
        public UserData(string username, int userId)
        {
            _username = username;
            _userId = userId; // Default ID
            _userLevel = 0; // Default level
        }

        public UserData(string username)
        {
            _username = username;
            _userId = GenerateDefaultUserId();
            _userLevel = 0; // Default level
        }

        public UserData()
        {
            _username = GetOSUserName();
            _userId = GenerateDefaultUserId();
            _userLevel = 0; // Default level
        }

        private int GenerateDefaultUserId()
        {
            var id = Guid.NewGuid();
            return id.GetHashCode();
        }

        private string GetOSUserName()
        {
            var username = WindowsIdentity.GetCurrent().Name;
            // Returns: "DOMAIN\username" or "MACHINE\username" on non-domain machines

            // If you want just the username without domain/machine prefix:
            return username.Split('\\')[1];
        }
    } 
}
