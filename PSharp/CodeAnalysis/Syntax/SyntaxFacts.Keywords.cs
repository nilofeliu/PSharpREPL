using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;
using System.Collections.Generic;

namespace Minsk.CodeAnalysis.Syntax;

public static partial class SyntaxFacts

{
    public static void TryAddSymbol(List<SyntaxSymbol> list, SyntaxSymbol symbol)
    {
        if (!list.Any(s => s.Kind == symbol.Kind))
            list.Add(symbol);
    }

    // Trivia
    internal static List<SyntaxSymbol> LoadTriviaKinds()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhiteSpaceTrivia, " "));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TabTrivia, "\t"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NewLineTrivia, "\n"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SingleLineCommentTrivia, "//"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MultiLineCommentTrivia, "/*"));
        return list;
    }

    internal static List<SyntaxSymbol> LoadDirectiveTrivia()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ShebangDirectiveTrivia, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LoadDirectiveTrivia, ""));
        return list;
    }

    // Punctuation
    internal static List<SyntaxSymbol> LoadPunctuation()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ColonToken, ":"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OpenParenthesisToken, "("));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CloseParenthesisToken, ")"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OpenBraceToken, "{"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CloseBraceToken, "}"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CommaToken,          ","));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SemicolonToken,      ";"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnderscoreToken,     "_"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DotToken,            "."));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QuestionToken,       "?"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExclamationToken,    "!"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AtToken,             "@"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.HashToken,           "#"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DollarToken,         "$"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PercentToken,        "%"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BacktickToken,       "`"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BackslashToken,      "\\"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OpenBracketToken,    "["));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CloseBracketToken,   "]"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoubleQuoteToken,    "\""));
        return list;
    }


    internal static List<SyntaxSymbol> LoadOperators()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StarToken, "*", 5));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SlashToken, "/", 5));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PlusToken, "+", 4, 6));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MinusToken, "-", 4, 6));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsEqualsToken, "==", 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BangEqualsToken, "!=", 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterOrEqualsToken, ">=", 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterToken, ">", 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessOrEqualsToken, "<=", 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessToken, "<", 3));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AmpersandAmpersandToken, "&&", 2));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AmpersandToken, "&", 2));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PipeToken, "|", 1));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PipePipeToken, "||", 1));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.HatToken, "^", 1));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TildeToken, "~", 0, 6));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BangToken, "!", 0, 6));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsToken, "="));
        return list;
    }

    // Predefined Types
    internal static List<SyntaxSymbol> LoadSpecialTypeKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CharKeyword, "char"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BoolKeyword, "bool"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ByteKeyword, "byte"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SByteKeyword, "sbyte"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ShortKeyword, "short"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UShortKeyword, "ushort"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IntKeyword, "int"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UIntKeyword, "uint"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LongKeyword, "long"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ULongKeyword, "ulong"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FloatKeyword, "float"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoubleKeyword, "double"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DecimalKeyword, "decimal"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StringKeyword, "string"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VoidKeyword, "void"));
        return list;
    }

    internal static List<SyntaxSymbol> LoadLiteralSpecialTypeTokens()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NumericLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IntegerLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LongLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FloatLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoubleLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DecimalLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StringLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CharLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ULongLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UIntLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BinaryIntegerLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolatedStringLiteralToken, ""));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VerbatimStringLiteralToken, ""));


        return list;
    }

    // Type Declarations
    internal static List<SyntaxSymbol> LoadTypeKindKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ClassKeyword, "class"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterfaceKeyword, "interface"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StructKeyword, "struct"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EnumKeyword, "enum"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DelegateKeyword, "delegate"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RecordKeyword, "record"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NamespaceKeyword, "namespace"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ObjectKeyword, "object"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NullKeyword, "null"));

        return list;
    }

    // Expression Keywords
    internal static List<SyntaxSymbol> LoadBooleanKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TrueKeyword, "true"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FalseKeyword, "false"));
        return list;
    }

    internal static List<SyntaxSymbol> LoadVariableKeywords()
    {

        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LetKeyword, "let"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VarKeyword, "var"));
        return list;

    }

    // Access Modifiers
    internal static List<SyntaxSymbol> LoadAccessModifierKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PublicKeyword, "public"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PrivateKeyword, "private"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InternalKeyword, "internal"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ProtectedKeyword, "protected"));
        return list;
    }

    // Inheritance Modifiers
    internal static List<SyntaxSymbol> LoadInheritanceModifierKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StaticKeyword, "static"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AbstractKeyword, "abstract"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SealedKeyword, "sealed"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VirtualKeyword, "virtual"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OverrideKeyword, "override"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NewKeyword, "new"));
        return list;


    }

    // Member Modifiers
    internal static List<SyntaxSymbol> LoadContextualKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReadOnlyKeyword, "readonly"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConstKeyword, "const"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GetKeyword, "get"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SetKeyword, "set"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InitKeyword, "init"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EventKeyword, "event"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PropertyKeyword, "property"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FixedKeyword, "fixed"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StackAllocKeyword, "stackalloc"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VolatileKeyword, "volatile"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.YieldKeyword, "yield"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PartialKeyword, "partial"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AliasKeyword, "alias"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GlobalKeyword, "global"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AssemblyKeyword, "assembly"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ModuleKeyword, "module"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeKeyword, "type"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FieldKeyword, "field"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MethodKeyword, "method"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParamKeyword, "param"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PropertyKeyword, "property"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeVarKeyword, "typevar"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GetKeyword, "get"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SetKeyword, "set"));

        return list;
    }



    // Statement Keywords
    internal static List<SyntaxSymbol> LoadConditionalsKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IfKeyword, "if"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElseKeyword, "else"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElseIfKeyword, "elseif"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhileKeyword, "while"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoKeyword, "do"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForKeyword, "for"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ToKeyword, "to"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SwitchKeyword, "switch"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CaseKeyword, "case"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultKeyword, "default"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MatchKeyword, "match"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForEachKeyword, "foreach"));

        return list;
    }

    // Flow Control Keywords
    internal static List<SyntaxSymbol> LoadFlowControlKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ContinueKeyword, "continue"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BreakKeyword, "break"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EndKeyword, "end"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReturnKeyword, "return"));
        return list;
    }


    // Exception Keywords
    internal static List<SyntaxSymbol> LoadExceptionKeywords()
    {
        var list = new List<SyntaxSymbol>();
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TryKeyword, "try"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchKeyword, "catch"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FinallyKeyword, "finally"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LockKeyword, "lock"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoKeyword, "goto"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThrowKeyword, "throw"));
        return list;

    }


    internal static List<SyntaxSymbol> LoadReferenceKeywords()
    {
        var list = new List<SyntaxSymbol>();

        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExternKeyword, "extern"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefKeyword, "ref"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OutKeyword, "out"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InKeyword, "in"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IsKeyword, "is"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AsKeyword, "as"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParamsKeyword, "params"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArgListKeyword, "__arglist"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MakeRefKeyword, "__makeref"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefTypeKeyword, "__reftype"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefValueKeyword, "__refvalue"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThisKeyword, "this"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseKeyword, "base"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UsingKeyword, "using"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CheckedKeyword, "checked"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UncheckedKeyword, "unchecked"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsafeKeyword, "unsafe"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OperatorKeyword, "operator"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExplicitKeyword, "explicit"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitKeyword, "implicit"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeOfKeyword, "typeof"));
        TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SizeOfKeyword, "sizeof"));
        return list;
    }

    internal static List<SyntaxSymbol> LoadOtherKeywords()
    {

        // Contextual keywords
        var list = new List<SyntaxSymbol>();
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddKeyword, "add"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RemoveKeyword, "remove"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhereKeyword, "where"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FromKeyword, "from"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GroupKeyword, "group"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.JoinKeyword, "join"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IntoKeyword, "into"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LetKeyword, "let"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ByKeyword, "by"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SelectKeyword, "select"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrderByKeyword, "orderby"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OnKeyword, "on"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsKeyword, "equals"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AscendingKeyword, "ascending"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DescendingKeyword, "descending"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NameOfKeyword, "nameof"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AsyncKeyword, "async"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AwaitKeyword, "await"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhenKeyword, "when"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrKeyword, "or"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AndKeyword, "and"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NotKeyword, "not"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WithKeyword, "with"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InitKeyword, "init"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RecordKeyword, "record"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ManagedKeyword, "managed"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnmanagedKeyword, "unmanaged"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RequiredKeyword, "required"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ScopedKeyword, "scoped"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FileKeyword, "file"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AllowsKeyword, "allows"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExtensionKeyword, "extension"));
        return list;
    }
    internal static List<SyntaxSymbol> LoadPreprocessorKeywords()
    {
        // Preprocessor keywords

        var list = new List<SyntaxSymbol>();
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElifKeyword, "elif"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EndIfKeyword, "endif"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RegionKeyword, "region"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EndRegionKeyword, "endregion"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefineKeyword, "define"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UndefKeyword, "undef"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WarningKeyword, "warning"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ErrorKeyword, "error"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LineKeyword, "line"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PragmaKeyword, "pragma"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.HiddenKeyword, "hidden"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ChecksumKeyword, "checksum"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DisableKeyword, "disable"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RestoreKeyword, "restore"));
        //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReferenceKeyword, "r"));

        return list;
    }
}