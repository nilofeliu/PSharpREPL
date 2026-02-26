using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Expressions;

internal abstract class BoundExpression : BoundNode
{
    public abstract TypeSymbol Type { get; }
}




