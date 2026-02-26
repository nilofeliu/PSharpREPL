using System.Runtime.InteropServices;

namespace PSharp.CodeAnalysis.Symbols;

public  class TypeSymbol : Symbol
{
    public Type ClrType { get; }
    public SpecialType SpecialType { get; }
    public TypeKind TypeKind { get; }
    public int? Size { get; }
    public bool IsValueType => ClrType.IsValueType;
    public bool IsReferenceType => !ClrType.IsValueType;
    public bool IsNumeric { get; }
    public bool IsIntegral { get; }
    public bool IsFloatingPoint { get; }
    public bool IsSigned { get; }
    public bool IsUnsigned { get; }

    public override SymbolKind Kind => throw new NotImplementedException();

    public TypeSymbol(SpecialType specialType, TypeKind typeKind,  string name, Type clrType,
        bool isNumeric = false, bool isIntegral = false, bool isFloatingPoint = false,
        bool isSigned = false, bool isUnsigned = false)
        : base(name)
    {
        ClrType = clrType;
        SpecialType = specialType;
        TypeKind = typeKind;
        Size = clrType.IsValueType ? Marshal.SizeOf(clrType) : null;
        IsNumeric = isNumeric;
        IsIntegral = isIntegral;
        IsFloatingPoint = isFloatingPoint;
        IsSigned = isSigned;
        IsUnsigned = isUnsigned;
    }

    public override bool Equals(object? obj)
    => obj is TypeSymbol other && SpecialType == other.SpecialType && TypeKind == other.TypeKind;

    public override int GetHashCode()
        => HashCode.Combine(SpecialType, TypeKind);

    public static bool operator ==(TypeSymbol? left, TypeSymbol? right)
        => left?.Equals(right) ?? right is null;

    public static bool operator !=(TypeSymbol? left, TypeSymbol? right)
        => !(left == right);


}