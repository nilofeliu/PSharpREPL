using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Binding.Semantics.Conversions;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis.Binding.Expressions;

internal sealed class BoundConversionExpression : BoundExpression
{
    public BoundConversionExpression(
        TypeSymbol targetType,
        BoundExpression expression,
        ConversionKind conversionKind)
    {
        Type = targetType;
        Expression = expression;
        ConversionKind = conversionKind;
    }

    public override BoundNodeKind Kind => BoundNodeKind.ConversionExpression;
    public override TypeSymbol Type { get; }
    public BoundExpression Expression { get; }
    public ConversionKind ConversionKind { get; }
}