using REPL.language;

namespace REPL.language.Syntax.Binding;

internal enum BoundNodeKind
{
    None,
    Ref,
    Out,
    In,
    Params,
    LiteralExpression,
    UnaryExpression,
    BinaryExpression,
    VariableExpression,
    AssignmentExpression,
    CommandExpression,
    
}


