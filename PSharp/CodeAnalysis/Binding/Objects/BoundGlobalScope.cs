using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Symbols;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Binding.Objects
{
    internal sealed class BoundGlobalScope
    {
        public BoundGlobalScope(BoundGlobalScope previous, ImmutableArray<Diagnostic> diagnostics,
            ImmutableArray<VariableSymbol> variables, BoundStatement statement)
        {
            Previous = previous;
            Diagnostics = diagnostics;
            Variables = variables;
            Statement = statement;
        }

        public BoundGlobalScope Previous { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }
        public ImmutableArray<VariableSymbol> Variables { get; }
        public BoundStatement Statement { get; }
    }
}

