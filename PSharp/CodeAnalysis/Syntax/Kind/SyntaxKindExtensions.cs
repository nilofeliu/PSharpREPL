using PSharp.CodeAnalysis.Syntax;
using PSharp.CodeAnalysis.Syntax.Kind;

public static class SyntaxKindExtensions
{
    public static bool IsComparisonOperator(this SyntaxKind kind)
        => SyntaxFacts.IsComparisonOperator(kind);

    public static bool IsAssignmentOperator(this SyntaxKind kind)
        => SyntaxFacts.IsAssignmentOperator(kind);

    public static bool IsLogicalOperator(this SyntaxKind kind)
        => SyntaxFacts.IsLogicalOperators(kind);

    public static bool IsLiteral(this SyntaxKind kind)
    => SyntaxFacts.IsLiteralToken(kind);
}