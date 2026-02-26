using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Semantics.Operators;

internal sealed class BinaryOperatorSignature
{
    public BinaryOperatorKind Kind { get; }
    public TypeSymbol LeftType { get; }
    public TypeSymbol RightType { get; }
    public TypeSymbol ReturnType { get; }
    public bool IsBuiltIn { get; }

    public BinaryOperatorSignature(
        BinaryOperatorKind kind,
        TypeSymbol leftType,
        TypeSymbol rightType,
        TypeSymbol returnType,
        bool isBuiltIn = true)
    {
        Kind = kind;
        LeftType = leftType;
        RightType = rightType;
        ReturnType = returnType;
        IsBuiltIn = isBuiltIn;
    }
}
