using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Metadata;

internal static class SpecialTypes
{
    internal static List<TypeSymbol> LoadSpecialTypes()
    {
        return new List<TypeSymbol>
        {
            // Value types
            new(SpecialType.System_Char,     TypeKind.Struct, "char",    typeof(char)),
            new(SpecialType.System_Boolean,  TypeKind.Struct, "bool",    typeof(bool)),
            new(SpecialType.System_Byte,     TypeKind.Struct, "byte",    typeof(byte),    isNumeric: true, isIntegral: true, isUnsigned: true),
            new(SpecialType.System_SByte,    TypeKind.Struct, "sbyte",   typeof(sbyte),   isNumeric: true, isIntegral: true, isSigned: true),
            new(SpecialType.System_Int16,    TypeKind.Struct, "short",   typeof(short),   isNumeric: true, isIntegral: true, isSigned: true),
            new(SpecialType.System_UInt16,   TypeKind.Struct, "ushort",  typeof(ushort),  isNumeric: true, isIntegral: true, isUnsigned: true),
            new(SpecialType.System_Int32,    TypeKind.Struct, "int",     typeof(int),     isNumeric: true, isIntegral: true, isSigned: true),
            new(SpecialType.System_UInt32,   TypeKind.Struct, "uint",    typeof(uint),    isNumeric: true, isIntegral: true, isUnsigned: true),
            new(SpecialType.System_Int64,    TypeKind.Struct, "long",    typeof(long),    isNumeric: true, isIntegral: true, isSigned: true),
            new(SpecialType.System_UInt64,   TypeKind.Struct, "ulong",   typeof(ulong),   isNumeric: true, isIntegral: true, isUnsigned: true),
            new(SpecialType.System_Single,   TypeKind.Struct, "float",   typeof(float),   isNumeric: true, isFloatingPoint: true, isSigned: true),
            new(SpecialType.System_Double,   TypeKind.Struct, "double",  typeof(double),  isNumeric: true, isFloatingPoint: true, isSigned: true),
            new(SpecialType.System_Decimal,  TypeKind.Struct, "decimal", typeof(decimal), isNumeric: true, isFloatingPoint: true, isSigned: true),

            // Reference types
            new(SpecialType.System_String,   TypeKind.Class,  "string",  typeof(string)),
            new(SpecialType.System_Object,   TypeKind.Class,  "object",  typeof(object)),

            // Special
            new(SpecialType.System_Void,     TypeKind.Struct,   "void",    typeof(void)),

            // Core runtime types (with ince no keyword maps to them)
            new(SpecialType.System_Array,         TypeKind.Class,     "array",        typeof(Array)),
            new(SpecialType.System_Enum,          TypeKind.Class,     "enum",         typeof(Enum)),
            new(SpecialType.System_ValueType,     TypeKind.Class,     "valuetype",    typeof(ValueType)),
            new(SpecialType.System_Delegate,      TypeKind.Class,     "delegate",     typeof(Delegate)),
            new(SpecialType.System_MulticastDelegate, TypeKind.Class, "multicastdelegate", typeof(MulticastDelegate)),
            new(SpecialType.System_IntPtr,        TypeKind.Struct,    "intptr",       typeof(nint)),
            new(SpecialType.System_UIntPtr,       TypeKind.Struct,    "uintptr",      typeof(nuint)),
            //new(SpecialType.System_DateTime,      TypeKind.Struct,    "datetime",     typeof(DateTime)),
            //new(SpecialType.System_TypedReference, TypeKind.Struct,    "typedref",     typeof(TypedReference)),
            //new(SpecialType.System_ArgIterator,   TypeKind.Struct,    "argiterator",  typeof(ArgIterator)),
            //new(SpecialType.System_RuntimeArgumentHandle, TypeKind.Struct, "runtimearg", typeof(RuntimeArgumentHandle)),
            //new(SpecialType.System_RuntimeFieldHandle, TypeKind.Struct, "runtimefield", typeof(RuntimeFieldHandle)),
            //new(SpecialType.System_RuntimeMethodHandle, TypeKind.Struct, "runtimemethod", typeof(RuntimeMethodHandle)),
            //new(SpecialType.System_RuntimeTypeHandle, TypeKind.Struct, "runtimetype", typeof(RuntimeTypeHandle)),
            //new(SpecialType.System_IAsyncResult,  TypeKind.Interface, "iasyncresult", typeof(IAsyncResult)),
            //new(SpecialType.System_AsyncCallback, TypeKind.Delegate, "asynccallback", typeof(AsyncCallback)),
            //new(SpecialType.System_IDisposable,   TypeKind.Interface, "idisposable", typeof(IDisposable)),
            //new(SpecialType.System_Collections_IEnumerable, TypeKind.Interface, "ienumerable", typeof(System.Collections.IEnumerable)),
            //new(SpecialType.System_Collections_Generic_IEnumerable_T, TypeKind.Interface, "ienumerable_t", typeof(System.Collections.Generic.IEnumerable<>)),
            //new(SpecialType.System_Collections_Generic_IEnumerator_T, TypeKind.Interface, "ienumerator_t", typeof(System.Collections.Generic.IEnumerator<>)),
            //new(SpecialType.System_Collections_IEnumerator, TypeKind.Interface, "ienumerator", typeof(System.Collections.IEnumerator)),
            //new(SpecialType.System_Collections_Generic_IList_T, TypeKind.Interface, "ilist_t", typeof(System.Collections.Generic.IList<>)),
            //new(SpecialType.System_Collections_Generic_ICollection_T, TypeKind.Interface, "icollection_t", typeof(System.Collections.Generic.ICollection<>)),
            //new(SpecialType.System_Collections_Generic_IReadOnlyList_T, TypeKind.Interface, "ireadonlylist_t", typeof(System.Collections.Generic.IReadOnlyList<>)),
            //new(SpecialType.System_Collections_Generic_IReadOnlyCollection_T, TypeKind.Interface, "ireadonlycollection_t", typeof(System.Collections.Generic.IReadOnlyCollection<>)),
            //new(SpecialType.System_Nullable_T,    TypeKind.Struct,    "nullable_t",   typeof(System.Nullable<>)),
            //new(SpecialType.System_Runtime_CompilerServices_IsVolatile, TypeKind.Class, "isvolatile", typeof(System.Runtime.CompilerServices.IsVolatile)),
            //new(SpecialType.System_Runtime_CompilerServices_RuntimeFeature, TypeKind.Class, "runtimefeature", typeof(System.Runtime.CompilerServices.RuntimeFeature)),
            //new(SpecialType.System_Runtime_CompilerServices_PreserveBaseOverridesAttribute, TypeKind.Class, "preservebaseoverrides", typeof(System.Runtime.CompilerServices.PreserveBaseOverridesAttribute)),
        };
    }

    internal static List<TypeSymbol> LoadTypeKinds()
    {
        return new List<TypeSymbol>
        {
            new(SpecialType.None, TypeKind.Class,      "class",     typeof(object)),
            new(SpecialType.None, TypeKind.Struct,     "struct",    typeof(ValueType)),
            new(SpecialType.None, TypeKind.Interface,  "interface", typeof(object)),
            new(SpecialType.None, TypeKind.Enum,       "enum",      typeof(Enum)),
            new(SpecialType.None, TypeKind.Delegate,   "delegate",  typeof(Delegate)),
            new(SpecialType.None, TypeKind.Array,      "array",     typeof(Array)),
            new(SpecialType.None, TypeKind.Null,       "null",      typeof(void)),
        };
    }
}
