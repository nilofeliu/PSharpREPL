using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Binding.Semantics.Operators;
using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Binding.Expressions;

internal sealed class BoundUnaryOperator
{
    private static readonly UnaryOperatorOverloadResolution _overloadResolution = new();

    private BoundUnaryOperator(UnaryOperatorSignature signature)
    {
        Signature = signature;
    }

    public UnaryOperatorSignature Signature { get; }
    public UnaryOperatorKind Kind => Signature.Kind;
    public TypeSymbol OperandType => Signature.OperandType;
    public TypeSymbol Type => Signature.ReturnType;

    public static BoundUnaryOperator Bind(SyntaxKind syntaxKind, TypeSymbol operandType)
    {
        var result = _overloadResolution.Resolve(syntaxKind, operandType);
        return result.IsValid ? new BoundUnaryOperator(result.Signature) : null;
    }
}

