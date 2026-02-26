namespace PSharp.CodeAnalysis.Binding.Semantics.Conversions;

internal sealed class BuiltInConversions
{
    private static readonly bool[,] _implicitNumeric = new bool[50, 50];

    static BuiltInConversions()
    {
        // From byte (unsigned 8-bit)
        Allow(SpecialType.System_Byte, SpecialType.System_Int16);
        Allow(SpecialType.System_Byte, SpecialType.System_UInt16);
        Allow(SpecialType.System_Byte, SpecialType.System_Int32);
        Allow(SpecialType.System_Byte, SpecialType.System_UInt32);
        Allow(SpecialType.System_Byte, SpecialType.System_Int64);
        Allow(SpecialType.System_Byte, SpecialType.System_UInt64);
        Allow(SpecialType.System_Byte, SpecialType.System_Single);
        Allow(SpecialType.System_Byte, SpecialType.System_Double);
        Allow(SpecialType.System_Byte, SpecialType.System_Decimal);

        // From sbyte (signed 8-bit)
        Allow(SpecialType.System_SByte, SpecialType.System_Int16);
        Allow(SpecialType.System_SByte, SpecialType.System_Int32);
        Allow(SpecialType.System_SByte, SpecialType.System_Int64);
        Allow(SpecialType.System_SByte, SpecialType.System_Single);
        Allow(SpecialType.System_SByte, SpecialType.System_Double);
        Allow(SpecialType.System_SByte, SpecialType.System_Decimal);

        // From short (signed 16-bit)
        Allow(SpecialType.System_Int16, SpecialType.System_Int32);
        Allow(SpecialType.System_Int16, SpecialType.System_Int64);
        Allow(SpecialType.System_Int16, SpecialType.System_Single);
        Allow(SpecialType.System_Int16, SpecialType.System_Double);
        Allow(SpecialType.System_Int16, SpecialType.System_Decimal);

        // From ushort (unsigned 16-bit)
        Allow(SpecialType.System_UInt16, SpecialType.System_Int32);
        Allow(SpecialType.System_UInt16, SpecialType.System_UInt32);
        Allow(SpecialType.System_UInt16, SpecialType.System_Int64);
        Allow(SpecialType.System_UInt16, SpecialType.System_UInt64);
        Allow(SpecialType.System_UInt16, SpecialType.System_Single);
        Allow(SpecialType.System_UInt16, SpecialType.System_Double);
        Allow(SpecialType.System_UInt16, SpecialType.System_Decimal);

        // From int (signed 32-bit)
        Allow(SpecialType.System_Int32, SpecialType.System_Int64);
        Allow(SpecialType.System_Int32, SpecialType.System_Single);
        Allow(SpecialType.System_Int32, SpecialType.System_Double);
        Allow(SpecialType.System_Int32, SpecialType.System_Decimal);

        // From uint (unsigned 32-bit)
        Allow(SpecialType.System_UInt32, SpecialType.System_Int64);
        Allow(SpecialType.System_UInt32, SpecialType.System_UInt64);
        Allow(SpecialType.System_UInt32, SpecialType.System_Single);
        Allow(SpecialType.System_UInt32, SpecialType.System_Double);
        Allow(SpecialType.System_UInt32, SpecialType.System_Decimal);

        // From long (signed 64-bit)
        Allow(SpecialType.System_Int64, SpecialType.System_Single);
        Allow(SpecialType.System_Int64, SpecialType.System_Double);
        Allow(SpecialType.System_Int64, SpecialType.System_Decimal);

        // From ulong (unsigned 64-bit)
        Allow(SpecialType.System_UInt64, SpecialType.System_Single);
        Allow(SpecialType.System_UInt64, SpecialType.System_Double);
        Allow(SpecialType.System_UInt64, SpecialType.System_Decimal);

        // From float
        Allow(SpecialType.System_Single, SpecialType.System_Double);

        // From char (16-bit unsigned)
        Allow(SpecialType.System_Char, SpecialType.System_UInt16);
        Allow(SpecialType.System_Char, SpecialType.System_Int32);
        Allow(SpecialType.System_Char, SpecialType.System_UInt32);
        Allow(SpecialType.System_Char, SpecialType.System_Int64);
        Allow(SpecialType.System_Char, SpecialType.System_UInt64);
        Allow(SpecialType.System_Char, SpecialType.System_Single);
        Allow(SpecialType.System_Char, SpecialType.System_Double);
        Allow(SpecialType.System_Char, SpecialType.System_Decimal);
    }

    private static void Allow(SpecialType from, SpecialType to)
    {
        _implicitNumeric[(int)from, (int)to] = true;
    }

    public Conversion ClassifyBuiltInConversion(SpecialType source, SpecialType target)
    {
        if (source == target)
            return Conversion.Identity;

        if (!IsNumericPrimitive(source) || !IsNumericPrimitive(target))
            return Conversion.NoConversion;

        return _implicitNumeric[(int)source, (int)target]
            ? Conversion.CreateImplicitNumeric()
            : Conversion.NoConversion;
    }

    private static bool IsNumericPrimitive(SpecialType type)
    {
        return type switch
        {
            SpecialType.System_Byte or SpecialType.System_SByte or
            SpecialType.System_Int16 or SpecialType.System_UInt16 or
            SpecialType.System_Int32 or SpecialType.System_UInt32 or
            SpecialType.System_Int64 or SpecialType.System_UInt64 or
            SpecialType.System_Single or SpecialType.System_Double or
            SpecialType.System_Decimal or SpecialType.System_Char => true,
            _ => false
        };
    }
}