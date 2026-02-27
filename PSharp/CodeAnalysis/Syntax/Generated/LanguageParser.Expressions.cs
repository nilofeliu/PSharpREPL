using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Parser
{
    internal partial class LanguageParser
    {
        private GreenExpression ParseAssignmentExpression()
        {
            if (PeekToken(0).Kind == SyntaxKind.IdentifierToken &&
                SyntaxFacts.IsAssignmentOperator(PeekToken(1).Kind))
            {
                var identifierToken = EatToken();
                var operatorToken = EatToken();
                var right = ParseAssignmentExpression();
                return ExpressionFactory.CreateAssignment(identifierToken, operatorToken, right);
            }
            return ParseOperatorExpression();
        }

        private GreenExpression ParseOperatorExpression(int parentPrecedence = 0)
        {
            GreenExpression left;
            var unaryPrecedence = CurrentToken.Kind.GetUnaryOperatorPrecedence();
            if (unaryPrecedence != 0 && unaryPrecedence >= parentPrecedence)
            {
                var operatorToken = EatToken();
                var operand = ParseOperatorExpression(unaryPrecedence);
                left = ExpressionFactory.CreatePrefixUnary(operatorToken, operand);
            }
            else
            {
                left = ParsePrimaryExpression();
            }
            while (true)
            {
                var precedence = CurrentToken.Kind.GetBinaryOperatorPrecedence();
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;
                var operatorToken = EatToken();
                var right = ParseOperatorExpression(precedence);
                left = operatorToken.Kind.IsComparisonOperator()
                    ? ExpressionFactory.CreateComparison(left, operatorToken, right)
                    : operatorToken.Kind.IsLogicalOperator()
                        ? ExpressionFactory.CreateLogical(left, operatorToken, right)
                        : ExpressionFactory.CreateBinary(left, operatorToken, right);
            }
            return left;
        }

        private GreenExpression ParsePrimaryExpression()
        {
            return CurrentToken.Kind switch
            {
                SyntaxKind.NumericLiteralToken => ParseNumericLiteralExpression(),
                SyntaxKind.StringLiteralToken => ParseStringLiteralExpression(),
                SyntaxKind.CharacterLiteralToken => ParseCharacterLiteralExpression(),
                SyntaxKind.TrueLiteralToken => ParseTrueLiteralExpression(),
                SyntaxKind.FalseLiteralToken => ParseFalseLiteralExpression(),
                SyntaxKind.NullLiteralToken => ParseNullLiteralExpression(),
                SyntaxKind.DefaultLiteralToken => ParseDefaultLiteralExpression(),
                SyntaxKind.IdentifierToken => ParseNameExpression(),
                SyntaxKind.OpenParenthesisToken => ParseParenthesizedExpression(),
                _ => ParseNameExpression() // fallback
            };
        }

        private GreenNumericLiteralExpression ParseNumericLiteralExpression()
        {
            var literalToken = EatToken(SyntaxKind.NumericLiteralToken);
            return new GreenNumericLiteralExpression(SyntaxKind.NumericLiteralExpression, literalToken);
        }

        private GreenStringLiteralExpression ParseStringLiteralExpression()
        {
            var literalToken = EatToken(SyntaxKind.StringLiteralToken);
            return new GreenStringLiteralExpression(SyntaxKind.StringLiteralToken, literalToken);
        }

        private GreenCharacterLiteralExpression ParseCharacterLiteralExpression()
        {
            var literalToken = EatToken(SyntaxKind.CharacterLiteralToken);
            return new GreenCharacterLiteralExpression(SyntaxKind.CharacterLiteralExpression, literalToken);
        }

        private GreenTrueLiteralExpression ParseTrueLiteralExpression()
        {
            var literalToken = EatToken(SyntaxKind.TrueLiteralToken);
            return new GreenTrueLiteralExpression(SyntaxKind.TrueLiteralExpression, literalToken);
        }

        private GreenFalseLiteralExpression ParseFalseLiteralExpression()
        {
            var literalToken = EatToken(SyntaxKind.FalseLiteralToken);
            return new GreenFalseLiteralExpression(SyntaxKind.FalseLiteralExpression, literalToken);
        }

        private GreenNullLiteralExpression ParseNullLiteralExpression()
        {
            var literalToken = EatToken(SyntaxKind.NullLiteralToken);
            return new GreenNullLiteralExpression(SyntaxKind.NullLiteralExpression, literalToken);
        }

        private GreenDefaultLiteralExpression ParseDefaultLiteralExpression()
        {
            var literalToken = EatToken(SyntaxKind.DefaultLiteralToken);
            return new GreenDefaultLiteralExpression(SyntaxKind.DefaultLiteralExpression, literalToken);
        }

        private GreenNameExpression ParseNameExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            return new GreenNameExpression(SyntaxKind.IdentifierName, identifierToken);
        }

        private GreenParenthesizedExpression ParseParenthesizedExpression()
        {
            var openParenthesisToken = EatToken(SyntaxKind.OpenParenthesisToken);
            var expression = ParseExpression();
            var closeParenthesisToken = EatToken(SyntaxKind.CloseParenthesisToken);
            return new GreenParenthesizedExpression(SyntaxKind.NumericLiteralExpression, openParenthesisToken, expression, closeParenthesisToken);
        }

    }
}
