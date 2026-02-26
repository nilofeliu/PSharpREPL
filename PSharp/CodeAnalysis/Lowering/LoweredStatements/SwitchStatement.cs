using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Lowering;
using PSharp.CodeAnalysis.Syntax.Kind;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSharp.CodeAnalysis.Lowering.LoweredStatements
{
    internal sealed class SwitchStatement
    {
        internal static BoundStatement Rewrite(Lowerer lowerer, BoundSwitchStatement node)
        {
            // switch (x) {
            //     case 1: stmt1;
            //     case 2: stmt2;
            //     default: stmtDefault;
            // }
            //
            // ---->
            //
            // gotoIfTrue (x == 1) case1Label
            // gotoIfTrue (x == 2) case2Label
            // goto defaultLabel (or endLabel if no default)
            // case1Label:
            // stmt1
            // goto endLabel
            // case2Label:
            // stmt2
            // goto endLabel
            // defaultLabel:
            // stmtDefault
            // goto endLabel
            // endLabel:

            var statements = ImmutableArray.CreateBuilder<BoundStatement>();
            var endLabel = lowerer.GenerateLabel();

            // Generate all case labels upfront
            ImmutableArray<BoundLabel> caseLabels = ImmutableArray<BoundLabel>.Empty;

            if (node.Cases.HasValue)
            {
                var caseLabelsBuilder = ImmutableArray.CreateBuilder<BoundLabel>(node.Cases.Value.Length);

                for (int i = 0; i < node.Cases.Value.Length; i++)
                {
                    caseLabelsBuilder.Add(lowerer.GenerateLabel());
                }

                caseLabels = caseLabelsBuilder.ToImmutable();
            }

            var defaultLabel = node.DefaultCase != null ? lowerer.GenerateLabel() : endLabel;

            // Generate conditional gotos for each case
            if (node.Cases.HasValue)
            {
                for (int i = 0; i < node.Cases.Value.Length; i++)
                {
                    var caseClause = node.Cases.Value[i];
                    var condition = new BoundBinaryExpression(
                        node.Pattern,
                        BoundBinaryOperator.Bind(SyntaxKind.EqualsEqualsToken, node.Pattern.Type, caseClause.Pattern.Type),
                        caseClause.Pattern
                    );

                    statements.Add(new BoundConditionalGotoStatement(caseLabels[i], condition, jumpIfTrue: true));
                }
            }

            // goto default (or end if no default)
            statements.Add(new BoundGotoStatement(defaultLabel));

            // Generate case bodies with labels
            if (node.Cases.HasValue)
            {
                for (int i = 0; i < node.Cases.Value.Length; i++)
                {
                    statements.Add(new BoundLabelStatement(caseLabels[i]));

                    if (node.Cases.Value[i].Body != null &&
                        !(node.Cases.Value[i].Body is BoundBlockStatement block && block.Statements.Length == 0))
                    {
                        statements.Add(node.Cases.Value[i].Body);
                        statements.Add(new BoundGotoStatement(endLabel)); // ← Only add if has body
                    }
                    // Empty body = no goto, falls through to next label
                }
            }


            // Generate default body if present
            if (node.DefaultCase != null)
            {
                statements.Add(new BoundLabelStatement(defaultLabel));
                statements.Add(node.DefaultCase.Body);
                statements.Add(new BoundGotoStatement(endLabel));
            }

            // End label
            statements.Add(new BoundLabelStatement(endLabel));

            var result = new BoundBlockStatement(statements.ToImmutable());
            return lowerer.RewriteStatement(result);
        }
    }
}
