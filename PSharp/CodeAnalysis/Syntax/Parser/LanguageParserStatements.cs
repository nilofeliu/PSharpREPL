using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSharp.CodeAnalysis.Syntax.Parser;


internal partial class LanguageParser
{

    private GreenBlockStatement ParseBlockStatement()
    {
        var openBrace = EatToken(SyntaxKind.OpenBraceToken);
        var greenStatements = new GreenNodeList(new List<GreenNode>());

        while (CurrentToken.Kind != SyntaxKind.EndOfFileToken &&
               CurrentToken.Kind != SyntaxKind.CloseBraceToken)
        {
            var startToken = CurrentToken;
            var statement = ParseStatement();
            greenStatements.Add(statement);

            if (startToken == CurrentToken)
                EatToken();
        }


        var closeBrace = EatToken(SyntaxKind.CloseBraceToken);
        return new GreenBlockStatement(SyntaxKind.BlockStatement, openBrace, greenStatements, closeBrace);
    }


    private GreenBlockStatement ParseMultiStatements(params SyntaxKind[] terminatingKinds)
    {
        if (CurrentToken.Kind == SyntaxKind.OpenBraceToken)
            return ParseBlockStatement();

        var builder = ImmutableArray.CreateBuilder<GreenStatement>();

        while (!terminatingKinds.Contains(CurrentToken.Kind) &&
               CurrentToken.Kind != SyntaxKind.EndOfFileToken)
        {
            var startToken = CurrentToken;
            var statement = ParseStatement();
            builder.Add(statement);

            if (startToken == CurrentToken)
                EatToken();
        }

        return ParseScopedStatements(builder.ToList());
    }

    private static GreenBlockStatement ParseScopedStatements(List<GreenStatement> statements)
    {
        var openBrace = GreenNodeFactory.MissingToken(SyntaxKind.OpenBraceToken);
        var closeBrace = GreenNodeFactory.MissingToken(SyntaxKind.CloseBraceToken);
        var greenStatements = new GreenNodeList(new List<GreenNode>());
        foreach (var statement in statements) 
            greenStatements.Add(statement);

        return new GreenBlockStatement(SyntaxKind.BlockStatement, openBrace, greenStatements, closeBrace);
    }
    private GreenStatement ParseIfStatement()
    {
        var ifKeyword = EatToken(SyntaxKind.IfKeyword);
        var condition = ParseExpression();
        var colonToken = EatToken(SyntaxKind.ColonToken);
        var thenStatement = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
                                                 SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
        var elseClause = ParseElseClause();
        var endToken = EatToken(SyntaxKind.EndKeyword);
        return new GreenIfStatement(SyntaxKind.IfStatement, ifKeyword, condition, colonToken, thenStatement, elseClause, endToken);
    }

    private GreenElseClause ParseElseClause()
    {
        if (CurrentToken.Kind != SyntaxKind.ElseKeyword && CurrentToken.Kind != SyntaxKind.ElseIfKeyword)
            return null;

        var keyword = EatToken();

        if (keyword.Kind == SyntaxKind.ElseIfKeyword)
        {
            var condition = ParseExpression();
            var elseColonToken = EatToken(SyntaxKind.ColonToken);
            var body = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
                                            SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
            var nestedElseClause = ParseElseClause();

            var syntheticIfKeyword = GreenNodeFactory.Token(SyntaxKind.IfKeyword, "if");
            var syntheticColon = GreenNodeFactory.Token(SyntaxKind.ColonToken, ":");
            var syntheticEnd = GreenNodeFactory.Token(SyntaxKind.EndKeyword, "end");
            var syntheticIf = new GreenIfStatement(SyntaxKind.IfStatement, syntheticIfKeyword, condition, syntheticColon,
                                                   body, nestedElseClause, syntheticEnd);

            return new GreenElseClause(SyntaxKind.ElseClause, keyword, elseColonToken, syntheticIf);
        }
        else
        {
            var elseColonToken = EatToken(SyntaxKind.ColonToken);
            var body = ParseMultiStatements(SyntaxKind.ElseKeyword, SyntaxKind.ElseIfKeyword,
                                            SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
            return new GreenElseClause(SyntaxKind.ElseClause, keyword, elseColonToken, body);
        }
    }

    private GreenStatement ParseWhileStatement()
    {
        var whileKeyword = EatToken(SyntaxKind.WhileKeyword);
        var condition = ParseExpression();
        var colonToken = EatToken(SyntaxKind.ColonToken);
        var body = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken);
        var endToken = EatToken(SyntaxKind.EndKeyword);
        return new GreenWhileStatement(SyntaxKind.WhileStatement, whileKeyword, condition, colonToken, body, endToken);
    }

    private GreenStatement ParseDoWhileStatement()
    {
        var doKeyword = EatToken(SyntaxKind.DoKeyword);
        var colonToken = EatToken(SyntaxKind.ColonToken);
        var body = ParseMultiStatements(SyntaxKind.WhileKeyword);
        var whileToken = EatToken(SyntaxKind.WhileKeyword);
        var condition = ParseExpression();
        return new GreenDoWhileStatement(SyntaxKind.DoStatement, doKeyword, colonToken, body, condition);
    }

    private GreenStatement ParseForStatement()
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
        return new GreenForStatement(SyntaxKind.ForStatement, forKeyword, identifier, equals, lowerBound, toKeyword, upperBound,
                                     colonToken, body, endToken);
    }

    private GreenStatement ParseSwitchStatement()
    {
        var keyword = CurrentToken.Kind == SyntaxKind.MatchKeyword
            ? EatToken(SyntaxKind.MatchKeyword)
            : EatToken(SyntaxKind.SwitchKeyword);

        var pattern = ParseExpression();
        var colonToken = EatToken(SyntaxKind.ColonToken);

        var casesBuilder = new GreenNodeList(new List<GreenNode>());
        GreenDefaultSwitchLabel defaultCase = null;

        while (CurrentToken.Kind != SyntaxKind.EndKeyword &&
               CurrentToken.Kind != SyntaxKind.DefaultKeyword &&
               CurrentToken.Kind != SyntaxKind.EndOfFileToken)
        {
            var startToken = CurrentToken;
            if (CurrentToken.Kind == SyntaxKind.CaseKeyword)
            {
                var caseStmt = ParseCaseSwitchStatement();
                if (caseStmt is GreenCaseSwitchLabel caseLabel &&
                    caseLabel.Expression is GreenNameExpression name &&
                    name.IdentifierToken.Text == "_")
                {          
                    break;
                }
                casesBuilder.Add(caseStmt);
            }
            if (startToken == CurrentToken)
                EatToken();
        }

        if (CurrentToken.Kind == SyntaxKind.DefaultKeyword && defaultCase == null)
            defaultCase = ParseDefaultSwitchStatement();

        var endToken = EatToken(SyntaxKind.EndKeyword);
        return new GreenSwitchStatement(SyntaxKind.SwitchStatement, keyword, pattern, colonToken, casesBuilder, defaultCase, endToken);
    }

    private GreenCaseSwitchLabel ParseCaseSwitchStatement()
    {

        var keyword = EatToken();

            var caseExpression = ParseExpression();
            var caseColonToken = EatToken(SyntaxKind.ColonToken);
            var body = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken,
                                            SyntaxKind.DefaultKeyword, SyntaxKind.CaseKeyword);
            return new GreenCaseSwitchLabel(SyntaxKind.CaseSwitchLabel, keyword, caseExpression, caseColonToken, body);
        }
    private GreenDefaultSwitchLabel ParseDefaultSwitchStatement()
    {


        var keyword = EatToken();
        var caseColonToken = EatToken(SyntaxKind.ColonToken);
        var body = ParseMultiStatements(SyntaxKind.EndKeyword, SyntaxKind.EndOfFileToken,
                                        SyntaxKind.CaseKeyword);
        return new GreenDefaultSwitchLabel(SyntaxKind.DefaultSwitchLabel, keyword, caseColonToken, body);
    }


    private GreenStatement ParseVariableDeclaration()
    {
        // Parse keyword (let, var, or type keyword)
        var expected = CurrentToken.Kind switch
        {
            SyntaxKind.LetKeyword => SyntaxKind.LetKeyword,
            SyntaxKind.VarKeyword => SyntaxKind.VarKeyword,
            _ when SyntaxFacts.IsSpecialTypeKeyword(CurrentToken.Kind) => CurrentToken.Kind,
            _ => SyntaxKind.VarKeyword
        };
        var keyword = EatToken(expected);

        // Optional explicit type
        GreenToken? type = null;
        if (SyntaxFacts.IsSpecialTypeKeyword(CurrentToken.Kind))
        {
            type = EatToken(CurrentToken.Kind);
        }

        // Parse declarators (comma-separated)
        var declarators = new List<GreenNode>();
        declarators.Add(ParseDeclarator());

        while (CurrentToken.Kind == SyntaxKind.CommaToken && CurrentToken.Kind !=SyntaxKind.NewLineTrivia)
        {
            var comma = EatToken(SyntaxKind.CommaToken);
            declarators.Add(comma);
            declarators.Add(ParseDeclarator());
        }

        var variables = new GreenNodeList(declarators);

        return new GreenVariableDeclaration(SyntaxKind.LocalDeclarationStatement, keyword, type, variables);
    }

    private GreenVariableDeclarator ParseDeclarator()
    {
        var identifier = EatToken(SyntaxKind.IdentifierToken);

        GreenEqualsValueClause? initializer = null;
        if (CurrentToken.Kind == SyntaxKind.EqualsToken)
        {
            var equals = EatToken(SyntaxKind.EqualsToken);
            var expression = ParseExpression();
            initializer = new GreenEqualsValueClause(SyntaxKind.EqualsValueClause, equals, expression);
        }

        return new GreenVariableDeclarator(SyntaxKind.VariableDeclarator, identifier, initializer);
    }


    //private GreenStatement ParseVariableDeclaration()
    //{
    //    var expected = CurrentToken.Kind switch
    //    {
    //        SyntaxKind.LetKeyword => SyntaxKind.LetKeyword,
    //        SyntaxKind.VarKeyword => SyntaxKind.VarKeyword,
    //        _ when SyntaxFacts.IsSpecialTypeKeyword(CurrentToken.Kind) => CurrentToken.Kind,
    //        _ => SyntaxKind.VarKeyword
    //    };
    //    var keyword = EatToken(expected);
    //    var identifier = EatToken(SyntaxKind.IdentifierToken);
    //    var equals = EatToken(SyntaxKind.EqualsToken);
    //    var initializer = ParseExpression();
    //    return new GreenVariableDeclaration(SyntaxKind.LocalDeclarationStatement, keyword, identifier, equals, initializer);
    //}
}


