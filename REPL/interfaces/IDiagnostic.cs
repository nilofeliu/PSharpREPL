using REPL.language;

namespace REPL.interfaces
{
    internal interface IDiagnostic
    {
        string Message { get; }
        TextSpan Span { get; }

        string ToString();
    }
}