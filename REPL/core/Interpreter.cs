using REPL.language;
using REPL.language.Syntax.Binding;
using REPL.language.Syntax.expressions;
using REPL.systemfiles.diagnostics;
using System.Collections.Immutable;


namespace REPL.core
{
    public class Interpreter
    {
        public SyntaxTree Syntax { get; }

        public Interpreter(SyntaxTree syntax)
        {
            Syntax = syntax;
        }

        public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
        {
            var binder = new Binder(variables);
            var boundExpression = binder.BindExpression((ExpressionSyntax)Syntax.Root);

            var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToImmutableArray();
            if (diagnostics.Length > 0)
                return new EvaluationResult(diagnostics, null);

            var evaluator = new Evaluator(boundExpression, variables);
            var value = evaluator.Evaluate();

            return new EvaluationResult(ImmutableArray<Diagnostic>.Empty, value);
        }

    }
}

