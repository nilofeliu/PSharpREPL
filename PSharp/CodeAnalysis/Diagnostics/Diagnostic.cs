using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Text;

namespace PSharp.CodeAnalysis.Diagnostics;

public sealed class Diagnostic
{
    internal Diagnostic(TextSpan span, string message, ErrorCode code,
                        DiagnosticSeverity severity = DiagnosticSeverity.Error)
    {
        Message = message;
        Code = code;
        Severity = severity;
        Location = new Location(span);
    }

    public string Message { get; }
    public ErrorCode Code { get; }
    public DiagnosticSeverity Severity { get; }
    public Location Location { get; }
    public TextSpan Span => Location.Span;

    public override string ToString() => $"[{Code}] {Message}";

    public static Diagnostic FromInfo(DiagnosticInfo info, TextSpan span)
    {
        var finalSpan = info.OverrideSpan ?? span;

        return new Diagnostic(
            finalSpan,
            info.Message,
            info.Code,
            info.Severity);
    }
}