using PSharp.CodeAnalysis.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSharp.CodeAnalysis.Binding.Semantics.Conversions;

internal abstract class ConversionsBase
{
    public abstract Conversion ClassifyConversion(TypeSymbol source, TypeSymbol target);
    public abstract Conversion ClassifyImplicitConversion(TypeSymbol source, TypeSymbol target);
    public abstract Conversion ClassifyExplicitConversion(TypeSymbol source, TypeSymbol target);
}
