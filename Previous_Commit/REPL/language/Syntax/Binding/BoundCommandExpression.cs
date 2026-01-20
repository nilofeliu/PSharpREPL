namespace REPL.language.Syntax.Binding;

internal sealed class BoundCommandExpression : BoundExpression
{
    public BoundCommandExpression(object value)
    {
        Value = value;
    }

    public object Value { get; }
    public override Type Type => Value.GetType();
    public override BoundNodeKind Kind => BoundNodeKind.CommandExpression;
}


