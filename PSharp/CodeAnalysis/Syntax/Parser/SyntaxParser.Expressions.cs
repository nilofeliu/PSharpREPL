using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;

namespace Minsk.CodeAnalysis.Syntax.Parser
    {
        internal partial class SyntaxParser
        {

            // --------------------------------------------------------------------
            // Expressions
            // --------------------------------------------------------------------

            internal StatementSyntax ParseExpressionStatement()
            {
                var expression = ParseExpression();
                return new ExpressionStatementSyntax(expression);
            }

            internal ExpressionSyntax ParseExpression()
            {
                return ParseAssignmentExpression();
            }

            private ExpressionSyntax ParseAssignmentExpression()
            {
                if (PeekToken(0).Kind == SyntaxKind.IdentifierToken &&
                    PeekToken(1).Kind == SyntaxKind.EqualsToken)
                {
                    var identifierToken = EatToken();
                    var operatorToken = EatToken();
                    var right = ParseAssignmentExpression();
                    return new AssignmentExpressionSyntax(identifierToken, operatorToken, right);
                }

                return ParseOperatorExpression();
            }

            private ExpressionSyntax ParseOperatorExpression(int parentPrecedence = 0)
            {
                ExpressionSyntax left;
                var unaryPrecedence = CurrentToken.Kind.GetUnaryOperatorPrecedence();
                if (unaryPrecedence != 0 && unaryPrecedence >= parentPrecedence)
                {
                    var operatorToken = EatToken();
                    var operand = ParseOperatorExpression(unaryPrecedence);
                    left = new UnaryExpressionSyntax(operatorToken, operand);
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
                    left = new BinaryExpressionSyntax(left, operatorToken, right);
                }

                return left;
            }

            private ExpressionSyntax ParseParenthesizedExpression()
            {
                var openParen = EatToken(SyntaxKind.OpenParenthesisToken);
                var expression = ParseAssignmentExpression();
                var closeParen = EatToken(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(openParen, expression, closeParen);
            }

            private ExpressionSyntax ParseBooleanLiteral()
            {
                var isTrue = CurrentToken.Kind == SyntaxKind.TrueKeyword;
                var keywordToken = isTrue
                    ? EatToken(SyntaxKind.TrueKeyword)
                    : EatToken(SyntaxKind.FalseKeyword);
                return new LiteralExpressionSyntax(keywordToken, isTrue);
            }

            private ExpressionSyntax ParseNumberLiteral()
            {
                var token = EatToken(); // lexer already returns specific numeric kind
                return new LiteralExpressionSyntax(token);
            }

            private ExpressionSyntax ParseStringLiteral()
            {
                var token = EatToken(SyntaxKind.StringLiteralToken);
                return new LiteralExpressionSyntax(token);
            }

            private ExpressionSyntax ParseNameExpression()
            {
                var token = EatToken(SyntaxKind.IdentifierToken);
                return new NameExpressionSyntax(token);
            }
        }
}
