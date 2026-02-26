using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Objects;
using PSharp.CodeAnalysis.Binding.Semantics.Conversions;
using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Compilations;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;
using PSharp.CodeAnalysis.Syntax.Nodes.Statements;
using System.Collections.Immutable;

namespace PSharp.src.CodeAnalysis.Binding;

internal sealed partial class Binder
{
    private readonly DiagnosticBag _diagnostics = new DiagnosticBag();

    private static readonly ConversionResolver _conversions = new ConversionResolver();

    private BoundScope _scope;
    private readonly Compilation _compilation;

    public Binder(BoundScope parent, Compilation compilation)
    {
        _scope = parent ?? new BoundScope(null);
        _compilation = compilation;
    }

    public static BoundGlobalScope BindGlobalScope(BoundGlobalScope previous, CompilationUnitSyntax syntax, Compilation compilation)
    {
        var parentScope = CreateParentScope(previous);

        var binder = new Binder(parentScope, compilation);
        var expression = binder.BindStatement(syntax.Statement);
        var variables = binder._scope.GetDeclaredVariables();
        var diagnostics = binder.Diagnostics.ToImmutableArray();

        if (previous != null)
            diagnostics = diagnostics.InsertRange(0, previous.Diagnostics);


        return new BoundGlobalScope(previous, diagnostics, variables, expression);
    }


    private static BoundScope CreateParentScope(BoundGlobalScope previous)
    {
        var stack = new Stack<BoundGlobalScope>();
        while (previous != null)
        {
            stack.Push(previous);
            previous = previous.Previous;
        }

        var parent = new BoundScope(null);

        while (stack.Count > 0)
        {
            previous = stack.Pop();
            foreach (var v in previous.Variables)
                TryDeclareVariable(parent, v);
        }

        return parent;
    }



    public DiagnosticBag Diagnostics => _diagnostics;

    private BoundStatement BindStatement(StatementSyntax syntax)
    {
        switch (syntax.Kind)
        {
            case SyntaxKind.BlockStatement:
                return BindBlockStatement((BlockStatementSyntax)syntax);
            case SyntaxKind.VariableDeclaration:
                return BindVariableDeclaration((VariableDeclarationSyntax)syntax);
            case SyntaxKind.IfStatement:
                return BindIfStatement((IfStatementSyntax)syntax);
            case SyntaxKind.WhileStatement:
                return BindWhileStatement((WhileStatementSyntax)syntax);
            case SyntaxKind.DoWhileStatement:
                return BindDoWhileStatement((DoWhileStatementSyntax)syntax);
            case SyntaxKind.ForStatement:
                return BindForStatement((ForStatementSyntax)syntax);
            case SyntaxKind.SwitchStatement:
                return BindSwitchStatement((SwitchStatementSyntax)syntax);
            case SyntaxKind.ExpressionStatement:
                return BindExpressionStatement((ExpressionStatementSyntax)syntax);
            default:
                throw new Exception($"Unexpected syntax {syntax.Kind}");
        }
    }

    private BoundStatement BindBlockStatement(BlockStatementSyntax syntax)
    {
        var statements = ImmutableArray.CreateBuilder<BoundStatement>();
        _scope = new BoundScope(_scope);

        foreach (var statementSyntax in syntax.Statements)
        {
            var statement = BindStatement(statementSyntax);
            statements.Add(statement);
        }

        _scope = _scope.Parent;

        return new BoundBlockStatement(statements.ToImmutable());
    }

    private BoundStatement BindVariableDeclaration(VariableDeclarationSyntax syntax)
    {
        var name = !string.IsNullOrEmpty(syntax?.Identifier?.Text) ? syntax.Identifier.Text : "?";
        var isReadOnly = syntax.Keyword.Kind == SyntaxKind.LetKeyword;
        var initializer = BindExpression(syntax.Initializer);
        var variable = new VariableSymbol(name, isReadOnly, initializer.Type);

        if (!TryDeclareVariable(variable, syntax.Identifier))
            return new BoundExpressionStatement(new BoundVoidExpression());

        return new BoundVariableDeclarationStatement(variable, initializer);
    }


    private BoundStatement BindIfStatement(IfStatementSyntax syntax)
    {
        var condition = BindExpression(syntax.Condition, Compilation.typeOf(SpecialType.System_Boolean));
        var thenStatement = BindStatement(syntax.ThenStatement);
        var elseStatement = syntax.ElseClause == null ? null : BindStatement(syntax.ElseClause.ElseStatement);

        return new BoundIfStatement(condition, thenStatement, elseStatement);
    }

    private BoundStatement BindWhileStatement(WhileStatementSyntax syntax)
    {
        var condition = BindExpression(syntax.Condition, Compilation.typeOf(SpecialType.System_Boolean));
        var body = BindStatement(syntax.Body);

        return new BoundWhileStatement(condition, body);
    }

    private BoundStatement BindDoWhileStatement(DoWhileStatementSyntax syntax)
    {
        var body = BindStatement(syntax.Body);
        var condition = BindExpression(syntax.Condition, Compilation.typeOf(SpecialType.System_Boolean));

        return new BoundDoWhileStatement(condition, body);
    }

    private BoundStatement BindForStatement(ForStatementSyntax syntax)
    {
        var lowerBound = BindExpression(syntax.LowerBound, Compilation.typeOf(SpecialType.System_Int32));
        var upperBound = BindExpression(syntax.UpperBound, Compilation.typeOf(SpecialType.System_Int32));

        _scope = new BoundScope(_scope);

        var name = syntax.Identifier.Text;
        var variable = new VariableSymbol(name, true, Compilation.typeOf(SpecialType.System_Int32));

        if (!TryDeclareVariable(variable, syntax.Identifier))
            return new BoundExpressionStatement(new BoundVoidExpression());

        var body = BindStatement(syntax.Body);

        _scope = _scope.Parent;

        return new BoundForStatement(variable, lowerBound, upperBound, body);
    }


    private BoundStatement BindSwitchStatement(SwitchStatementSyntax syntax)
    {
        // Bind the switch value expression
        var boundSwitchValue = BindExpression(syntax.Pattern);

        // Track seen case values for duplicate detection
        var seenValues = new HashSet<object>();

        // Bind each case
        ImmutableArray<BoundSwitchCase>? boundCases = null;

        if (syntax.Cases.HasValue)
        {
            var boundCasesBuilder = ImmutableArray.CreateBuilder<BoundSwitchCase>();

            foreach (var caseClause in syntax.Cases.Value)
            {
                // Bind the case pattern expression
                var boundCasePattern = BindExpression(caseClause.Expression);

                // Check for duplicate literal case values
                if (boundCasePattern is BoundLiteralExpression literal)
                {
                    if (!seenValues.Add(literal.Value))
                    {
                        _diagnostics.ReportDuplicateCaseLabel(caseClause.Expression.Span, literal.Value);
                    }
                }

                // Bind the case body statement
                var boundCaseBody = caseClause.Body != null
                    ? BindStatement(caseClause.Body)
                    : null;

                boundCasesBuilder.Add(new BoundSwitchCase(boundCasePattern, boundCaseBody));
            }

            boundCases = boundCasesBuilder.ToImmutable();
        }

        // Bind default case if present
        BoundSwitchCase? boundDefault = null;
        if (syntax.DefaultCase != null)
        {
            var boundDefaultBody = syntax.DefaultCase.Body != null
                ? BindStatement(syntax.DefaultCase.Body)
                : null;
            boundDefault = new BoundSwitchCase(null, boundDefaultBody);
        }

        return new BoundSwitchStatement(boundSwitchValue, boundCases, boundDefault);
    }


    private BoundExpression BindExpression(ExpressionSyntax syntax, TypeSymbol targetType)
    {
        var result = BindExpression(syntax);

        if (result.Type == Compilation.typeOf(TypeKind.Null) || result.Type == Compilation.typeOf(SpecialType.System_Void))
            return result;

        if (result.Type != targetType)
            _diagnostics.ReportCannotConvert(syntax.Span, result.Type, targetType);

        return result;
    }

    private BoundStatement BindExpressionStatement(ExpressionStatementSyntax syntax)
    {
        var expression = BindExpression(syntax.Expression);
        return new BoundExpressionStatement(expression);
    }

    public BoundExpression BindExpression(ExpressionSyntax syntax)
    {
        switch (syntax.Kind)
        {
            case SyntaxKind.ParenthesisedExpression:
                return BindParenthesizedExpression((ParenthesizedExpressionSyntax)syntax);

            case SyntaxKind.LiteralExpression:
                return BindLiteralExpression((LiteralExpressionSyntax)syntax);

            case SyntaxKind.IdentifierName:
                return BindNameExpression((NameExpressionSyntax)syntax);

            case SyntaxKind.AssignmentExpression:
                return BindAssignmentExpression((AssignmentExpressionSyntax)syntax);

            case SyntaxKind.UnaryExpression:
                return BindUnaryExpression((UnaryExpressionSyntax)syntax);

            case SyntaxKind.BinaryExpression:
                return BindBinaryExpression((BinaryExpressionSyntax)syntax);

            default:
                throw new Exception($"Unexpected syntax node {syntax.Kind}");
        }
    }


    private BoundExpression BindParenthesizedExpression(ParenthesizedExpressionSyntax syntax)
    {
        return BindExpression(syntax.Expression);
    }

    private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
    {
        if (syntax.Value is null)
            return new BoundNullExpression();

        return new BoundLiteralExpression(syntax.Value);
    }

    private BoundExpression BindNameExpression(NameExpressionSyntax syntax)
    {
        var name = syntax.IdentifierToken.Text;

        if (string.IsNullOrEmpty(name))
        {
            // Case where Token was inserted by the parser
            // Error already reported.
            // Just return an error expression.
            return new BoundNullExpression();
        }


        if (!_scope.TryLookup(name, out var variable))
        {
            _diagnostics.ReportUndefinedName(syntax.IdentifierToken.Span, name);
            return new BoundNullExpression();
        }


        return new BoundVariableExpression(variable);
    }

    private BoundExpression BindAssignmentExpression(AssignmentExpressionSyntax syntax)
    {
        var name = syntax.IdentifierToken.Text;
        var boundExpression = BindExpression(syntax.Expression);

        if (!_scope.TryLookup(name, out var variable))
        {
            _diagnostics.ReportUndefinedName(syntax.IdentifierToken.Span, name);
            return boundExpression;
        }

        if (variable.IsReadOnly)
        {
            _diagnostics.ReportCannotAssign(syntax.EqualsToken.Span, name);
        }

        if (boundExpression.Type != variable.Type)
        {
            //if (boundExpression.Type is SpecialTypeData fromSt && variable.Type is SpecialTypeData toSt)
            if (boundExpression.Type.SpecialType != SpecialType.None && variable.Type.SpecialType != SpecialType.None)
            {
                var fromSt = boundExpression.Type;
                var toSt = variable.Type;
                var conversion = _conversions.ClassifyImplicitConversion(fromSt, toSt);
                if (!conversion.Exists)
                {
                    _diagnostics.ReportCannotConvert(syntax.Expression.Span, boundExpression.Type, variable.Type);
                    return new BoundNullExpression();
                }
                boundExpression = new BoundConversionExpression(variable.Type, boundExpression, conversion.Kind);
            }
            else
            {
                _diagnostics.ReportCannotConvert(syntax.Expression.Span, boundExpression.Type, variable.Type);
                return new BoundNullExpression();
            }
        }

        return new BoundAssignmentExpression(variable, boundExpression);
    }

    private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
    {
        var boundOperand = BindExpression(syntax.Operand);

        if (boundOperand.Type == Compilation.typeOf(TypeKind.Null))
            return new BoundNullExpression();

        var boundOperator = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);
        if (boundOperator == null)
        {
            _diagnostics.ReportUndefinedUnaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundOperand.Type);


            return boundOperand;
        }
        return new BoundUnaryExpression(boundOperator, boundOperand);
    }

    private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
    {
        var boundLeft = BindExpression(syntax.Left);
        var boundRight = BindExpression(syntax.Right);
        var boundOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

        if (boundLeft.Type == Compilation.typeOf(TypeKind.Null) || boundRight.Type == Compilation.typeOf(TypeKind.Null))
            return new BoundNullExpression();


        if (boundOperator == null)
        {
            _diagnostics.ReportUndefinedBinaryOperator(
                syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundLeft.Type, boundRight.Type);
            return new BoundVoidExpression();
        }

        return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
    }
}

