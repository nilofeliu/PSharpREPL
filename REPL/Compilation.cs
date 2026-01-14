using REPL.language;

namespace REPL
{
    internal class Compilation
    {
        private object _syntaxTree;

        public Compilation(object syntaxTree)
        {
            _syntaxTree = syntaxTree;
        }

        public object Evaluate(Dictionary<VariableSymbol, object> variables)
        {
            // Dummy implementation for illustration purposes
            return null;
        }
    }
}