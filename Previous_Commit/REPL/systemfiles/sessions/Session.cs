using REPL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPL.systemfiles.sessions
{
    internal class Session : ISession
    {

        private Guid _sessionID;
        private string _sessionName;
        private IUserData _user;
        private bool _isActive;
        private DateTime _sessionTimeout;
        private DateTime _sessionTimeStart;
        private DateTime _sessionTimeEnd;
        private DateTime _sessionTimeoutStart;
        private DateTime _sessionTimeoutEnd;


        public Guid SessionID => throw new NotImplementedException();
        public string SessionName => _sessionName;

        public IUserData User => _user;

        public bool IsActive => _isActive;

        public DateTime SessionTimeout => _sessionTimeout;

        public DateTime SessionTimeStart => _sessionTimeStart;

   
        public DateTime SessionTimeEnd
        {
            get { return _sessionTimeEnd; }
            set { _sessionTimeEnd = value; }
        }

        public Session(string sessionName, IUserData user)
        {
            _sessionID = Guid.NewGuid();
            _sessionName = sessionName;
            _user = user;
            _isActive = true;
            _sessionTimeStart = DateTime.Now;

        }

        public TimeSpan GetSessionTime()
        {
            return SessionTimeEnd - SessionTimeStart;
        }

        public void SetTimeoutSession(TimeSpan timeoutDuration)
        {
            _sessionTimeoutStart = DateTime.Now;
            _sessionTimeoutEnd = _sessionTimeoutStart.Add(timeoutDuration);
            _sessionTimeout = _sessionTimeoutEnd;
        }

        public void EndSession()
        {
            SessionTimeEnd = DateTime.Now;
        }




    }
}
