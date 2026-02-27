namespace PSharp.CodeAnalysis.Syntax.Parser;

/// <summary>
/// Simple wrapper for array elements to avoid covariance issues (pattern from Roslyn).
/// </summary>
internal struct ArrayElement<T>
{
    public T Value;
}
