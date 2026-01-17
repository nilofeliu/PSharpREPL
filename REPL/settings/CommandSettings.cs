using REPL.language.Syntax.expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPL.settings
{
    internal class CommandSettings
    {
        private static readonly CommandSettings _instance = new();
        internal static CommandSettings Instance => _instance;

        private char _commandPrefix;
        public char CommandPrefix
        {
            get { return _commandPrefix; }
            set { _commandPrefix = value; }
        }

        public CommandSettings()
        {
            _commandPrefix = '.';
        }

    }
}
