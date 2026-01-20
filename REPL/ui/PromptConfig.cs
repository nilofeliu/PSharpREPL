using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static REPL.systemfiles.settings.PromptSettings;
using REPL.utils;
using REPL.systemfiles.environments;
using REPL.systemfiles.users;
using REPL.systemfiles.settings;

namespace REPL.ui
{
    internal class PromptStream
    {
        private UserData _userData;
        private VirtualEnv _virtualEnv;
        private static ConsoleColor _defaultColor = ConsoleColor.White;
        private static string promptPreffix = "#";
        private static string promptsuffix = ">";

        private SysSettings SystemSettings = SysSettings.Instance;
        private readonly StringBuilder _builder;

        public PromptStream(StringBuilder builder, UserData userData, VirtualEnv virtualEnv)
        {
            if (userData == null)
                _userData = new UserData(SystemSettings.HostUser);
            else
                _userData = userData;


            if (virtualEnv == null)
                _virtualEnv = new VirtualEnv(SystemSettings.HostDomain);
            else
                _virtualEnv = virtualEnv;
            _builder = builder;
        }

        public PromptStream(UserData userData)
        {
            _userData = userData;
                
        }

        internal void Write()
        {
            if (_builder.Length == 0)
            {
                if (HasDate)
                {
                    ConsoleColored.Write(DateTime.Now.ToString($"<{DateTimeFormat}>"));
                }
                if (HasTime)
                {
                    ConsoleColored.Write(DateTime.Now.ToString($" {DateTimeFormat}"));
                }
                if (HasUserName)
                {
                    ConsoleColored.Write($"{_userData.Username.ToLower()}", UserNameColor);
                }
                if (HasEnvironment)
                {
                    ConsoleColored.Write($"<@{_virtualEnv.EnvName.ToLower()}>", EnvColor);
                }
                ConsoleColored.Write($" {promptPreffix}");
                ConsoleColored.Write(promptsuffix);
                ConsoleColored.Write(" ");
            }
            else
                Console.Write("|> ");
        }


    }


}
