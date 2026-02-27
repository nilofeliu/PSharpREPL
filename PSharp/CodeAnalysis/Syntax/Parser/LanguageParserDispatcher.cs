using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Parser;

internal partial class LanguageParser
{
    internal GreenStatement ParseStatement()
    {
        switch (CurrentToken.Kind)
        {
            case SyntaxKind.OpenBraceToken:
                return ParseBlockStatement();
            case SyntaxKind.LetKeyword:
            case SyntaxKind.VarKeyword:
                return ParseVariableDeclaration();
            case SyntaxKind.IfKeyword:
                return ParseIfStatement();
            case SyntaxKind.SwitchKeyword:
            case SyntaxKind.MatchKeyword:
                return ParseSwitchStatement();
            case SyntaxKind.WhileKeyword:
                return ParseWhileStatement();
            case SyntaxKind.DoKeyword:
                return ParseDoWhileStatement();
            case SyntaxKind.ForKeyword:
                return ParseForStatement();
            default:
                if (SyntaxFacts.IsSpecialTypeKeyword(CurrentToken.Kind))
                    return ParseVariableDeclaration();
                return ParseExpressionStatement();
        }
    }

    internal GreenStatement ParseExpressionStatement()
    {
        var expression = ParseExpression();
        return new GreenExpressionStatement(SyntaxKind.ExpressionStatement, expression);
    }

    internal GreenExpression ParseExpression()
    {
        return ParseAssignmentExpression();
    }

}
