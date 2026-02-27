using PSharp.CodeAnalysis;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Nodes.Statements;
using PSharp.CodeAnalysis.Syntax.Nodes.Expressions;
using PSharp.CodeAnalysis.Syntax.Green.Statements;
using PSharp.CodeAnalysis.Syntax.Green.Expressions;
using System;

namespace PSharp.CodeAnalysis.Syntax
{
    public partial class RedNodeFactory
    {
        public static SyntaxNode CreateRed(GreenNode green, SyntaxNode? parent, int position)
        {
            return green.Kind switch
            {
                SyntaxKind.EqualsValueClause => new EqualsValueClauseSyntax((GreenEqualsValueClause)green, parent, position),
                SyntaxKind.VariableDeclarator => new VariableDeclaratorSyntax((GreenVariableDeclarator)green, parent, position),
                SyntaxKind.VariableDeclaration => new VariableDeclarationSyntax((GreenVariableDeclaration)green, parent, position),
                SyntaxKind.LocalDeclarationStatement => new LocalDeclarationStatementSyntax((GreenLocalDeclarationStatement)green, parent, position),
                SyntaxKind.IdentifierName => new NameExpressionSyntax((GreenNameExpression)green, parent, position),
                SyntaxKind.UnaryMinusExpression => new UnaryMinusExpressionSyntax((GreenUnaryMinusExpression)green, parent, position),
                SyntaxKind.UnaryPlusExpression => new UnaryPlusExpressionSyntax((GreenUnaryPlusExpression)green, parent, position),
                SyntaxKind.LogicalNotExpression => new LogicalNotExpressionSyntax((GreenLogicalNotExpression)green, parent, position),
                SyntaxKind.BitwiseNotExpression => new BitwiseNotExpressionSyntax((GreenBitwiseNotExpression)green, parent, position),
                SyntaxKind.PreIncrementExpression => new PreIncrementExpressionSyntax((GreenPreIncrementExpression)green, parent, position),
                SyntaxKind.PreDecrementExpression => new PreDecrementExpressionSyntax((GreenPreDecrementExpression)green, parent, position),
                SyntaxKind.PostIncrementExpression => new PostIncrementExpressionSyntax((GreenPostIncrementExpression)green, parent, position),
                SyntaxKind.PostDecrementExpression => new PostDecrementExpressionSyntax((GreenPostDecrementExpression)green, parent, position),
                SyntaxKind.AddExpression => new AddExpressionSyntax((GreenAddExpression)green, parent, position),
                SyntaxKind.SubtractExpression => new SubtractExpressionSyntax((GreenSubtractExpression)green, parent, position),
                SyntaxKind.MultiplyExpression => new MultiplyExpressionSyntax((GreenMultiplyExpression)green, parent, position),
                SyntaxKind.DivideExpression => new DivideExpressionSyntax((GreenDivideExpression)green, parent, position),
                SyntaxKind.ModuloExpression => new ModuloExpressionSyntax((GreenModuloExpression)green, parent, position),
                SyntaxKind.EqualsExpression => new EqualsExpressionSyntax((GreenEqualsExpression)green, parent, position),
                SyntaxKind.NotEqualsExpression => new NotEqualsExpressionSyntax((GreenNotEqualsExpression)green, parent, position),
                SyntaxKind.LessThanExpression => new LessThanExpressionSyntax((GreenLessThanExpression)green, parent, position),
                SyntaxKind.LessThanOrEqualExpression => new LessThanOrEqualExpressionSyntax((GreenLessThanOrEqualExpression)green, parent, position),
                SyntaxKind.GreaterThanExpression => new GreaterThanExpressionSyntax((GreenGreaterThanExpression)green, parent, position),
                SyntaxKind.GreaterThanOrEqualExpression => new GreaterThanOrEqualExpressionSyntax((GreenGreaterThanOrEqualExpression)green, parent, position),
                SyntaxKind.LogicalAndExpression => new LogicalAndExpressionSyntax((GreenLogicalAndExpression)green, parent, position),
                SyntaxKind.LogicalOrExpression => new LogicalOrExpressionSyntax((GreenLogicalOrExpression)green, parent, position),
                SyntaxKind.BitwiseAndExpression => new BitwiseAndExpressionSyntax((GreenBitwiseAndExpression)green, parent, position),
                SyntaxKind.BitwiseOrExpression => new BitwiseOrExpressionSyntax((GreenBitwiseOrExpression)green, parent, position),
                SyntaxKind.ExclusiveOrExpression => new ExclusiveOrExpressionSyntax((GreenExclusiveOrExpression)green, parent, position),
                SyntaxKind.CoalesceExpression => new CoalesceExpressionSyntax((GreenCoalesceExpression)green, parent, position),
                SyntaxKind.SimpleAssignmentExpression => new SimpleAssignmentExpressionSyntax((GreenSimpleAssignmentExpression)green, parent, position),
                SyntaxKind.AddAssignmentExpression => new AddAssignmentExpressionSyntax((GreenAddAssignmentExpression)green, parent, position),
                SyntaxKind.SubtractAssignmentExpression => new SubtractAssignmentExpressionSyntax((GreenSubtractAssignmentExpression)green, parent, position),
                SyntaxKind.MultiplyAssignmentExpression => new MultiplyAssignmentExpressionSyntax((GreenMultiplyAssignmentExpression)green, parent, position),
                SyntaxKind.DivideAssignmentExpression => new DivideAssignmentExpressionSyntax((GreenDivideAssignmentExpression)green, parent, position),
                SyntaxKind.ModuloAssignmentExpression => new ModuloAssignmentExpressionSyntax((GreenModuloAssignmentExpression)green, parent, position),
                SyntaxKind.AndAssignmentExpression => new AndAssignmentExpressionSyntax((GreenAndAssignmentExpression)green, parent, position),
                SyntaxKind.OrAssignmentExpression => new OrAssignmentExpressionSyntax((GreenOrAssignmentExpression)green, parent, position),
                SyntaxKind.ExclusiveOrAssignmentExpression => new ExclusiveOrAssignmentExpressionSyntax((GreenExclusiveOrAssignmentExpression)green, parent, position),
                SyntaxKind.LeftShiftAssignmentExpression => new LeftShiftAssignmentExpressionSyntax((GreenLeftShiftAssignmentExpression)green, parent, position),
                SyntaxKind.RightShiftAssignmentExpression => new RightShiftAssignmentExpressionSyntax((GreenRightShiftAssignmentExpression)green, parent, position),
                SyntaxKind.CoalesceAssignmentExpression => new CoalesceAssignmentExpressionSyntax((GreenCoalesceAssignmentExpression)green, parent, position),
                SyntaxKind.ByteLiteralExpression => new ByteLiteralExpressionSyntax((GreenByteLiteralExpression)green, parent, position),
                SyntaxKind.SByteLiteralExpression => new SByteLiteralExpressionSyntax((GreenSByteLiteralExpression)green, parent, position),
                SyntaxKind.ShortLiteralExpression => new ShortLiteralExpressionSyntax((GreenShortLiteralExpression)green, parent, position),
                SyntaxKind.UShortLiteralExpression => new UShortLiteralExpressionSyntax((GreenUShortLiteralExpression)green, parent, position),
                SyntaxKind.IntLiteralExpression => new IntLiteralExpressionSyntax((GreenIntLiteralExpression)green, parent, position),
                SyntaxKind.UIntLiteralExpression => new UIntLiteralExpressionSyntax((GreenUIntLiteralExpression)green, parent, position),
                SyntaxKind.LongLiteralExpression => new LongLiteralExpressionSyntax((GreenLongLiteralExpression)green, parent, position),
                SyntaxKind.ULongLiteralExpression => new ULongLiteralExpressionSyntax((GreenULongLiteralExpression)green, parent, position),
                SyntaxKind.FloatLiteralExpression => new FloatLiteralExpressionSyntax((GreenFloatLiteralExpression)green, parent, position),
                SyntaxKind.DoubleLiteralExpression => new DoubleLiteralExpressionSyntax((GreenDoubleLiteralExpression)green, parent, position),
                SyntaxKind.DecimalLiteralExpression => new DecimalLiteralExpressionSyntax((GreenDecimalLiteralExpression)green, parent, position),
                SyntaxKind.StringLiteralExpression => new StringLiteralExpressionSyntax((GreenStringLiteralExpression)green, parent, position),
                SyntaxKind.VoidLiteralExpression => new VoidLiteralExpressionSyntax((GreenVoidLiteralExpression)green, parent, position),
                SyntaxKind.CharacterLiteralExpression => new CharacterLiteralExpressionSyntax((GreenCharacterLiteralExpression)green, parent, position),
                SyntaxKind.TrueLiteralExpression => new TrueLiteralExpressionSyntax((GreenTrueLiteralExpression)green, parent, position),
                SyntaxKind.FalseLiteralExpression => new FalseLiteralExpressionSyntax((GreenFalseLiteralExpression)green, parent, position),
                SyntaxKind.NullLiteralExpression => new NullLiteralExpressionSyntax((GreenNullLiteralExpression)green, parent, position),
                SyntaxKind.DefaultLiteralExpression => new DefaultLiteralExpressionSyntax((GreenDefaultLiteralExpression)green, parent, position),
                SyntaxKind.ParenthesizedExpression => new ParenthesizedExpressionSyntax((GreenParenthesizedExpression)green, parent, position),
                SyntaxKind.Block => new BlockStatementSyntax((GreenBlockStatement)green, parent, position),
                SyntaxKind.DoWhileStatement => new DoWhileStatementSyntax((GreenDoWhileStatement)green, parent, position),
                SyntaxKind.ForStatement => new ForStatementSyntax((GreenForStatement)green, parent, position),
                SyntaxKind.IfStatement => new IfStatementSyntax((GreenIfStatement)green, parent, position),
                SyntaxKind.ElseClause => new ElseClauseSyntax((GreenElseClause)green, parent, position),
                SyntaxKind.CaseSwitchLabel => new CaseSwitchLabelSyntax((GreenCaseSwitchLabel)green, parent, position),
                SyntaxKind.DefaultSwitchLabel => new DefaultSwitchLabelSyntax((GreenDefaultSwitchLabel)green, parent, position),
                SyntaxKind.SwitchStatement => new SwitchStatementSyntax((GreenSwitchStatement)green, parent, position),
                SyntaxKind.WhileStatement => new WhileStatementSyntax((GreenWhileStatement)green, parent, position),
                _ => throw new InvalidOperationException($"Unknown SyntaxKind: {green.Kind}")
            };
        }
    }
}
