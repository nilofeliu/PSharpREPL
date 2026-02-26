using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Lowering;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Lowering.LoweredStatements
{
    internal sealed class WhileStatement
    {
        internal static BoundStatement Rewrite(Lowerer lowerer, BoundWhileStatement node)
        {
            // while <condition>
            //      <body>
            //
            // ----->
            //
            // goto check
            // continue:
            // <body>
            // check:
            // gotoTrue <condition> continue
            // end:
            //

            var continueLabel = lowerer.GenerateLabel();
            var checkLabel = lowerer.GenerateLabel();
            var endLabel = lowerer.GenerateLabel();

            var gotoCheck = new BoundGotoStatement(checkLabel);
            var continueLabelStatement = new BoundLabelStatement(continueLabel);
            var checkLabelStatement = new BoundLabelStatement(checkLabel);
            var gotoTrue = new BoundConditionalGotoStatement(continueLabel, node.Condition);
            var endLabelStatement = new BoundLabelStatement(endLabel);

            var result = new BoundBlockStatement(ImmutableArray.Create(
                gotoCheck,
                continueLabelStatement,
                node.Body,
                checkLabelStatement,
                gotoTrue,
                endLabelStatement
            ));

            return lowerer.RewriteStatement(result);
        }

    }
}