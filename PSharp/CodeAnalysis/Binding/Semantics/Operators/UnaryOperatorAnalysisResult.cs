using PSharp.CodeAnalysis.Binding.Semantics.Conversions;

namespace PSharp.CodeAnalysis.Binding.Semantics.Operators;


internal struct UnaryOperatorAnalysisResult
{
    public UnaryOperatorSignature Signature { get; }
    public Conversion OperandConversion { get; }
    public bool IsValid { get; }

    private UnaryOperatorAnalysisResult(
        UnaryOperatorSignature signature,
        Conversion operandConversion,
        bool isValid)
    {
        Signature = signature;
        OperandConversion = operandConversion;
        IsValid = isValid;
    }

    public static UnaryOperatorAnalysisResult Valid(
        UnaryOperatorSignature signature,
        Conversion operandConversion) =>
        new(signature, operandConversion, true);

    public static UnaryOperatorAnalysisResult Invalid() =>
        new(default, Conversion.NoConversion, false);
}