using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using PSharp.CodeAnalysis.Syntax.Nodes;

namespace PSharp.CodeAnalysis.Syntax.Parser
{
    internal static partial class ExpressionFactory
    {
        public static GreenExpression CreateBinary(GreenExpression left, GreenToken operatorToken, GreenExpression right)
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

        public static GreenExpression CreateComparison(GreenExpression left, GreenToken operatorToken, GreenExpression right)
        {
            return left.Kind switch
            {
                SyntaxKind.EqualsEqualsToken => new GreenEqualsExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.BangEqualsToken => new GreenNotEqualsExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.LessThanToken => new GreenLessThanExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.LessThanEqualsToken => new GreenLessThanOrEqualExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.GreaterThanToken => new GreenGreaterThanExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.GreaterThanEqualsToken => new GreenGreaterThanOrEqualExpression(operatorToken.Kind, left, operatorToken, right),
                _ => throw new InvalidOperationException($"Unexpected comparison operator: {left.Kind}")
            };
        }

        public static GreenExpression CreateLogical(GreenExpression left, GreenToken operatorToken, GreenExpression right)
        {
            return left.Kind switch
            {
                SyntaxKind.AmpersandAmpersandToken => new GreenLogicalAndExpression(operatorToken.Kind, left, operatorToken, right),
                SyntaxKind.PipePipeToken => new GreenLogicalOrExpression(operatorToken.Kind, left, operatorToken, right),
                _ => throw new InvalidOperationException($"Unexpected logical operator: {left.Kind}")
            };
        }

        public static GreenExpression CreatePrefixUnary(GreenToken operatorToken, GreenExpression operand)
        {
            return operatorToken.Kind switch
            {
                SyntaxKind.MinusToken => new GreenUnaryMinusExpression(operatorToken.Kind, operatorToken, operand),
                SyntaxKind.PlusToken => new GreenUnaryPlusExpression(operatorToken.Kind, operatorToken, operand),
                SyntaxKind.BangToken => new GreenLogicalNotExpression(operatorToken.Kind, operatorToken, operand),
                SyntaxKind.TildeToken => new GreenBitwiseNotExpression(operatorToken.Kind, operatorToken, operand),
                SyntaxKind.PlusPlusToken => new GreenPreIncrementExpression(operatorToken.Kind, operatorToken, operand),
                SyntaxKind.MinusMinusToken => new GreenPreDecrementExpression(operatorToken.Kind, operatorToken, operand),
                _ => throw new InvalidOperationException($"Unexpected prefix unary operator: {operatorToken.Kind}")
            };
        }

        public static GreenExpression CreatePostfixUnary(GreenExpression operand, GreenToken operatorToken)
        {
            return operand.Kind switch
            {
                SyntaxKind.PlusPlusToken => new GreenPostIncrementExpression(operatorToken.Kind, operand, operatorToken),
                SyntaxKind.MinusMinusToken => new GreenPostDecrementExpression(operatorToken.Kind, operand, operatorToken),
                _ => throw new InvalidOperationException($"Unexpected postfix unary operator: {operand.Kind}")
            };
        }

        public static GreenExpression CreateAssignment(GreenToken identifierToken, GreenToken operatorToken, GreenExpression expression)
        {
            return identifierToken.Kind switch
            {
                SyntaxKind.EqualsToken => new GreenSimpleAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.PlusEqualsToken => new GreenAddAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.MinusEqualsToken => new GreenSubtractAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.StarEqualsToken => new GreenMultiplyAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.SlashEqualsToken => new GreenDivideAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.PercentEqualsToken => new GreenModuloAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.AmpersandEqualsToken => new GreenAndAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.PipeEqualsToken => new GreenOrAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.CaretEqualsToken => new GreenExclusiveOrAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.LessThanLessThanEqualsToken => new GreenLeftShiftAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.GreaterThanGreaterThanEqualsToken => new GreenRightShiftAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                SyntaxKind.QuestionQuestionEqualsToken => new GreenCoalesceAssignmentExpression(operatorToken.Kind, identifierToken, operatorToken, expression),
                _ => throw new InvalidOperationException($"Unexpected assignment operator: {identifierToken.Kind}")
            };
        }

        public static GreenExpression CreateLiteral(GreenToken token)
        {
            return token.Kind switch
            {
                SyntaxKind.ByteLiteralToken => new GreenByteLiteralExpression(token.Kind, token),
                SyntaxKind.SByteLiteralToken => new GreenSByteLiteralExpression(token.Kind, token),
                SyntaxKind.ShortLiteralToken => new GreenShortLiteralExpression(token.Kind, token),
                SyntaxKind.UShortLiteralToken => new GreenUShortLiteralExpression(token.Kind, token),
                SyntaxKind.IntLiteralToken => new GreenIntLiteralExpression(token.Kind, token),
                SyntaxKind.UIntLiteralToken => new GreenUIntLiteralExpression(token.Kind, token),
                SyntaxKind.LongLiteralToken => new GreenLongLiteralExpression(token.Kind, token),
                SyntaxKind.ULongLiteralToken => new GreenULongLiteralExpression(token.Kind, token),
                SyntaxKind.FloatLiteralToken => new GreenFloatLiteralExpression(token.Kind, token),
                SyntaxKind.DoubleLiteralToken => new GreenDoubleLiteralExpression(token.Kind, token),
                SyntaxKind.DecimalLiteralToken => new GreenDecimalLiteralExpression(token.Kind, token),
                SyntaxKind.StringLiteralToken => new GreenStringLiteralExpression(token.Kind, token),
                SyntaxKind.VoidLiteralToken => new GreenVoidLiteralExpression(token.Kind, token),
                SyntaxKind.CharacterLiteralToken => new GreenCharacterLiteralExpression(token.Kind, token),
                SyntaxKind.TrueLiteralToken => new GreenTrueLiteralExpression(token.Kind, token),
                SyntaxKind.FalseLiteralToken => new GreenFalseLiteralExpression(token.Kind, token),
                SyntaxKind.NullLiteralToken => new GreenNullLiteralExpression(token.Kind, token),
                SyntaxKind.DefaultLiteralToken => new GreenDefaultLiteralExpression(token.Kind, token),
                _ => throw new InvalidOperationException($"Invalid literal token kind: {token.Kind}")
            };
        }

        public static GreenExpression CreateOtherNodes(GreenToken token)
        {
            return token.Kind switch
            {
                SyntaxKind.IdentifierName => new GreenNameExpression(token.Kind, token),
                _ => throw new InvalidOperationException($"Invalid token kind: {token.Kind}")
            };
        }

    }
}
