using Minsk.CodeAnalysis.Syntax;
using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.src.CodeAnalysis.Binding;

internal sealed partial class Binder
{
    private static bool TryDeclareVariable(BoundScope scope, VariableSymbol variable)
    {
        if (SyntaxFacts.IsKeyword(variable.Name))
        {
            return false;
        }
        return scope.TryDeclare(variable);
    }
    private bool TryDeclareVariable(VariableSymbol variable, SyntaxNode syntax)
    {
        string name = variable.Name;

        if (SyntaxFacts.IsKeyword(name))
        {
            _diagnostics.ReportKeywordAsIdentifier(syntax.Span, name);
            return false;
        }

        if (!_scope.TryDeclare(variable))
        {
            _diagnostics.ReportVariableAlreadyDeclared(syntax.Span, name);
            return false;
        }

        return true;
    }
}

