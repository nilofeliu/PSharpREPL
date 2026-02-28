using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Parser;

internal partial class LanguageParser
{

    public GreenExpressionStatement ParseExpressionStatement()
    {
        // Parse the underlying expression first
        GreenExpression expr = ParsePrimaryExpression();

        // Wrap it in an expression statement
        return new GreenExpressionStatement(SyntaxKind.ExpressionStatement, expr);
    }


    internal GreenExpression ParseExpression()
    {
        return ParseAssignmentExpression();
    }


    private GreenStatement ParseStatement()
    {
        if (CurrentToken.Kind == SyntaxKind.NullLiteralExpression)
            return null;

        if (CurrentToken.Kind == SyntaxKind.OpenBraceToken)
            return ParseBlockStatement();

        if (CurrentToken.Kind == SyntaxKind.IfKeyword)
            return ParseIfStatement();

        if (CurrentToken.Kind == SyntaxKind.WhileKeyword)
            return ParseWhileStatement();

        if (CurrentToken.Kind == SyntaxKind.DoKeyword)
            return ParseDoWhileStatement();

        if (CurrentToken.Kind == SyntaxKind.ForKeyword)
            return ParseForStatement();

        if (CurrentToken.Kind == SyntaxKind.SwitchKeyword || CurrentToken.Kind == SyntaxKind.MatchKeyword)
            return ParseSwitchStatement();

        if (CurrentToken.Kind == SyntaxKind.LetKeyword || CurrentToken.Kind == SyntaxKind.VarKeyword)
            return ParseVariableDeclaration();

        if (SyntaxFacts.IsSpecialTypeKeyword(CurrentToken.Kind))
            return ParseVariableDeclaration();

        return ParseExpressionStatement();
    }


    // --------------------
    // Top-level entry: returns a GreenExpressionStatement
    // --------------------
    public GreenExpression ParsePrimaryExpression()
    {
        // Literals and identifiers
        if (CurrentToken.Kind.IsLiteral() || CurrentToken.Kind == SyntaxKind.IdentifierName)
        {
            return ParseTokenNode(CurrentToken);
        }

        // Otherwise, parse it as an expression node (binary, unary, etc.)
        var expr = ParseExpressionNode(ParseExpression(), null, null);
        return expr;
    }

    // --------------------
    // Expression-based nodes
    // --------------------
    public GreenExpression ParseExpressionNode(GreenExpression left, GreenToken operatorToken, GreenExpression right = null)
    {
        // Binary operators
        if (left.Kind switch
        {
            SyntaxKind.PlusToken => true,
            SyntaxKind.MinusToken => true,
            SyntaxKind.StarToken => true,
            SyntaxKind.SlashToken => true,
            SyntaxKind.PercentToken => true,
            SyntaxKind.AmpersandToken => true,
            SyntaxKind.PipeToken => true,
            SyntaxKind.CaretToken => true,
            SyntaxKind.QuestionQuestionToken => true,
            _ => false
        })
        {
            return left.Kind switch
            {
                SyntaxKind.PlusToken => new GreenAddExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.MinusToken => new GreenSubtractExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.StarToken => new GreenMultiplyExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.SlashToken => new GreenDivideExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.PercentToken => new GreenModuloExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.AmpersandToken => new GreenBitwiseAndExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.PipeToken => new GreenBitwiseOrExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.CaretToken => new GreenExclusiveOrExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.QuestionQuestionToken => new GreenCoalesceExpression(operatorToken.Kind, left, operatorToken, right),
                _ => throw new InvalidOperationException($"Unexpected binary operator: {left.Kind}")
            };
        }

        // Comparison operators
        if (left.Kind.IsComparisonOperator())
            return ParseComparisonNodes(left, operatorToken, right);

        // Logical operators
        if (left.Kind.IsLogicalOperator())
            return ParseLogicalNodes(left, operatorToken, right);
                
        // Prefix unary
        switch (left.Kind)
        {
            case SyntaxKind.MinusToken:
                return new GreenUnaryMinusExpression(operatorToken.Kind, operatorToken, left);
            case SyntaxKind.PlusToken:
                return new GreenUnaryPlusExpression(operatorToken.Kind, operatorToken, left);
            case SyntaxKind.BangToken:
                return new GreenLogicalNotExpression(operatorToken.Kind, operatorToken, left);
            case SyntaxKind.TildeToken:
                return new GreenBitwiseNotExpression(operatorToken.Kind, operatorToken, left);
            case SyntaxKind.PlusPlusToken:
                // If the token appears before a primary expression, treat as prefix
                return new GreenPreIncrementExpression(operatorToken.Kind, operatorToken, left);
            case SyntaxKind.MinusMinusToken:
                return new GreenPreDecrementExpression(operatorToken.Kind, operatorToken, left);
        }

        // Postfix unary
        switch (left.Kind)
        {
            case SyntaxKind.PlusPlusToken:
                // If the token appears after a primary expression, treat as postfix
                return new GreenPostIncrementExpression(operatorToken.Kind, left, operatorToken);
            case SyntaxKind.MinusMinusToken:
                return new GreenPostDecrementExpression(operatorToken.Kind, left, operatorToken);
        }

        throw new InvalidOperationException($"Unexpected expression node kind: {left.Kind}");
    }

    // --------------------
    // Token-based nodes
    // --------------------
    public GreenExpression ParseTokenNode(GreenToken token, GreenToken operatorToken = null, GreenExpression right = null)
    {
        // Assignment operators
        if (token.Kind.IsAssignmentOperator())
            return ParseAssignmentNodes(token, operatorToken, right);

        // Literals
        if (token.Kind switch
        {
            SyntaxKind.ByteLiteralToken => true,
            SyntaxKind.SByteLiteralToken => true,
            SyntaxKind.ShortLiteralToken => true,
            SyntaxKind.UShortLiteralToken => true,
            SyntaxKind.IntLiteralToken => true,
            SyntaxKind.UIntLiteralToken => true,
            SyntaxKind.LongLiteralToken => true,
            SyntaxKind.ULongLiteralToken => true,
            SyntaxKind.FloatLiteralToken => true,
            SyntaxKind.DoubleLiteralToken => true,
            SyntaxKind.DecimalLiteralToken => true,
            SyntaxKind.StringLiteralToken => true,
            SyntaxKind.VoidLiteralToken => true,
            SyntaxKind.CharacterLiteralToken => true,
            SyntaxKind.TrueLiteralToken => true,
            SyntaxKind.FalseLiteralToken => true,
            SyntaxKind.NullLiteralToken => true,
            SyntaxKind.DefaultLiteralToken => true,
            _ => false
        })
            return ParseLiteralNodes(token);

        // Identifier
        if (token.Kind == SyntaxKind.IdentifierName)
            return ParseIdentifierNameNodes(token);

        throw new InvalidOperationException($"Unexpected token node kind: {token.Kind}");
    }
}
 