using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Binding.Semantics.Conversions;
using PSharp.CodeAnalysis.Compilations;
using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Binding.Semantics.Operators;

internal sealed class UnaryOperatorOverloadResolution
{
    private static readonly ConversionResolver _conversions = new ConversionResolver();

    private static readonly UnaryOperatorSignature[] _builtInOperators =
    {
        new(UnaryOperatorKind.LogicalNegation, Compilation.typeOf(SpecialType.System_Boolean),   Compilation.typeOf(SpecialType.System_Boolean)),

        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_Byte),    Compilation.typeOf(SpecialType.System_Byte)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_SByte),   Compilation.typeOf(SpecialType.System_SByte)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_Int16),   Compilation.typeOf(SpecialType.System_Int16)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_UInt16),  Compilation.typeOf(SpecialType.System_UInt16)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_Int32),     Compilation.typeOf(SpecialType.System_Int32)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_UInt32),    Compilation.typeOf(SpecialType.System_UInt32)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_Int64),    Compilation.typeOf(SpecialType.System_Int64)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_UInt64),   Compilation.typeOf(SpecialType.System_UInt64)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_Single),   Compilation.typeOf(SpecialType.System_Single)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_Double),  Compilation.typeOf(SpecialType.System_Double)),
        new(UnaryOperatorKind.Identity, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Decimal)),

        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_Byte),    Compilation.typeOf(SpecialType.System_Byte)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_SByte),   Compilation.typeOf(SpecialType.System_SByte)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_Int16),   Compilation.typeOf(SpecialType.System_Int16)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_UInt16),  Compilation.typeOf(SpecialType.System_UInt16)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_Int32),     Compilation.typeOf(SpecialType.System_Int32)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_UInt32),    Compilation.typeOf(SpecialType.System_UInt32)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_Int64),    Compilation.typeOf(SpecialType.System_Int64)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_UInt64),   Compilation.typeOf(SpecialType.System_UInt64)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_Single),   Compilation.typeOf(SpecialType.System_Single)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_Double),  Compilation.typeOf(SpecialType.System_Double)),
        new(UnaryOperatorKind.Negation, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Decimal)),

        new(UnaryOperatorKind.OnesComplement, Compilation.typeOf(SpecialType.System_Byte),   Compilation.typeOf(SpecialType.System_Byte)),
        new(UnaryOperatorKind.OnesComplement, Compilation.typeOf(SpecialType.System_SByte),  Compilation.typeOf(SpecialType.System_SByte)),
        new(UnaryOperatorKind.OnesComplement, Compilation.typeOf(SpecialType.System_Int16),  Compilation.typeOf(SpecialType.System_Int16)),
        new(UnaryOperatorKind.OnesComplement, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_UInt16)),
        new(UnaryOperatorKind.OnesComplement, Compilation.typeOf(SpecialType.System_Int32),    Compilation.typeOf(SpecialType.System_Int32)),
        new(UnaryOperatorKind.OnesComplement, Compilation.typeOf(SpecialType.System_UInt32),   Compilation.typeOf(SpecialType.System_UInt32)),
        new(UnaryOperatorKind.OnesComplement, Compilation.typeOf(SpecialType.System_Int64),   Compilation.typeOf(SpecialType.System_Int64)),
        new(UnaryOperatorKind.OnesComplement, Compilation.typeOf(SpecialType.System_UInt64),  Compilation.typeOf(SpecialType.System_UInt64)),
    };

    public UnaryOperatorAnalysisResult Resolve(SyntaxKind syntaxKind, TypeSymbol operandType)
    {
        var candidates = GetCandidates(syntaxKind);

        foreach (var signature in candidates)
        {
            var conversion = _conversions.ClassifyConversion(operandType, signature.OperandType);
            if (conversion.Exists && conversion.IsImplicit)
                return UnaryOperatorAnalysisResult.Valid(signature, conversion);
        }

        return UnaryOperatorAnalysisResult.Invalid();
    }

    private static IEnumerable<UnaryOperatorSignature> GetCandidates(SyntaxKind syntaxKind)
    {
        var kind = SyntaxKindToOperatorKind(syntaxKind);
        foreach (var op in _builtInOperators)
            if (op.Kind == kind)
                yield return op;
    }

    private static UnaryOperatorKind SyntaxKindToOperatorKind(SyntaxKind syntaxKind) =>
        syntaxKind switch
        {
            SyntaxKind.BangToken => UnaryOperatorKind.LogicalNegation,
            SyntaxKind.PlusToken => UnaryOperatorKind.Identity,
            SyntaxKind.MinusToken => UnaryOperatorKind.Negation,
            SyntaxKind.TildeToken => UnaryOperatorKind.OnesComplement,
            _ => throw new Exception($"Unexpected unary syntax kind {syntaxKind}")
        };
}
