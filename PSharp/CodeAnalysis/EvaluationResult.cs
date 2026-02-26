using PSharp.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis
{
    public sealed class EvaluationResult
    {
        public EvaluationResult(ImmutableArray<Diagnostic> diagnostics, object value)
        {
            Value = value;
            Diagnostics = diagnostics;
        }
        public object Value { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }
    }
}
