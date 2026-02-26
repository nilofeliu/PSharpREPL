using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Compilations;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Expressions
{
    internal class BoundNullExpression : BoundExpression
    {
        public override TypeSymbol Type => Compilation.typeOf(TypeKind.Null);
        public override BoundNodeKind Kind => BoundNodeKind.NullExpression;
    }


}