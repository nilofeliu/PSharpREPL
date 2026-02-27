using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Symbols;

public sealed class SyntaxSymbol
{
    public SyntaxKind Kind { get; }
    public string Text { get; }
    public int BinaryPrecedence { get; }
    public int UnaryPrecedence { get; }
    public SyntaxGroup Group { get; }

    public SyntaxSymbol(SyntaxKind kind, string text, SyntaxGroup group = SyntaxGroup.None, int binaryPrecedence = 0, int unaryPrecedence = 0)
    {
        Kind = kind;
        Text = text;
        BinaryPrecedence = binaryPrecedence;
        UnaryPrecedence = unaryPrecedence;
        Group = group;
    }
}

public enum SyntaxGroup
{
    None,

    // Operators
    ArithmeticOperator,
    ComparisonOperator,
    LogicalOperator,
    BitwiseOperator,
    UnaryOperator,
    AssignmentOperator,
    CompoundAssignmentOperator,

    // Tokens
    Literal,
    Punctuation,
    Trivia,
    SpecialType,

    // Keywords
    TypeKind,
    BooleanKeywords,
    VariableKeywords,
    AccessModifierKeywords,
    InheritanceModifierKeywords,
    ContextualKeywords,
    ConditionalKeywords,
    FlowControlStatement,
    ExceptionKeywords,
    ReferenceKeywords,

    // Expressions
    BinaryExpression,
    UnaryExpression,
    AssignmentExpression,
    LiteralExpression,

    // Nodes
    Statement,
    Declaration,
    ComparisonExpression,
    LogicalExpression,
}