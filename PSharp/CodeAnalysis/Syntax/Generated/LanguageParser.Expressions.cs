using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Green;

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

        private GreenNameExpression ParseNameExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            return new GreenNameExpression(SyntaxKind.IdentifierName, identifierToken);
        }

        private GreenUnaryMinusExpression ParseUnaryMinusExpression()
        {
            var minusToken = EatToken(SyntaxKind.MinusToken);
            var operand = ParseExpression();
            return new GreenUnaryMinusExpression(SyntaxKind.UnaryMinusExpression, minusToken, operand);
        }

        private GreenUnaryPlusExpression ParseUnaryPlusExpression()
        {
            var plusToken = EatToken(SyntaxKind.PlusToken);
            var operand = ParseExpression();
            return new GreenUnaryPlusExpression(SyntaxKind.UnaryPlusExpression, plusToken, operand);
        }

        private GreenLogicalNotExpression ParseLogicalNotExpression()
        {
            var bangToken = EatToken(SyntaxKind.BangToken);
            var operand = ParseExpression();
            return new GreenLogicalNotExpression(SyntaxKind.LogicalNotExpression, bangToken, operand);
        }

        private GreenBitwiseNotExpression ParseBitwiseNotExpression()
        {
            var tildeToken = EatToken(SyntaxKind.TildeToken);
            var operand = ParseExpression();
            return new GreenBitwiseNotExpression(SyntaxKind.BitwiseNotExpression, tildeToken, operand);
        }

        private GreenPreIncrementExpression ParsePreIncrementExpression()
        {
            var plusPlusToken = EatToken(SyntaxKind.PlusPlusToken);
            var operand = ParseExpression();
            return new GreenPreIncrementExpression(SyntaxKind.PreIncrementExpression, plusPlusToken, operand);
        }

        private GreenPreDecrementExpression ParsePreDecrementExpression()
        {
            var minusMinusToken = EatToken(SyntaxKind.MinusMinusToken);
            var operand = ParseExpression();
            return new GreenPreDecrementExpression(SyntaxKind.PreDecrementExpression, minusMinusToken, operand);
        }

        private GreenPostIncrementExpression ParsePostIncrementExpression()
        {
            var operand = ParseExpression();
            var plusPlusToken = EatToken(SyntaxKind.PlusPlusToken);
            return new GreenPostIncrementExpression(SyntaxKind.PostIncrementExpression, operand, plusPlusToken);
        }

        private GreenPostDecrementExpression ParsePostDecrementExpression()
        {
            var operand = ParseExpression();
            var minusMinusToken = EatToken(SyntaxKind.MinusMinusToken);
            return new GreenPostDecrementExpression(SyntaxKind.PostDecrementExpression, operand, minusMinusToken);
        }

        private GreenAddExpression ParseAddExpression()
        {
            var left = ParseExpression();
            var plusToken = EatToken(SyntaxKind.PlusToken);
            var right = ParseExpression();
            return new GreenAddExpression(SyntaxKind.AddExpression, left, plusToken, right);
        }

        private GreenSubtractExpression ParseSubtractExpression()
        {
            var left = ParseExpression();
            var minusToken = EatToken(SyntaxKind.MinusToken);
            var right = ParseExpression();
            return new GreenSubtractExpression(SyntaxKind.SubtractExpression, left, minusToken, right);
        }

        private GreenMultiplyExpression ParseMultiplyExpression()
        {
            var left = ParseExpression();
            var starToken = EatToken(SyntaxKind.StarToken);
            var right = ParseExpression();
            return new GreenMultiplyExpression(SyntaxKind.MultiplyExpression, left, starToken, right);
        }

        private GreenDivideExpression ParseDivideExpression()
        {
            var left = ParseExpression();
            var slashToken = EatToken(SyntaxKind.SlashToken);
            var right = ParseExpression();
            return new GreenDivideExpression(SyntaxKind.DivideExpression, left, slashToken, right);
        }

        private GreenModuloExpression ParseModuloExpression()
        {
            var left = ParseExpression();
            var percentToken = EatToken(SyntaxKind.PercentToken);
            var right = ParseExpression();
            return new GreenModuloExpression(SyntaxKind.ModuloExpression, left, percentToken, right);
        }

        private GreenEqualsExpression ParseEqualsExpression()
        {
            var left = ParseExpression();
            var equalsEqualsToken = EatToken(SyntaxKind.EqualsEqualsToken);
            var right = ParseExpression();
            return new GreenEqualsExpression(SyntaxKind.EqualsExpression, left, equalsEqualsToken, right);
        }

        private GreenNotEqualsExpression ParseNotEqualsExpression()
        {
            var left = ParseExpression();
            var bangEqualsToken = EatToken(SyntaxKind.BangEqualsToken);
            var right = ParseExpression();
            return new GreenNotEqualsExpression(SyntaxKind.NotEqualsExpression, left, bangEqualsToken, right);
        }

        private GreenLessThanExpression ParseLessThanExpression()
        {
            var left = ParseExpression();
            var lessThanToken = EatToken(SyntaxKind.LessThanToken);
            var right = ParseExpression();
            return new GreenLessThanExpression(SyntaxKind.LessThanExpression, left, lessThanToken, right);
        }

        private GreenLessThanOrEqualExpression ParseLessThanOrEqualExpression()
        {
            var left = ParseExpression();
            var lessThanEqualsToken = EatToken(SyntaxKind.LessThanEqualsToken);
            var right = ParseExpression();
            return new GreenLessThanOrEqualExpression(SyntaxKind.LessThanOrEqualExpression, left, lessThanEqualsToken, right);
        }

        private GreenGreaterThanExpression ParseGreaterThanExpression()
        {
            var left = ParseExpression();
            var greaterThanToken = EatToken(SyntaxKind.GreaterThanToken);
            var right = ParseExpression();
            return new GreenGreaterThanExpression(SyntaxKind.GreaterThanExpression, left, greaterThanToken, right);
        }

        private GreenGreaterThanOrEqualExpression ParseGreaterThanOrEqualExpression()
        {
            var left = ParseExpression();
            var greaterThanEqualsToken = EatToken(SyntaxKind.GreaterThanEqualsToken);
            var right = ParseExpression();
            return new GreenGreaterThanOrEqualExpression(SyntaxKind.GreaterThanOrEqualExpression, left, greaterThanEqualsToken, right);
        }

        private GreenLogicalAndExpression ParseLogicalAndExpression()
        {
            var left = ParseExpression();
            var ampersandAmpersandToken = EatToken(SyntaxKind.AmpersandAmpersandToken);
            var right = ParseExpression();
            return new GreenLogicalAndExpression(SyntaxKind.LogicalAndExpression, left, ampersandAmpersandToken, right);
        }

        private GreenLogicalOrExpression ParseLogicalOrExpression()
        {
            var left = ParseExpression();
            var pipePipeToken = EatToken(SyntaxKind.PipePipeToken);
            var right = ParseExpression();
            return new GreenLogicalOrExpression(SyntaxKind.LogicalOrExpression, left, pipePipeToken, right);
        }

        private GreenBitwiseAndExpression ParseBitwiseAndExpression()
        {
            var left = ParseExpression();
            var ampersandToken = EatToken(SyntaxKind.AmpersandToken);
            var right = ParseExpression();
            return new GreenBitwiseAndExpression(SyntaxKind.BitwiseAndExpression, left, ampersandToken, right);
        }

        private GreenBitwiseOrExpression ParseBitwiseOrExpression()
        {
            var left = ParseExpression();
            var pipeToken = EatToken(SyntaxKind.PipeToken);
            var right = ParseExpression();
            return new GreenBitwiseOrExpression(SyntaxKind.BitwiseOrExpression, left, pipeToken, right);
        }

        private GreenExclusiveOrExpression ParseExclusiveOrExpression()
        {
            var left = ParseExpression();
            var caretToken = EatToken(SyntaxKind.CaretToken);
            var right = ParseExpression();
            return new GreenExclusiveOrExpression(SyntaxKind.ExclusiveOrExpression, left, caretToken, right);
        }

        private GreenCoalesceExpression ParseCoalesceExpression()
        {
            var left = ParseExpression();
            var questionQuestionToken = EatToken(SyntaxKind.QuestionQuestionToken);
            var right = ParseExpression();
            return new GreenCoalesceExpression(SyntaxKind.CoalesceExpression, left, questionQuestionToken, right);
        }

        private GreenSimpleAssignmentExpression ParseSimpleAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var equalsToken = EatToken(SyntaxKind.EqualsToken);
            var expression = ParseExpression();
            return new GreenSimpleAssignmentExpression(SyntaxKind.SimpleAssignmentExpression, identifierToken, equalsToken, expression);
        }

        private GreenAddAssignmentExpression ParseAddAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var plusEqualsToken = EatToken(SyntaxKind.PlusEqualsToken);
            var expression = ParseExpression();
            return new GreenAddAssignmentExpression(SyntaxKind.AddAssignmentExpression, identifierToken, plusEqualsToken, expression);
        }

        private GreenSubtractAssignmentExpression ParseSubtractAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var minusEqualsToken = EatToken(SyntaxKind.MinusEqualsToken);
            var expression = ParseExpression();
            return new GreenSubtractAssignmentExpression(SyntaxKind.SubtractAssignmentExpression, identifierToken, minusEqualsToken, expression);
        }

        private GreenMultiplyAssignmentExpression ParseMultiplyAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var starEqualsToken = EatToken(SyntaxKind.StarEqualsToken);
            var expression = ParseExpression();
            return new GreenMultiplyAssignmentExpression(SyntaxKind.MultiplyAssignmentExpression, identifierToken, starEqualsToken, expression);
        }

        private GreenDivideAssignmentExpression ParseDivideAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var slashEqualsToken = EatToken(SyntaxKind.SlashEqualsToken);
            var expression = ParseExpression();
            return new GreenDivideAssignmentExpression(SyntaxKind.DivideAssignmentExpression, identifierToken, slashEqualsToken, expression);
        }

        private GreenModuloAssignmentExpression ParseModuloAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var percentEqualsToken = EatToken(SyntaxKind.PercentEqualsToken);
            var expression = ParseExpression();
            return new GreenModuloAssignmentExpression(SyntaxKind.ModuloAssignmentExpression, identifierToken, percentEqualsToken, expression);
        }

        private GreenAndAssignmentExpression ParseAndAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var ampersandEqualsToken = EatToken(SyntaxKind.AmpersandEqualsToken);
            var expression = ParseExpression();
            return new GreenAndAssignmentExpression(SyntaxKind.AndAssignmentExpression, identifierToken, ampersandEqualsToken, expression);
        }

        private GreenOrAssignmentExpression ParseOrAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var pipeEqualsToken = EatToken(SyntaxKind.PipeEqualsToken);
            var expression = ParseExpression();
            return new GreenOrAssignmentExpression(SyntaxKind.OrAssignmentExpression, identifierToken, pipeEqualsToken, expression);
        }

        private GreenExclusiveOrAssignmentExpression ParseExclusiveOrAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var caretEqualsToken = EatToken(SyntaxKind.CaretEqualsToken);
            var expression = ParseExpression();
            return new GreenExclusiveOrAssignmentExpression(SyntaxKind.ExclusiveOrAssignmentExpression, identifierToken, caretEqualsToken, expression);
        }

        private GreenLeftShiftAssignmentExpression ParseLeftShiftAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var lessThanLessThanEqualsToken = EatToken(SyntaxKind.LessThanLessThanEqualsToken);
            var expression = ParseExpression();
            return new GreenLeftShiftAssignmentExpression(SyntaxKind.LeftShiftAssignmentExpression, identifierToken, lessThanLessThanEqualsToken, expression);
        }

        private GreenRightShiftAssignmentExpression ParseRightShiftAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var greaterThanGreaterThanEqualsToken = EatToken(SyntaxKind.GreaterThanGreaterThanEqualsToken);
            var expression = ParseExpression();
            return new GreenRightShiftAssignmentExpression(SyntaxKind.RightShiftAssignmentExpression, identifierToken, greaterThanGreaterThanEqualsToken, expression);
        }

        private GreenCoalesceAssignmentExpression ParseCoalesceAssignmentExpression()
        {
            var identifierToken = EatToken(SyntaxKind.IdentifierToken);
            var questionQuestionEqualsToken = EatToken(SyntaxKind.QuestionQuestionEqualsToken);
            var expression = ParseExpression();
            return new GreenCoalesceAssignmentExpression(SyntaxKind.CoalesceAssignmentExpression, identifierToken, questionQuestionEqualsToken, expression);
        }

        private GreenByteLiteralExpression ParseByteLiteralExpression()
        {
            var byteLiteralToken = EatToken(SyntaxKind.ByteLiteralToken);
            return new GreenByteLiteralExpression(SyntaxKind.ByteLiteralExpression, byteLiteralToken);
        }

        private GreenSByteLiteralExpression ParseSByteLiteralExpression()
        {
            var sByteLiteralToken = EatToken(SyntaxKind.SByteLiteralToken);
            return new GreenSByteLiteralExpression(SyntaxKind.SByteLiteralExpression, sByteLiteralToken);
        }

        private GreenShortLiteralExpression ParseShortLiteralExpression()
        {
            var shortLiteralToken = EatToken(SyntaxKind.ShortLiteralToken);
            return new GreenShortLiteralExpression(SyntaxKind.ShortLiteralExpression, shortLiteralToken);
        }

        private GreenUShortLiteralExpression ParseUShortLiteralExpression()
        {
            var uShortLiteralToken = EatToken(SyntaxKind.UShortLiteralToken);
            return new GreenUShortLiteralExpression(SyntaxKind.UShortLiteralExpression, uShortLiteralToken);
        }

        private GreenIntLiteralExpression ParseIntLiteralExpression()
        {
            var intLiteralToken = EatToken(SyntaxKind.IntLiteralToken);
            return new GreenIntLiteralExpression(SyntaxKind.IntLiteralExpression, intLiteralToken);
        }

        private GreenUIntLiteralExpression ParseUIntLiteralExpression()
        {
            var uIntLiteralToken = EatToken(SyntaxKind.UIntLiteralToken);
            return new GreenUIntLiteralExpression(SyntaxKind.UIntLiteralExpression, uIntLiteralToken);
        }

        private GreenLongLiteralExpression ParseLongLiteralExpression()
        {
            var longLiteralToken = EatToken(SyntaxKind.LongLiteralToken);
            return new GreenLongLiteralExpression(SyntaxKind.LongLiteralExpression, longLiteralToken);
        }

        private GreenULongLiteralExpression ParseULongLiteralExpression()
        {
            var uLongLiteralToken = EatToken(SyntaxKind.ULongLiteralToken);
            return new GreenULongLiteralExpression(SyntaxKind.ULongLiteralExpression, uLongLiteralToken);
        }

        private GreenFloatLiteralExpression ParseFloatLiteralExpression()
        {
            var floatLiteralToken = EatToken(SyntaxKind.FloatLiteralToken);
            return new GreenFloatLiteralExpression(SyntaxKind.FloatLiteralExpression, floatLiteralToken);
        }

        private GreenDoubleLiteralExpression ParseDoubleLiteralExpression()
        {
            var doubleLiteralToken = EatToken(SyntaxKind.DoubleLiteralToken);
            return new GreenDoubleLiteralExpression(SyntaxKind.DoubleLiteralExpression, doubleLiteralToken);
        }

        private GreenDecimalLiteralExpression ParseDecimalLiteralExpression()
        {
            var decimalLiteralToken = EatToken(SyntaxKind.DecimalLiteralToken);
            return new GreenDecimalLiteralExpression(SyntaxKind.DecimalLiteralExpression, decimalLiteralToken);
        }

        private GreenStringLiteralExpression ParseStringLiteralExpression()
        {
            var stringLiteralToken = EatToken(SyntaxKind.StringLiteralToken);
            return new GreenStringLiteralExpression(SyntaxKind.StringLiteralExpression, stringLiteralToken);
        }

        private GreenVoidLiteralExpression ParseVoidLiteralExpression()
        {
            var voidLiteralToken = EatToken(SyntaxKind.VoidLiteralToken);
            return new GreenVoidLiteralExpression(SyntaxKind.VoidLiteralExpression, voidLiteralToken);
        }

        private GreenCharacterLiteralExpression ParseCharacterLiteralExpression()
        {
            var characterLiteralToken = EatToken(SyntaxKind.CharacterLiteralToken);
            return new GreenCharacterLiteralExpression(SyntaxKind.CharacterLiteralExpression, characterLiteralToken);
        }

        private GreenTrueLiteralExpression ParseTrueLiteralExpression()
        {
            var trueLiteralToken = EatToken(SyntaxKind.TrueLiteralToken);
            return new GreenTrueLiteralExpression(SyntaxKind.TrueLiteralExpression, trueLiteralToken);
        }

        private GreenFalseLiteralExpression ParseFalseLiteralExpression()
        {
            var falseLiteralToken = EatToken(SyntaxKind.FalseLiteralToken);
            return new GreenFalseLiteralExpression(SyntaxKind.FalseLiteralExpression, falseLiteralToken);
        }

        private GreenNullLiteralExpression ParseNullLiteralExpression()
        {
            var nullLiteralToken = EatToken(SyntaxKind.NullLiteralToken);
            return new GreenNullLiteralExpression(SyntaxKind.NullLiteralExpression, nullLiteralToken);
        }

        private GreenDefaultLiteralExpression ParseDefaultLiteralExpression()
        {
            var defaultLiteralToken = EatToken(SyntaxKind.DefaultLiteralToken);
            return new GreenDefaultLiteralExpression(SyntaxKind.DefaultLiteralExpression, defaultLiteralToken);
        }

    }
}
