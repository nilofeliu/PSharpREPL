using REPL.interfaces;
using System.Security.Principal;


namespace REPL.systemfiles.users
{
    internal class User : IUser
    {
        private readonly UserData _userdata;
        private bool _isLoggedIn = false;
        public string UserName => _userdata.Username;
        public bool IsLoggedIn => _isLoggedIn;

        IUserData IUser.UserInfo => _userdata;

        bool IUser.IsLoggedIn => IsLoggedIn;

        public User(string username)
        {
            string osUserName = username;
            _userdata = new UserData(osUserName);
            _isLoggedIn = true; // Assuming the OS user is always logged in
        }


        public User(UserData userData)
        {
            _userdata = userData;
        }
    }
}