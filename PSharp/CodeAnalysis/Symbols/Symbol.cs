namespace PSharp.CodeAnalysis.Symbols;

public abstract class Symbol
{
    private protected Symbol(string name)
    {
        Name = name;
    }
    public virtual string Name { get; }
    public abstract SymbolKind Kind { get; }

    public override string ToString() => Name;


    // Under Development - Not yet implemented

    //public virtual Accessibility DeclaredAccessibility { get; }
    //public virtual bool IsStatic { get; }
    //public virtual bool IsAbstract { get; }
    //public virtual bool IsSealed { get; }
    //public virtual bool IsOverride { get; }
    //public virtual bool IsVirtual { get; }
    //public virtual bool IsExtern { get; }

    //public virtual ISymbol ContainingSymbol { get; }
    //public virtual INamespaceSymbol ContainingNamespace { get; }
    //public virtual INamedTypeSymbol ContainingType { get; }
    //public virtual IAssemblySymbol ContainingAssembly { get; }
    //public virtual ImmutableArray<Location> Locations { get; }
    //public virtual ImmutableArray<SyntaxReference> DeclaringSyntaxReferences { get; }
}