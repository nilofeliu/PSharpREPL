using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Lowering.LoweredStatements;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Lowering;

internal sealed class Lowerer : BoundTreeRewrite
{

    private int _labelCount;
    
    private Lowerer()
    {
    }

    internal BoundLabel GenerateLabel()
    {
        var name = $"Label{++_labelCount}";
        return new BoundLabel(name);
    }

    public static BoundBlockStatement Lower(BoundStatement statement)
    {
        var lowerer = new Lowerer();
        var result = lowerer.RewriteStatement(statement);
        return Flatten(result);
    }

    private static BoundBlockStatement Flatten(BoundStatement statement)
    {
        var builder = ImmutableArray.CreateBuilder<BoundStatement>();
        var stack = new Stack<BoundStatement>();
        stack.Push(statement);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (current is BoundBlockStatement block)
            {
                foreach (var s in block.Statements.Reverse())
                    stack.Push(s);
            }
            else
            {
                builder.Add(current);
            }
        }

        return new BoundBlockStatement(builder.ToImmutable());
    }

    protected override BoundStatement RewriteIfStatement(BoundIfStatement node)
    {
        return IfStatement.Rewrite(this, node);
    }

    protected override BoundStatement RewriteWhileStatement(BoundWhileStatement node)
    {
        return WhileStatement.Rewrite(this, node);
    }

    protected override BoundStatement RewriteDoWhileStatement(BoundDoWhileStatement node)
    {
        return DoWhileStatement.Rewrite(this, node);
    }

    protected override BoundStatement RewriteForStatement(BoundForStatement node)
    {
        return ForStatement.Rewrite(this, node);
    }

    protected override BoundStatement RewriteSwitchStatement(BoundSwitchStatement node)
    {
        return SwitchStatement.Rewrite(this, node);
    }

}

