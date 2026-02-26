using PSharp.CodeAnalysis.Syntax.Internal;

namespace PSharp.CodeAnalysis.Syntax;
public static partial class SyntaxFacts
{
    static SyntaxFacts()
    {
        // ── Operators ─────────────────────────────────────────────────────────
        Register(SymbolTable.Operators, LoadOperators());

        // ── Keywords ──────────────────────────────────────────────────────────
        Register(SymbolTable.Keywords, LoadBooleanKeywords());
        Register(SymbolTable.Keywords, LoadVariableKeywords());
        Register(SymbolTable.Keywords, LoadAccessModifierKeywords());
        Register(SymbolTable.Keywords, LoadInheritanceModifierKeywords());
        Register(SymbolTable.Keywords, LoadContextualKeywords());
        Register(SymbolTable.Keywords, LoadTypeKindKeywords());
        Register(SymbolTable.Keywords, LoadReferenceKeywords());
        Register(SymbolTable.Keywords, LoadOtherKeywords());
        Register(SymbolTable.Keywords, LoadPreprocessorKeywords());

        // ── Special Types ─────────────────────────────────────────────────────
        Register(SymbolTable.SpecialTypes, LoadSpecialTypeKeywords());

        // ── Punctuation ───────────────────────────────────────────────────────
        Register(SymbolTable.Punctuation, LoadPunctuation());

        // ── Flow / Control Keywords ───────────────────────────────────────────
        Register(SymbolTable.FlowKeywords, LoadConditionalsKeywords());
        Register(SymbolTable.FlowKeywords, LoadFlowControlKeywords());
        Register(SymbolTable.FlowKeywords, LoadExceptionKeywords());

        // ── Trivia ────────────────────────────────────────────────────────────
        Register(SymbolTable.Trivia, LoadTriviaKinds());
        Register(SymbolTable.Trivia, LoadDirectiveTrivia());

        // ── Literal Tokens ────────────────────────────────────────────────────
        Register(SymbolTable.Tokens, LoadLiteralSpecialTypeTokens());

        // ── Expressions ───────────────────────────────────────────────────────
        Register(SymbolTable.Expressions, LoadExpressionTypes());
        Register(SymbolTable.Expressions, LoadBinaryExpressions());
        Register(SymbolTable.Expressions, LoadBinaryAssignmentExpressions());
        Register(SymbolTable.Expressions, LoadUnaryExpressions());
        Register(SymbolTable.Expressions, LoadPrimaryExpressions());
        Register(SymbolTable.Expressions, LoadPrimaryFunctionExpressions());
        Register(SymbolTable.Expressions, LoadQueryExpressions());

        // ── Statements ────────────────────────────────────────────────────────
        Register(SymbolTable.Statements, LoadStatements());
        Register(SymbolTable.Statements, LoadJumpStatements());
        Register(SymbolTable.Statements, LoadLoopStatements());
        Register(SymbolTable.Statements, LoadCheckedStatements());
        Register(SymbolTable.Statements, FlowControlStatements());
        Register(SymbolTable.Statements, LoadAdditionalStatements());

        // ── Declarations ──────────────────────────────────────────────────────
        Register(SymbolTable.Declarations, LoadDeclarations());
        Register(SymbolTable.Declarations, LoadAttributes());
        Register(SymbolTable.Declarations, LoadTypeDeclarations());
        Register(SymbolTable.Declarations, LoadTypeConstraints());
        Register(SymbolTable.Declarations, LoadMemberDeclarations());
        Register(SymbolTable.Declarations, LoadParameters());
        Register(SymbolTable.Declarations, LoadMiscellaneous());
    }
}