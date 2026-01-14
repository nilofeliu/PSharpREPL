using System.Collections.Immutable;

namespace REPL.language.Syntax
{
    internal class Parser
    {
        private string _text;

        public Parser(string text)
        {
            _text = text;
        }

        public string Parse()
        {
            // Dummy implementation for illustration purposes
            return null;
        }
    }
}