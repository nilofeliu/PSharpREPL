namespace PSharp.CodeAnalysis.Syntax.Internal;

public enum SymbolTable
{
    Operators,
    Punctuation,
    Type,
    SpecialTypes,
    Trivia,
    AccessModifiers,      // public, private, protected, internal
    MemberModifiers,      // static, abstract, sealed, virtual, override
    InheritanceModifiers, // new, base, this
    Keywords,
    FlowKeywords,
    ExceptionKeywords,    // try, catch, finally, throw
    OtherKeywords,        // using, return, null
    Tokens,
    Expressions,
    Statements,
    Declarations,
}


