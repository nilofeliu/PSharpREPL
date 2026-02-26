using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Binding.Kind;

namespace PSharp.CodeAnalysis.Binding.Statements
{
    internal sealed class BoundGotoStatement : BoundStatement
    {
        public BoundGotoStatement(BoundLabel label)
        {
            Label = label;
        }

        public override BoundNodeKind Kind => BoundNodeKind.GotoStatement;

        public BoundLabel Label { get; }
    }
}

