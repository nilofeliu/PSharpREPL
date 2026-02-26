using PSharp.CodeAnalysis.Text;

namespace PSharp.CodeAnalysis.Diagnostics
{
    public class DiagnosticInfo
    {
        public ErrorCode Code { get; }
        public string Message { get; }
        public DiagnosticSeverity Severity { get; }
        public TextSpan? OverrideSpan { get; }

        public DiagnosticInfo(ErrorCode code, DiagnosticSeverity severity = DiagnosticSeverity.Error)
            : this(code, severity, null, Array.Empty<object>()) { }

        public DiagnosticInfo(ErrorCode code, DiagnosticSeverity severity, params object[] args)
            : this(code, severity, null, args) { }

        public DiagnosticInfo(ErrorCode code, DiagnosticSeverity severity, TextSpan? overrideSpan, params object[] args)
        {
            Code = code;
            Severity = severity;
            OverrideSpan = overrideSpan;
            Message = ErrorCodeMessages.GetMessage(code, args);
        }
    }
}