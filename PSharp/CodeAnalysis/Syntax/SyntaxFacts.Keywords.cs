using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;
using System.Collections.Generic;

namespace PSharp.CodeAnalysis.Syntax;

public static partial class SyntaxFacts
{
    public static void TryAddSymbol(List<SyntaxSymbol> list, SyntaxSymbol symbol)
    {
        if (!list.Any(s => s.Kind == symbol.Kind))
            list.Add(symbol);
    }

    internal static List<SyntaxSymbol> LoadOperators()
    {
        var list = new List<SyntaxSymbol>();
        list.AddRange(LoadArithmeticOperators());
        list.AddRange(LoadComparisonOperators());
        list.AddRange(LoadLogicalOperators());
        list.AddRange(LoadBitwiseOperators());
        list.AddRange(LoadUnaryOperators());
        list.AddRange(LoadAssignmentOperators());
        list.AddRange(LoadCompoundAssignmentOperators());
        return list;
    }

    internal static List<SyntaxSymbol> LoadArithmeticOperators()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StarToken, "*", SyntaxGroup.ArithmeticOperator, 5));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SlashToken, "/", SyntaxGroup.ArithmeticOperator, 5));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PercentToken, "%", SyntaxGroup.ArithmeticOperator, 5));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PlusToken, "+", SyntaxGroup.ArithmeticOperator, 4, 6));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MinusToken, "-", SyntaxGroup.ArithmeticOperator, 4, 6));
        return list;
    }

    internal static List<SyntaxSymbol> LoadComparisonOperators()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsEqualsToken, "==", SyntaxGroup.ComparisonOperator, 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BangEqualsToken, "!=", SyntaxGroup.ComparisonOperator, 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanToken, ">", SyntaxGroup.ComparisonOperator, 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanEqualsToken, ">=", SyntaxGroup.ComparisonOperator, 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanToken, "<", SyntaxGroup.ComparisonOperator, 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanOrEqualsToken, "<=", SyntaxGroup.ComparisonOperator, 3));
        return list;
    }

    internal static List<SyntaxSymbol> LoadLogicalOperators()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AmpersandAmpersandToken, "&&", SyntaxGroup.LogicalOperator, 2));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PipePipeToken, "||", SyntaxGroup.LogicalOperator, 1));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QuestionQuestionToken, "??", SyntaxGroup.LogicalOperator, 1));
        return list;
    }

    internal static List<SyntaxSymbol> LoadBitwiseOperators()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AmpersandToken, "&", SyntaxGroup.BitwiseOperator, 2));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PipeToken, "|", SyntaxGroup.BitwiseOperator, 1));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CaretToken, "^", SyntaxGroup.BitwiseOperator, 1));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TildeToken, "~", SyntaxGroup.BitwiseOperator, 0, 6));
        return list;
    }

    internal static List<SyntaxSymbol> LoadUnaryOperators()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BangToken, "!", SyntaxGroup.UnaryOperator, 0, 6));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PlusPlusToken, "++", SyntaxGroup.UnaryOperator, 0, 6));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MinusMinusToken, "--", SyntaxGroup.UnaryOperator, 0, 6));
        return list;
    }

    internal static List<SyntaxSymbol> LoadAssignmentOperators()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsToken, "=", SyntaxGroup.AssignmentOperator));
        return list;
    }

    internal static List<SyntaxSymbol> LoadCompoundAssignmentOperators()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PlusEqualsToken, "+=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MinusEqualsToken, "-=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StarEqualsToken, "*=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SlashEqualsToken, "/=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PercentEqualsToken, "%=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AmpersandEqualsToken, "&=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PipeEqualsToken, "|=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CaretEqualsToken, "^=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanLessThanEqualsToken, "<<=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanGreaterThanEqualsToken, ">>=", SyntaxGroup.CompoundAssignmentOperator));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QuestionQuestionEqualsToken, "??=", SyntaxGroup.CompoundAssignmentOperator));
        return list;
    }

    // Trivia
    internal static List<SyntaxSymbol> LoadTriviaKinds()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhiteSpaceTrivia, " ", SyntaxGroup.Trivia));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TabTrivia, "\t", SyntaxGroup.Trivia));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NewLineTrivia, "\n", SyntaxGroup.Trivia));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SingleLineCommentTrivia, "//", SyntaxGroup.Trivia));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MultiLineCommentTrivia, "/*", SyntaxGroup.Trivia));
        return list;
    }

    internal static List<SyntaxSymbol> LoadDirectiveTrivia()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ShebangDirectiveTrivia, "", SyntaxGroup.Trivia));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LoadDirectiveTrivia, "", SyntaxGroup.Trivia));
        return list;
    }

    // Punctuation
    internal static List<SyntaxSymbol> LoadPunctuation()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ColonToken, ":", SyntaxGroup.Punctuation));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OpenParenthesisToken, "(", SyntaxGroup.Punctuation));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CloseParenthesisToken, ")", SyntaxGroup.Punctuation));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OpenBraceToken, "{", SyntaxGroup.Punctuation));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CloseBraceToken, "}", SyntaxGroup.Punctuation));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CommaToken,          ",", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SemicolonToken,      ";", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnderscoreToken,     "_", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DotToken,            ".", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QuestionToken,       "?", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExclamationToken,    "!", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AtToken,             "@", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.HashToken,           "#", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DollarToken,         "$", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PercentToken,        "%", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BacktickToken,       "`", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BackslashToken,      "\\",SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OpenBracketToken,    "[", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CloseBracketToken,   "]", SyntaxGroup.Punctuation));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoubleQuoteToken,    "\"",SyntaxGroup.Punctuation));
        return list;
    }

    // Predefined Types
    internal static List<SyntaxSymbol> LoadSpecialTypeKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CharKeyword, "char", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BoolKeyword, "bool", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ByteKeyword, "byte", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SByteKeyword, "sbyte", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ShortKeyword, "short", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UShortKeyword, "ushort", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IntegerKeyword, "int", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UIntegerKeyword, "uint", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LongKeyword, "long", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ULongKeyword, "ulong", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FloatKeyword, "float", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoubleKeyword, "double", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DecimalKeyword, "decimal", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StringKeyword, "string", SyntaxGroup.SpecialType));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VoidKeyword, "void", SyntaxGroup.SpecialType));
        return list;
    }

    internal static List<SyntaxSymbol> LoadLiteralSpecialTypeTokens()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NumericLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IntLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LongLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FloatLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoubleLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DecimalLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StringLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CharacterLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ULongLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UIntLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ByteLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SByteLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ShortLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UShortLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IntLiteralToken, "", SyntaxGroup.Literal));

        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NullLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VoidLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TrueLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FalseLiteralToken, "", SyntaxGroup.Literal));

        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BinaryIntegerLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolatedStringLiteralToken, "", SyntaxGroup.Literal));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VerbatimStringLiteralToken, "", SyntaxGroup.Literal));
        return list;
    }

    

        

        
        


    // Type Declarations
    internal static List<SyntaxSymbol> LoadTypeKindKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ClassKeyword, "class", SyntaxGroup.TypeKind));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterfaceKeyword, "interface", SyntaxGroup.TypeKind));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StructKeyword, "struct", SyntaxGroup.TypeKind));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EnumKeyword, "enum", SyntaxGroup.TypeKind));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DelegateKeyword, "delegate", SyntaxGroup.TypeKind));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RecordKeyword, "record", SyntaxGroup.TypeKind));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NamespaceKeyword, "namespace", SyntaxGroup.TypeKind));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ObjectKeyword, "object", SyntaxGroup.TypeKind));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NullKeyword, "null", SyntaxGroup.TypeKind));
        return list;
    }

    // Expression Keywords
    internal static List<SyntaxSymbol> LoadBooleanKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TrueKeyword, "true", SyntaxGroup.BooleanKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FalseKeyword, "false", SyntaxGroup.BooleanKeywords));
        return list;
    }

    internal static List<SyntaxSymbol> LoadVariableKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LetKeyword, "let", SyntaxGroup.VariableKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VarKeyword, "var", SyntaxGroup.VariableKeywords));
        return list;
    }

    // Access Modifiers
    internal static List<SyntaxSymbol> LoadAccessModifierKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PublicKeyword, "public", SyntaxGroup.AccessModifierKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PrivateKeyword, "private", SyntaxGroup.AccessModifierKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InternalKeyword, "internal", SyntaxGroup.AccessModifierKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ProtectedKeyword, "protected", SyntaxGroup.AccessModifierKeywords));
        return list;
    }

    // Inheritance Modifiers
    internal static List<SyntaxSymbol> LoadInheritanceModifierKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StaticKeyword, "static", SyntaxGroup.InheritanceModifierKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AbstractKeyword, "abstract", SyntaxGroup.InheritanceModifierKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SealedKeyword, "sealed", SyntaxGroup.InheritanceModifierKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VirtualKeyword, "virtual", SyntaxGroup.InheritanceModifierKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OverrideKeyword, "override", SyntaxGroup.InheritanceModifierKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NewKeyword, "new", SyntaxGroup.InheritanceModifierKeywords));
        return list;
    }

    // Member Modifiers
    internal static List<SyntaxSymbol> LoadContextualKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReadOnlyKeyword, "readonly", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConstKeyword, "const", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GetKeyword, "get", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SetKeyword, "set", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InitKeyword, "init", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EventKeyword, "event", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PropertyKeyword, "property", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FixedKeyword, "fixed", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StackAllocKeyword, "stackalloc", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VolatileKeyword, "volatile", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.YieldKeyword, "yield", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PartialKeyword, "partial", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AliasKeyword, "alias", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GlobalKeyword, "global", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AssemblyKeyword, "assembly", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ModuleKeyword, "module", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeKeyword, "type", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FieldKeyword, "field", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MethodKeyword, "method", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParamKeyword, "param", SyntaxGroup.ContextualKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeVarKeyword, "typevar", SyntaxGroup.ContextualKeywords));
        return list;
    }

    // Statement Keywords
    internal static List<SyntaxSymbol> LoadConditionalsKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IfKeyword, "if", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElseKeyword, "else", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElseIfKeyword, "elseif", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhileKeyword, "while", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoKeyword, "do", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForKeyword, "for", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ToKeyword, "to", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SwitchKeyword, "switch", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CaseKeyword, "case", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultKeyword, "default", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MatchKeyword, "match", SyntaxGroup.ConditionalKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForEachKeyword, "foreach", SyntaxGroup.ConditionalKeywords));
        return list;
    }

    // Flow Control Keywords
    internal static List<SyntaxSymbol> LoadFlowControlStatement()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ContinueKeyword, "continue", SyntaxGroup.FlowControlStatement));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BreakKeyword, "break", SyntaxGroup.FlowControlStatement));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EndKeyword, "end", SyntaxGroup.FlowControlStatement));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReturnKeyword, "return", SyntaxGroup.FlowControlStatement));
        return list;
    }

    // Exception Keywords
    internal static List<SyntaxSymbol> LoadExceptionKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TryKeyword, "try", SyntaxGroup.ExceptionKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchKeyword, "catch", SyntaxGroup.ExceptionKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FinallyKeyword, "finally", SyntaxGroup.ExceptionKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LockKeyword, "lock", SyntaxGroup.ExceptionKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoKeyword, "goto", SyntaxGroup.ExceptionKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThrowKeyword, "throw", SyntaxGroup.ExceptionKeywords));
        return list;
    }

    internal static List<SyntaxSymbol> LoadReferenceKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExternKeyword, "extern", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefKeyword, "ref", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OutKeyword, "out", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InKeyword, "in", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IsKeyword, "is", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AsKeyword, "as", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParamsKeyword, "params", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArgListKeyword, "__arglist", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MakeRefKeyword, "__makeref", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefTypeKeyword, "__reftype", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefValueKeyword, "__refvalue", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThisKeyword, "this", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseKeyword, "base", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UsingKeyword, "using", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CheckedKeyword, "checked", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UncheckedKeyword, "unchecked", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsafeKeyword, "unsafe", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OperatorKeyword, "operator", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExplicitKeyword, "explicit", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitKeyword, "implicit", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeOfKeyword, "typeof", SyntaxGroup.ReferenceKeywords));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SizeOfKeyword, "sizeof", SyntaxGroup.ReferenceKeywords));
        return list;
    }

    internal static List<SyntaxSymbol> LoadOtherKeywords()
    {
        var list = new List<SyntaxSymbol>();
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddKeyword,         "add",        SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RemoveKeyword,      "remove",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhereKeyword,       "where",      SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FromKeyword,        "from",       SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GroupKeyword,       "group",      SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.JoinKeyword,        "join",       SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IntoKeyword,        "into",       SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LetKeyword,         "let",        SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ByKeyword,          "by",         SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SelectKeyword,      "select",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrderByKeyword,     "orderby",    SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OnKeyword,          "on",         SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsKeyword,      "equals",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AscendingKeyword,   "ascending",  SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DescendingKeyword,  "descending", SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NameOfKeyword,      "nameof",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AsyncKeyword,       "async",      SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AwaitKeyword,       "await",      SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhenKeyword,        "when",       SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrKeyword,          "or",         SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AndKeyword,         "and",        SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NotKeyword,         "not",        SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WithKeyword,        "with",       SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InitKeyword,        "init",       SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RecordKeyword,      "record",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ManagedKeyword,     "managed",    SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnmanagedKeyword,   "unmanaged",  SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RequiredKeyword,    "required",   SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ScopedKeyword,      "scoped",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FileKeyword,        "file",       SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AllowsKeyword,      "allows",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExtensionKeyword,   "extension",  SyntaxGroup.None));
        return list;
    }

    internal static List<SyntaxSymbol> LoadPreprocessorKeywords()
    {
        var list = new List<SyntaxSymbol>();
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElifKeyword,        "elif",      SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EndIfKeyword,       "endif",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RegionKeyword,      "region",    SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EndRegionKeyword,   "endregion", SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefineKeyword,      "define",    SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UndefKeyword,       "undef",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WarningKeyword,     "warning",   SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ErrorKeyword,       "error",     SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LineKeyword,        "line",      SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PragmaKeyword,      "pragma",    SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.HiddenKeyword,      "hidden",    SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ChecksumKeyword,    "checksum",  SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DisableKeyword,     "disable",   SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RestoreKeyword,     "restore",   SyntaxGroup.None));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReferenceKeyword,   "r",         SyntaxGroup.None));
        return list;
    }
}