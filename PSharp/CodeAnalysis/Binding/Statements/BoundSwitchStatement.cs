using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Kind;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Binding.Statements;

internal sealed class BoundSwitchStatement : BoundStatement
{
    public BoundSwitchStatement(
        BoundExpression pattern,
        ImmutableArray<BoundSwitchCase>? cases,
        BoundSwitchCase? defaultCase)
    {
        Pattern = pattern;
        Cases = cases;
        DefaultCase = defaultCase;
    }

    public override BoundNodeKind Kind => BoundNodeKind.SwitchStatement;
    public BoundExpression Pattern { get; }  // ← Added
    public ImmutableArray<BoundSwitchCase>? Cases { get; }
    public BoundSwitchCase? DefaultCase { get; }
}

internal sealed class BoundSwitchCase : BoundNode
{
    public BoundSwitchCase(BoundExpression? pattern, BoundStatement? body)
    {
        Pattern = pattern;
        Body = body;
    }

    public override BoundNodeKind Kind => BoundNodeKind.SwitchCase;

    public BoundExpression? Pattern { get; }  // Null for default case
    public BoundStatement? Body { get; }
}
