namespace PSharp.CodeAnalysis.Binding.Semantics.Conversions;

public readonly struct Conversion : IEquatable<Conversion>
{
    public static readonly Conversion NoConversion = new(ConversionKind.NoConversion);
    public static readonly Conversion Identity = new(ConversionKind.Identity);
    public static readonly Conversion ImplicitNumeric = new(ConversionKind.ImplicitNumeric);
    public static readonly Conversion ExplicitNumeric = new(ConversionKind.ExplicitNumeric);
    public static readonly Conversion Boxing = new(ConversionKind.Boxing);
    // Add others as needed

    public ConversionKind Kind { get; }

    // Properties for overload resolution
    public bool Exists => Kind != ConversionKind.NoConversion;
    public bool IsIdentity => Kind == ConversionKind.Identity;
    public bool IsImplicit => Kind == ConversionKind.Identity ||
                              Kind == ConversionKind.ImplicitNumeric ||
                              Kind == ConversionKind.ImplicitEnumeration ||
                              Kind == ConversionKind.ImplicitNullable ||
                              Kind == ConversionKind.ImplicitReference ||
                              Kind == ConversionKind.Boxing ||
                              Kind == ConversionKind.ImplicitConstant ||
                              Kind == ConversionKind.UserDefinedImplicit;

    public bool IsExplicit => !IsImplicit && Exists;
    public bool IsNumeric => Kind == ConversionKind.ImplicitNumeric ||
                            Kind == ConversionKind.ExplicitNumeric;

    // For betterness rules
    public bool IsBetterThan(Conversion other)
    {
        if (IsIdentity && !other.IsIdentity) return true;
        if (!IsIdentity && other.IsIdentity) return false;
        if (IsImplicit && other.IsExplicit) return true;
        if (IsExplicit && other.IsImplicit) return false;

        // Numeric promotion hierarchy
        // Add more rules here

        return false;
    }

    private Conversion(ConversionKind kind)
    {
        Kind = kind;
    }

    public static Conversion CreateImplicitNumeric() => new(ConversionKind.ImplicitNumeric);
    public static Conversion CreateExplicitNumeric() => new(ConversionKind.ExplicitNumeric);
    public static Conversion CreateBoxing() => new(ConversionKind.Boxing);

    // Factory methods for other conversion kinds

    public override string ToString() => Kind.ToString();
    public bool Equals(Conversion other) => Kind == other.Kind;
    public override bool Equals(object? obj) => obj is Conversion c && Equals(c);
    public override int GetHashCode() => (int)Kind;
}