using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Semantics.Conversions;

internal sealed class ConversionResolver : ConversionsBase
{
    private readonly BuiltInConversions _builtIn;

    public ConversionResolver()
    {
        _builtIn = new BuiltInConversions();
    }

    public override Conversion ClassifyConversion(TypeSymbol source, TypeSymbol target)
    {
        if (source == target)
            return Conversion.Identity;

        // Handle primitive conversions
        //if (source is SpecialTypeData srcSpecial && target is SpecialTypeData tgtSpecial)
        if (source.SpecialType != SpecialType.None && target.SpecialType != SpecialType.None)
        {
            var srcSpecial = source.SpecialType;
            var tgtSpecial = target.SpecialType;
            return _builtIn.ClassifyBuiltInConversion(srcSpecial, tgtSpecial);
        }
        // Future: reference conversions, boxing, etc.

        return Conversion.NoConversion;
    }

    public override Conversion ClassifyImplicitConversion(TypeSymbol source, TypeSymbol target)
    {
        var conversion = ClassifyConversion(source, target);
        return conversion.IsImplicit ? conversion : Conversion.NoConversion;
    }

    public override Conversion ClassifyExplicitConversion(TypeSymbol source, TypeSymbol target)
    {
        var conversion = ClassifyConversion(source, target);
        return conversion.Exists ? conversion : Conversion.NoConversion;
    }
}