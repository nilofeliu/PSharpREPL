using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Compilations
{
    public partial class Compilation
    {
        public static TypeSymbol typeOf(SpecialType type)
            => _lazySpecialTypes[(int)type];

        public static TypeSymbol? typeOf(string name)
            => _lazySpecialTypes.FirstOrDefault(t => t?.Name == name);

        public static TypeSymbol? typeOf(TypeKind typeKind)
            => _lazySpecialTypes.FirstOrDefault(t => t?.TypeKind == typeKind);

        //public static TypeSymbol? typeOf(SyntaxKind syntaxKind)
        //    => _lazySpecialTypes.FirstOrDefault(t => t?.SyntaxType == syntaxKind);

        public static bool IsNumericPrimitive(SpecialType specialType)
            => typeOf(specialType)?.IsNumeric ?? false;


        //public bool IsNumericPrimitive<T>()
        //    => CompilationTypeResolution.IsNumericPrimitive<T>();
    }
}
