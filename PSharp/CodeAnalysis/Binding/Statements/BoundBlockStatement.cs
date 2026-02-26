using PSharp.CodeAnalysis.Binding.Kind;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Binding.Statements;

internal sealed class  BoundBlockStatement : BoundStatement
{
    public BoundBlockStatement(ImmutableArray<BoundStatement> statements)
    {
        Statements = statements;
    }

    public ImmutableArray<BoundStatement> Statements { get; }

    public override BoundNodeKind Kind => BoundNodeKind.BlockStatement;
}
