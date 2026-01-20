namespace REPL.language.Syntax.Binding;

internal abstract class BoundExpression : BoundNode
{
    public abstract Type Type { get; }
}


