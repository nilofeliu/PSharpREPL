using PSharp.CodeAnalysis.Binding.Objects;
using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Lowering;
using PSharp.CodeAnalysis.Metadata;
using PSharp.CodeAnalysis.Symbols;
using PSharp.src.CodeAnalysis.Binding;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Compilations;

public partial class Compilation
{

    private BoundGlobalScope _globalScope;

    // Instance dictionary - non-static, per compilation
    //private Dictionary<SpecialType, TypeSymbol> _wellKnownTypes;

    private static TypeSymbol[] _lazySpecialTypes = new TypeSymbol[(int)SpecialType.System_UIntPtr + 1];


    //  CompilationTypeResolution _compilationType = new();

    public Compilation(SyntaxTree syntaxTree)
    : this(null, syntaxTree)
    {
    }

    private Compilation(Compilation previous, SyntaxTree syntaxTree)
    {
        Previous = previous;
        SyntaxTree = syntaxTree;

        LoadBuiltInTypes(); // Populate from static source
    }

    private void LoadBuiltInTypes()
    {
        foreach (var meta in SpecialTypes.LoadSpecialTypes())
            _lazySpecialTypes[(int)meta.SpecialType] = meta;
    }

    public static IEnumerable<TypeSymbol> GetSpecialTypes()
    => _lazySpecialTypes.Where(t => t != null);

    public Compilation Previous { get; }
    public SyntaxTree SyntaxTree { get; }
            
    internal BoundGlobalScope GlobalScope
    {
        get 
        { 
            if (_globalScope == null)
            {
                var globalScope = Binder.BindGlobalScope(Previous?.GlobalScope, SyntaxTree.Root, this);
                Interlocked.CompareExchange(ref _globalScope, globalScope, null);
            }
            return _globalScope; 
        }
    }
      

    public Compilation ContinueWith(SyntaxTree syntaxTree)
    {
        return new Compilation(this, syntaxTree);
    }

    public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
    {
        var diagnostics = SyntaxTree.Diagnostics.Concat(GlobalScope.Diagnostics).ToImmutableArray();
        if (diagnostics.Length > 0)
            return new EvaluationResult(diagnostics, null);

        var statement = GetStatement();
        var evaluator = new Evaluator(statement, variables, this);
        var value = evaluator.Evaluate();

        return new EvaluationResult(ImmutableArray< Diagnostic>.Empty, value);
    }

    public void EmitTree(TextWriter writer)
    {
        var statement = GetStatement();
        statement.WriteTo(writer);
    }

    private BoundBlockStatement GetStatement()
    {
        var result = GlobalScope.Statement;
        return Lowerer.Lower(result);
    }

}
