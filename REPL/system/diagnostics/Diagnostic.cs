using REPL.interfaces;
using REPL.language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPL.system.diagnostics
{
    internal class Diagnostic : IDiagnostic
    {
        public Diagnostic(TextSpan span, string message)
        {
            Message = message;
            Span = span;
        }
        public string Message { get; }
        public TextSpan Span { get; }

        public override string ToString() => Message;
    }
}
