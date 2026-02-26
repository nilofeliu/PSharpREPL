namespace PSharp.CodeAnalysis.Binding.Semantics.Conversions;

public enum ConversionKind
{
    NoConversion = 0,
    Identity,
    ImplicitNumeric,
    ExplicitNumeric,
    ImplicitEnumeration,
    ExplicitEnumeration,
    ImplicitNullable,
    ExplicitNullable,
    ImplicitReference,
    ExplicitReference,
    Boxing,
    Unboxing,
    ImplicitConstant,
    UserDefinedImplicit,
    UserDefinedExplicit
}
