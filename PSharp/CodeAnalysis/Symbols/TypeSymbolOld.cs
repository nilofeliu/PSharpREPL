namespace PSharp.CodeAnalysis.Symbols;
public sealed class TypeSymbolOld : Symbol
{
    public TypeSymbolOld(SpecialType specialType, TypeKind typeKind, string name, Type clrType)
        :base (name)
    {
        SpecialType = specialType;
        TypeKind = typeKind;
        ClrType = clrType;
        name = base.Name;
    }

    private readonly string _name;
    public SpecialType SpecialType { get; }
    public TypeKind TypeKind { get; }
    public Type ClrType { get; }
    public bool IsValueType => ClrType.IsValueType;
    public bool IsReferenceType => !ClrType.IsValueType;

    public override SymbolKind Kind => SymbolKind.NamedType;
    public override string Name => base.Name;
}