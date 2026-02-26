using PSharp.CodeAnalysis.Binding.Semantics.Conversions;

namespace PSharp.CodeAnalysis.Binding.Semantics.Operators;

internal struct BinaryOperatorAnalysisResult
{
    public BinaryOperatorSignature Signature { get; }
    public Conversion LeftConversion { get; }
    public Conversion RightConversion { get; }
    public bool IsValid { get; }

    private BinaryOperatorAnalysisResult(
        BinaryOperatorSignature signature,
        Conversion leftConversion,
        Conversion rightConversion,
        bool isValid)
    {
        Signature = signature;
        LeftConversion = leftConversion;
        RightConversion = rightConversion;
        IsValid = isValid;
    }

    public static BinaryOperatorAnalysisResult Valid(
        BinaryOperatorSignature signature,
        Conversion leftConversion,
        Conversion rightConversion) =>
        new(signature, leftConversion, rightConversion, true);

    public static BinaryOperatorAnalysisResult Invalid() =>
        new(null, Conversion.NoConversion, Conversion.NoConversion, false);
}


