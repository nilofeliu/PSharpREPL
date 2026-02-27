//using PSharp.CodeAnalysis.Syntax.Kind;
//using PSharp.CodeAnalysis.Syntax.Nodes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PSharp.CodeAnalysis.Syntax.Parser
//{
//    internal partial class SyntaxParser
//    {

//        internal StatementSyntax ParseStatement()
//        {
//            switch (CurrentToken.Kind)
//            {
//                case SyntaxKind.OpenBraceToken:
//                    return ParseBlockStatement();
//                case SyntaxKind.LetKeyword:
//                case SyntaxKind.VarKeyword:
//                    return ParseVariableDeclaration();
//                case SyntaxKind.IfKeyword:
//                    return ParseIfStatement();
//                case SyntaxKind.SwitchKeyword:
//                case SyntaxKind.MatchKeyword:
//                    return ParseSwitchStatement();
//                case SyntaxKind.WhileKeyword:
//                    return ParseWhileStatement();
//                case SyntaxKind.DoKeyword:
//                    return ParseDoWhileStatement();
//                case SyntaxKind.ForKeyword:
//                    return ParseForStatement();
//                default:
//                    if (SyntaxFacts.IsSpecialTypeKeyword(CurrentToken.Kind))
//                        return ParseVariableDeclaration();
//                    return ParseExpressionStatement();
//            }
//        }

//        private ExpressionSyntax ParsePrimaryExpression()
//        {
//            switch (CurrentToken.Kind)
//            {
//                case SyntaxKind.OpenParenthesisToken:
//                    return ParseParenthesizedExpression();

//                case SyntaxKind.TrueKeyword:
//                case SyntaxKind.FalseKeyword:
//                    return ParseBooleanLiteral();

//                case SyntaxKind.NumericLiteralToken:
//                case SyntaxKind.IntegerLiteralToken:
//                case SyntaxKind.LongLiteralToken:
//                case SyntaxKind.FloatLiteralToken:
//                case SyntaxKind.DoubleLiteralToken:
//                case SyntaxKind.DecimalLiteralToken:
//                    return ParseNumberLiteral();

//                case SyntaxKind.StringLiteralToken:
//                    return ParseStringLiteral();

//                default:
//                    return ParseNameExpression();
//            }
//        }

//    }
//}
