using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Semantics.Operators;


internal readonly struct UnaryOperatorSignature
{
    public UnaryOperatorSignature(UnaryOperatorKind kind, TypeSymbol operandType, TypeSymbol returnType)
    {
        Kind = kind;
        OperandType = operandType;
        ReturnType = returnType;
    }

    public UnaryOperatorKind Kind { get; }
    public TypeSymbol OperandType { get; }
    public TypeSymbol ReturnType { get; }
}
