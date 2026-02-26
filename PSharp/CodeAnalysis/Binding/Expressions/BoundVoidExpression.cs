using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Compilations;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Expressions
{
    internal class BoundVoidExpression : BoundExpression
    {
        public override TypeSymbol Type => Compilation.typeOf(SpecialType.System_Void);
        public override BoundNodeKind Kind => BoundNodeKind.VoidExpression;
    }


}