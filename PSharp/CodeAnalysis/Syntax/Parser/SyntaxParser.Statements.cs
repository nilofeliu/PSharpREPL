using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;
using PSharp.CodeAnalysis.Syntax.Nodes.Statements;
using System.Collections.Immutable;

namespace Minsk.CodeAnalysis.Syntax.Parser
{
    internal partial class SyntaxParser
    {

        // --------------------------------------------------------------------
        // Statements
        // --------------------------------------------------------------------



        private StatementSyntax ParseVariableDeclaration()
        {
            var expected = CurrentToken.Kind switch
            {
                SyntaxKind.LetKeyword => SyntaxKind.LetKeyword,
                SyntaxKind.VarKeyword => SyntaxKind.VarKeyword,
                _ when SyntaxFacts.IsSpecialTypeKeyword(CurrentToken.Kind) => CurrentToken.Kind,
                _ => SyntaxKind.VarKeyword
            };
            var keyword = EatToken(expected);
            var identifier = EatToken(SyntaxKind.IdentifierToken);
            var equals = EatToken(SyntaxKind.EqualsToken);
            var initializer = ParseExpression();
            return new VariableDeclarationSyntax(keyword, identifier, equals, initializer);
        }

        private StatementSyntax ParseSwitchStatement()
        {
            var keyword = CurrentToken.Kind == SyntaxKind.MatchKeyword
                ? EatToken(SyntaxKind.MatchKeyword)
                : EatToken(SyntaxKind.SwitchKeyword);

            var pattern = ParseExpression();
            var colonToken = EatToken(SyntaxKind.ColonToken);

            var casesBuilder = ImmutableArray.CreateBuilder<SwitchCaseStatementSyntax>();

            SwitchCaseStatementSyntax defaultCase = null;

            while (CurrentToken.Kind != SyntaxKind.EndKeyword &&
                   CurrentToken.Kind != SyntaxKind.DefaultKeyword &&
                   CurrentToken.Kind != SyntaxKind.EndOfFileToken)
            {
                var startToken = CurrentToken;
                if (CurrentToken.Kind == SyntaxKind.CaseKeyword)
                {
                    var caseStmt = ParseSwitchCaseStatement();
                    if (caseStmt.Expression is NameExpressionSyntax name &&
                        name.IdentifierToken.Kind == SyntaxKind.IdentifierToken &&
                        name.IdentifierToken.Text == "_")
                    {
                        defaultCase = caseStmt;
                        break;
                    }
                    casesBuilder.Add(caseStmt);
                }
                if (startToken == CurrentToken)
                    EatToken();
            }

            var cases = casesBuilder.ToImmutable();

            if (CurrentToken.Kind == SyntaxKind.DefaultKeyword && defaultCase == null)
                defaultCase = ParseSwitchCaseStatement();

            var endToken = EatToken(SyntaxKind.EndKeyword);
            return new SwitchStatementSyntax(keyword, pattern, colonToken, cases, defaultCase, endToken);
        }

        private SwitchCaseStatementSyntax ParseSwitchCaseStatement()
        {
            if (CurrentToken.Kind != SyntaxKind.CaseKeyword && CurrentToken.Kind != SyntaxKind.DefaultKeyword)
                return null;

            var keyword = EatToken();
            ExpressionSyntax caseExpression = null;
            if (keyword.Kind == SyntaxKind.CaseKeyword)
            {
                caseExpression = ParseExpression();
            }

            var caseColonToken = EatToken(SyntaxKind.ColonToken);
            var caseStatement = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken,
                                                     SyntaxKind.DefaultKeyword, SyntaxKind.CaseKeyword);
            return new SwitchCaseStatementSyntax(keyword, caseExpression, caseColonToken, caseStatement);
        }

        private StatementSyntax ParseIfStatement()
        {
            var ifKeyword = EatToken(SyntaxKind.IfKeyword);
            var condition = ParseExpression();
            var colonToken = EatToken(SyntaxKind.ColonToken);
            var thenStatement = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
                                                     SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
            var elseClause = ParseElseClause();
            var endToken = EatToken(SyntaxKind.EndKeyword);
            return new IfStatementSyntax(ifKeyword, condition, colonToken, thenStatement, elseClause, endToken);
        }

        private ElseClauseSyntax ParseElseClause()
        {
            if (CurrentToken.Kind != SyntaxKind.ElseKeyword && CurrentToken.Kind != SyntaxKind.ElseIfKeyword)
                return null;

            var keyword = EatToken();

            if (keyword.Kind == SyntaxKind.ElseIfKeyword)
            {
                var condition = ParseExpression();
                var elseColonToken = EatToken(SyntaxKind.ColonToken);
                var caseStatement = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
                                                         SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
                var nestedElseClause = ParseElseClause();

                // Synthesize an if statement for the else‑if part
                var syntheticIfKeyword = SyntaxFactory.Token(SyntaxKind.IfKeyword, "if");
                var redIfKeyword = new SyntaxToken(syntheticIfKeyword, null, 0);
                var syntheticColon = SyntaxFactory.Token(SyntaxKind.ColonToken, ":");
                var redColon = new SyntaxToken(syntheticColon, null, 1);
                var syntheticEnd = SyntaxFactory.Token(SyntaxKind.EndKeyword, "end");
                var redEndKeyword = new SyntaxToken(syntheticEnd, null, 3);
                var syntheticIf = new IfStatementSyntax(redIfKeyword, condition, redColon,
                                                       caseStatement, nestedElseClause, redEndKeyword);

                return new ElseClauseSyntax(keyword, elseColonToken, syntheticIf);
            }
            else
            {
                var elseColonToken = EatToken(SyntaxKind.ColonToken);
                var caseStatement = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
                                                         SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
                return new ElseClauseSyntax(keyword, elseColonToken, caseStatement);
            }
        }

        private StatementSyntax ParseWhileStatement()
        {
            var whileKeyword = EatToken(SyntaxKind.WhileKeyword);
            var condition = ParseExpression();
            var colonToken = EatToken(SyntaxKind.ColonToken);
            var body = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
            var endToken = EatToken(SyntaxKind.EndKeyword);
            return new WhileStatementSyntax(whileKeyword, condition, colonToken, body, endToken);
        }

        private StatementSyntax ParseDoWhileStatement()
        {
            var doKeyword = EatToken(SyntaxKind.DoKeyword);
            var colonToken = EatToken(SyntaxKind.ColonToken);
            var body = ParseMultiStatements(SyntaxKind.WhileKeyword);
            var whileToken = EatToken(SyntaxKind.WhileKeyword);
            var condition = ParseExpression();
            return new DoWhileStatementSyntax(doKeyword, colonToken, body, condition);
        }

        private StatementSyntax ParseForStatement()
        {
            var forKeyword = EatToken(SyntaxKind.ForKeyword);
            var identifier = EatToken(SyntaxKind.IdentifierToken);
            var equals = EatToken(SyntaxKind.EqualsToken);
            var lowerBound = ParseExpression();
            var toKeyword = EatToken(SyntaxKind.ToKeyword);
            var upperBound = ParseExpression();
            var colonToken = EatToken(SyntaxKind.ColonToken);
            var body = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
            var endToken = EatToken(SyntaxKind.EndKeyword);
            return new ForStatementSyntax(forKeyword, identifier, equals, lowerBound, toKeyword, upperBound,
                                         colonToken, body, endToken);
        }

        private BlockStatementSyntax ParseMultiStatements(params SyntaxKind[] terminatingKinds)
        {
            if (CurrentToken.Kind == SyntaxKind.OpenBraceToken)
            {
                return ParseBlockStatement();
            }

            var builder = ImmutableArray.CreateBuilder<StatementSyntax>();

            while (!terminatingKinds.Contains(CurrentToken.Kind) &&
                   CurrentToken.Kind != SyntaxKind.EndOfFileToken)
            {
                var startToken = CurrentToken;
                var statement = ParseStatement();
                builder.Add(statement);

                if (startToken == CurrentToken)
                    EatToken();
            }

            var statements = builder.ToImmutable();
            return ParseScopedStatements(statements);
        }

        private static BlockStatementSyntax ParseScopedStatements(ImmutableArray<StatementSyntax> statements)
        {
            var openBrace = SyntaxFactory.MissingToken(SyntaxKind.OpenBraceToken);
            var redOpenBrace = new SyntaxToken(openBrace, null, 0);
            var closeBrace = SyntaxFactory.MissingToken(SyntaxKind.CloseBraceToken);
            var redCloseBrace = new SyntaxToken(openBrace, null, 1);
            return new BlockStatementSyntax(redOpenBrace, statements, redCloseBrace);
        }

        private BlockStatementSyntax ParseBlockStatement()
        {
            var openBrace = EatToken(SyntaxKind.OpenBraceToken);
            var statements = ImmutableArray.CreateBuilder<StatementSyntax>();

            while (CurrentToken.Kind != SyntaxKind.EndOfFileToken &&
                   CurrentToken.Kind != SyntaxKind.CloseBraceToken)
            {
                var startToken = CurrentToken;
                var statement = ParseStatement();
                statements.Add(statement);

                if (startToken == CurrentToken)
                    EatToken();
            }

            var closeBrace = EatToken(SyntaxKind.CloseBraceToken);
            return new BlockStatementSyntax(openBrace, statements.ToImmutable(), closeBrace);
        }
    }



}
