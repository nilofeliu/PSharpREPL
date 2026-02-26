using PSharp.CodeAnalysis.Compilations;

namespace PSharp.CodeAnalysis.Symbols;

public static class TypeSymbolExtensions
{
    public static TypeSymbol? GetTypeSymbol(this object value)
        => Compilation.GetSpecialTypes().FirstOrDefault(t => t.ClrType == value.GetType());

}