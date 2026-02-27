using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;

namespace PSharp.CodeAnalysis.Syntax.Parser
{
    internal static partial class ExpressionFactory
    {
        public static GreenExpression CreateBinary(GreenExpression left, GreenToken operatorToken, GreenExpression right)
        {
            return left.Kind switch
            {
                SyntaxKind.PlusToken => new GreenAddExpression(SyntaxKind.AddExpression, left, operatorToken, right),
                SyntaxKind.MinusToken => new GreenSubtractExpression(SyntaxKind.SubtractExpression, left, operatorToken, right),
                SyntaxKind.StarToken => new GreenMultiplyExpression(SyntaxKind.MultiplyExpression , left, operatorToken, right),
                SyntaxKind.SlashToken => new GreenDivideExpression(SyntaxKind.DivideExpression, left, operatorToken, right),
                //SyntaxKind.PercentToken => new GreenModuloExpression(SyntaxKind.PercentToken, left, operatorToken, right),
                SyntaxKind.AmpersandToken => new GreenBitwiseAndExpression(SyntaxKind.BitwiseAndExpression, left, operatorToken, right),
                SyntaxKind.PipeToken => new GreenBitwiseOrExpression(SyntaxKind.BitwiseOrExpression ,left, operatorToken, right),
                SyntaxKind.CaretToken => new GreenExclusiveOrExpression(SyntaxKind.ExclusiveOrAssignmentExpression ,left, operatorToken, right),
                //SyntaxKind.QuestionQuestionToken => new GreenCoalesceExpression(left, operatorToken, right),
                _ => throw new InvalidOperationException($"Unexpected binary operator: {left.Kind}")
            };
        }

        public static GreenExpression CreateComparison(GreenExpression left, GreenToken operatorToken, GreenExpression right)
        {
            return left.Kind switch
            {
                SyntaxKind.EqualsEqualsToken => new GreenEqualsExpression(SyntaxKind.EqualsExpression, left, operatorToken, right),
                SyntaxKind.BangEqualsToken => new GreenNotEqualsExpression(SyntaxKind.NotEqualsExpression, left, operatorToken, right),
                SyntaxKind.LessThanToken => new GreenLessThanExpression(SyntaxKind.LessThanExpression, left, operatorToken, right),
                SyntaxKind.LessThanEqualsToken => new GreenLessThanOrEqualExpression(SyntaxKind.LessThanOrEqualExpression, left, operatorToken, right),
                SyntaxKind.GreaterThanToken => new GreenGreaterThanExpression(SyntaxKind.GreaterThanExpression, left, operatorToken, right),
                SyntaxKind.GreaterThanEqualsToken => new GreenGreaterThanOrEqualExpression(SyntaxKind.GreaterThanOrEqualExpression, left, operatorToken, right),
                _ => throw new InvalidOperationException($"Unexpected comparison operator: {left.Kind}")
            };
        }

        public static GreenExpression CreateLogical(GreenExpression left, GreenToken operatorToken, GreenExpression right)
        {
            return left.Kind switch
            {
                SyntaxKind.AmpersandAmpersandToken => new GreenLogicalAndExpression(SyntaxKind.BangEqualsToken, left, operatorToken, right),
                SyntaxKind.PipePipeToken => new GreenLogicalOrExpression(SyntaxKind.BangEqualsToken, left, operatorToken, right),
                _ => throw new InvalidOperationException($"Unexpected logical operator: {left.Kind}")
            };
        }

        public static GreenExpression CreatePrefixUnary(GreenToken operatorToken, GreenExpression operand)
        {
            return operatorToken.Kind switch
            {
                SyntaxKind.MinusToken => new GreenUnaryMinusExpression(SyntaxKind.BangEqualsToken, operatorToken, operand),
                SyntaxKind.PlusToken => new GreenUnaryPlusExpression(SyntaxKind.BangEqualsToken,operatorToken, operand),
                SyntaxKind.BangToken => new GreenLogicalNotExpression(SyntaxKind.BangEqualsToken,operatorToken, operand),
                SyntaxKind.TildeToken => new GreenBitwiseNotExpression(SyntaxKind.BangEqualsToken,operatorToken, operand),
                SyntaxKind.PlusPlusToken => new GreenPreIncrementExpression(SyntaxKind.PreIncrementExpression,operatorToken, operand),
                SyntaxKind.MinusMinusToken => new GreenPreDecrementExpression(SyntaxKind.PreDecrementExpression,operatorToken, operand),
                _ => throw new InvalidOperationException($"Unexpected prefix unary operator: {operatorToken.Kind}")
            };
        }

        public static GreenExpression CreatePostfixUnary(GreenExpression operand, GreenToken operatorToken)
        {
            return operand.Kind switch
            {
                //SyntaxKind.PlusPlusToken => new GreenPostIncrementExpression(SyntaxKind.PostIncrementExpression, operatorToken, operand),
                //SyntaxKind.MinusMinusToken => new GreenPostDecrementExpression(SyntaxKind.PostDecrementExpression, operatorToken, operand),
                _ => throw new InvalidOperationException($"Unexpected postfix unary operator: {operand.Kind}")
            };
        }

        public static GreenExpression CreateAssignment(GreenToken identifierToken, GreenToken operatorToken, GreenExpression expression)
        {
            return identifierToken.Kind switch
            {
                SyntaxKind.EqualsToken => new GreenSimpleAssignmentExpression(SyntaxKind.SimpleAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.PlusEqualsToken => new GreenAddAssignmentExpression(SyntaxKind.AddAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.MinusEqualsToken => new GreenSubtractAssignmentExpression(SyntaxKind.SubtractAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.StarEqualsToken => new GreenMultiplyAssignmentExpression(SyntaxKind.MultiplyAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.SlashEqualsToken => new GreenDivideAssignmentExpression(SyntaxKind.DivideAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.PercentEqualsToken => new GreenModuloAssignmentExpression(SyntaxKind.ModuloAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.AmpersandEqualsToken => new GreenAndAssignmentExpression(SyntaxKind.AndAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.PipeEqualsToken => new GreenOrAssignmentExpression(SyntaxKind.PostDecrementExpression, identifierToken, operatorToken, expression),
                SyntaxKind.CaretEqualsToken => new GreenExclusiveOrAssignmentExpression(SyntaxKind.ExclusiveOrAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.LessThanLessThanEqualsToken => new GreenLeftShiftAssignmentExpression(SyntaxKind.LeftShiftAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.GreaterThanGreaterThanEqualsToken => new GreenRightShiftAssignmentExpression(SyntaxKind.RightShiftAssignmentExpression, identifierToken, operatorToken, expression),
                SyntaxKind.QuestionQuestionEqualsToken => new GreenCoalesceAssignmentExpression(SyntaxKind.CoalesceAssignmentExpression, identifierToken, operatorToken, expression),
                _ => throw new InvalidOperationException($"Unexpected assignment operator: {identifierToken.Kind}")
            };
        }

        public static GreenExpression CreateLiteral(GreenToken token)
        {
            return token.Kind switch
            {
                SyntaxKind.NumericLiteralToken => new GreenNumericLiteralExpression(SyntaxKind.NumericLiteralExpression, token),
                SyntaxKind.StringLiteralToken => new GreenStringLiteralExpression(SyntaxKind.StringLiteralExpression,  token),
                SyntaxKind.CharacterLiteralToken => new GreenCharacterLiteralExpression(SyntaxKind.CharacterLiteralExpression, token),
                SyntaxKind.TrueLiteralToken => new GreenTrueLiteralExpression(SyntaxKind.TrueLiteralExpression, token),
                SyntaxKind.FalseLiteralToken => new GreenFalseLiteralExpression(SyntaxKind.FalseLiteralExpression, token),
                SyntaxKind.NullLiteralToken => new GreenNullLiteralExpression(SyntaxKind.NullLiteralExpression, token),
                SyntaxKind.DefaultLiteralToken => new GreenDefaultLiteralExpression(SyntaxKind.DefaultLiteralExpression, token),
                _ => throw new InvalidOperationException($"Invalid literal token kind: {token.Kind}")
            };
        }

    }
}
