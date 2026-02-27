using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Compilations;
using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Lowering.LoweredStatements
{
    internal sealed class ForStatement
    {
  
        internal static BoundStatement Rewrite(Lowerer lowerer, BoundForStatement node)
        {
            // for <var> = <lower> to <upper>
            //      <body>
            //
            // ---->
            //
            // {
            //      var <var> = <lower>
            //      let upperBound = <upper>
            //      while (<var> <= upperBound)
            //      {
            //          <body>
            //          <var> = <var> + 1
            //      }   
            // }

            var variableDeclaration = new BoundVariableDeclarationStatement(node.Variable, node.LowerBound);
            var variableExpression = new BoundVariableExpression(node.Variable);
            var upperBoundSymbol = new VariableSymbol("upperBound", true, Compilation.typeOf(SpecialType.System_Int32));
            var upperBoundDeclaration = new BoundVariableDeclarationStatement(upperBoundSymbol, node.UpperBound);

            var condition = new BoundBinaryExpression(
                variableExpression,
                BoundBinaryOperator.Bind(SyntaxKind.LessThanEqualsToken, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Int32)),                               
                new BoundVariableExpression(upperBoundSymbol)
            );
            var increment = new BoundExpressionStatement(
                new BoundAssignmentExpression(
                    node.Variable,
                    new BoundBinaryExpression(
                            variableExpression,
                            BoundBinaryOperator.Bind(SyntaxKind.PlusToken, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Int32)),
                            new BoundLiteralExpression(1)
                    )
                )
            );
            var whileBody = new BoundBlockStatement(ImmutableArray.Create(node.Body, increment));
            var whileStatement = new BoundWhileStatement(condition, whileBody);
            var result = new BoundBlockStatement(ImmutableArray.Create<BoundStatement>(
                variableDeclaration,
                upperBoundDeclaration,
                whileStatement
                ));
            return lowerer.RewriteStatement(result);
        }
    }
}