using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax
{
    public static partial class SyntaxFacts
    {

        // Expressions

        internal static List<SyntaxSymbol> LoadExpressionTypes()
        {

            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParenthesizedExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConditionalExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InvocationExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElementAccessExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArgumentList, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BracketedArgumentList, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Argument, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NameColon, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CastExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousMethodExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleLambdaExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParenthesizedLambdaExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ObjectInitializerExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CollectionInitializerExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrayInitializerExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousObjectMemberDeclarator, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ComplexElementInitializerExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ObjectCreationExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousObjectCreationExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrayCreationExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitArrayCreationExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StackAllocArrayCreationExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OmittedArraySizeExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolatedStringExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitElementAccess, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IsPatternExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RangeExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitObjectCreationExpression, "", SyntaxGroup.None));
            return list;
        }

        internal static List<SyntaxSymbol> LoadBinaryExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SubtractExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MultiplyExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DivideExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ModuloExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LeftShiftExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RightShiftExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsignedRightShiftExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseOrExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseAndExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExclusiveOrExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CoalesceExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleMemberAccessExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PointerMemberAccessExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConditionalAccessExpression, "", SyntaxGroup.BinaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsExpression, "", SyntaxGroup.ComparisonExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NotEqualsExpression, "", SyntaxGroup.ComparisonExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanExpression, "", SyntaxGroup.ComparisonExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanOrEqualExpression, "", SyntaxGroup.ComparisonExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanExpression, "", SyntaxGroup.ComparisonExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanOrEqualExpression, "", SyntaxGroup.ComparisonExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IsExpression, "", SyntaxGroup.ComparisonExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AsExpression, "", SyntaxGroup.ComparisonExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalOrExpression, "", SyntaxGroup.LogicalExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalAndExpression, "", SyntaxGroup.LogicalExpression));
            return list;
        }

        internal static List<SyntaxSymbol> LoadLiteralExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ByteLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SByteLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ShortLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UShortLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IntLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UIntLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LongLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ULongLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FloatLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoubleLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DecimalLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StringLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VoidLiteralExpression, "", SyntaxGroup.LiteralExpression));
            return list;
        }


        //internal static List<SyntaxSymbol> LoadBindingExpressions()
        //{
        //    var list = new List<SyntaxSymbol>();
        //    TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MemberBindingExpression, "", SyntaxGroup.BinaryExpression));
        //    TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElementBindingExpression, "", SyntaxGroup.BinaryExpression));
        //    return list;
        //}

        internal static List<SyntaxSymbol> LoadBinaryAssignmentExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SubtractAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MultiplyAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DivideAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ModuloAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AndAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExclusiveOrAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LeftShiftAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RightShiftAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CoalesceAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsignedRightShiftAssignmentExpression, "", SyntaxGroup.AssignmentExpression));
            return list;
        }

        internal static List<SyntaxSymbol> LoadUnaryExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnaryPlusExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnaryMinusExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseNotExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalNotExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PreIncrementExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PreDecrementExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PointerIndirectionExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddressOfExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PostIncrementExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PostDecrementExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AwaitExpression, "", SyntaxGroup.UnaryExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IndexExpression, "", SyntaxGroup.UnaryExpression));
            return list;
        }

        internal static List<SyntaxSymbol> LoadPrimaryExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThisExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArgListExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NumericLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StringLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CharacterLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TrueLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FalseLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NullLiteralExpression, "", SyntaxGroup.LiteralExpression));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultLiteralExpression, "", SyntaxGroup.LiteralExpression));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Utf8StringLiteralExpression, "", SyntaxGroup.LiteralExpression));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FieldExpression, "", SyntaxGroup.LiteralExpression));
            return list;
        }

        internal static List<SyntaxSymbol> LoadPrimaryFunctionExpressions()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeOfExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SizeOfExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CheckedExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UncheckedExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MakeRefExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefValueExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefTypeExpression, "", SyntaxGroup.None));
            return list;
        }

        internal static List<SyntaxSymbol> LoadQueryExpressions()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryExpression, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryBody, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FromClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LetClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.JoinClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.JoinIntoClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhereClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrderByClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AscendingOrdering, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DescendingOrdering, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SelectClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GroupClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryContinuation, "", SyntaxGroup.None));
            return list;
        }

        internal static List<SyntaxSymbol> LoadStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Block, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LocalDeclarationStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VariableDeclaration, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VariableDeclarator, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsValueClause, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExpressionStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EmptyStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LabeledStatement, "", SyntaxGroup.Statement));
            return list;
        }

        internal static List<SyntaxSymbol> LoadJumpStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoCaseStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoDefaultStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BreakStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ContinueStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReturnStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.YieldReturnStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.YieldBreakStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThrowStatement, "", SyntaxGroup.Statement));
            return list;
        }

        internal static List<SyntaxSymbol> LoadLoopStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhileStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForEachStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UsingStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FixedStatement, "", SyntaxGroup.Statement));
            return list;
        }

        internal static List<SyntaxSymbol> LoadCheckedStatements()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CheckedStatement, "", SyntaxGroup.Statement));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UncheckedStatement, "", SyntaxGroup.Statement));
            return list;
        }

        internal static List<SyntaxSymbol> FlowControlStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsafeStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LockStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IfStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElseClause, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SwitchStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SwitchSection, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CaseSwitchLabel, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultSwitchLabel, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TryStatement, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchClause, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchDeclaration, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchFilterClause, "", SyntaxGroup.Statement));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FinallyClause, "", SyntaxGroup.Statement));
            return list;
        }

        internal static List<SyntaxSymbol> LoadAdditionalStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LocalFunctionStatement, "", SyntaxGroup.Statement));
            return list;
        }

        internal static List<SyntaxSymbol> LoadDeclarations()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CompilationUnit, "", SyntaxGroup.Declaration));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GlobalStatement, "", SyntaxGroup.Declaration));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NamespaceDeclaration, "", SyntaxGroup.Declaration));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UsingDirective, "", SyntaxGroup.Declaration));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExternAliasDirective, "", SyntaxGroup.Declaration));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FileScopedNamespaceDeclaration, "", SyntaxGroup.Declaration));
            return list;
        }

        internal static List<SyntaxSymbol> LoadAttributes()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeList, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeTargetSpecifier, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Attribute, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeArgumentList, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeArgument, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NameEquals, "", SyntaxGroup.None));
            return list;
        }

        internal static List<SyntaxSymbol> LoadTypeDeclarations()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ClassDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StructDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterfaceDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EnumDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DelegateDeclaration, "", SyntaxGroup.Declaration));
            return list;
        }

        internal static List<SyntaxSymbol> LoadTypeConstraints()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseList, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleBaseType, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameterConstraintClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConstructorConstraint, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ClassConstraint, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StructConstraint, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeConstraint, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExplicitInterfaceSpecifier, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EnumMemberDeclaration, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FieldDeclaration, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EventFieldDeclaration, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MethodDeclaration, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OperatorDeclaration, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConversionOperatorDeclaration, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConstructorDeclaration, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AllowsConstraintClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefStructConstraint, "", SyntaxGroup.None));
            return list;
        }

        internal static List<SyntaxSymbol> LoadMemberDeclarations()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseConstructorInitializer, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThisConstructorInitializer, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DestructorDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PropertyDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EventDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IndexerDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AccessorList, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GetAccessorDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SetAccessorDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddAccessorDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RemoveAccessorDeclaration, "", SyntaxGroup.Declaration));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnknownAccessorDeclaration, "", SyntaxGroup.Declaration));
            return list;
        }

        internal static List<SyntaxSymbol> LoadParameters()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParameterList, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BracketedParameterList, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Parameter, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameterList, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameter, "", SyntaxGroup.None));
            return list;
        }

        internal static List<SyntaxSymbol> LoadMiscellaneous()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IncompleteMember, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrowExpressionClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Interpolation, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolatedStringText, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolationAlignmentClause, "", SyntaxGroup.None));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolationFormatClause, "", SyntaxGroup.None));
            return list;
        }

    }
}


//using Minsk.CodeAnalysis.Symbols;
//using Minsk.CodeAnalysis.Syntax.Kind;

//namespace Minsk.CodeAnalysis.Syntax
//{
//    public static partial class SyntaxFacts
//    {

//        // Expressions

//        internal static List<SyntaxSymbol> LoadExpressionTypes()
//        {

//            var list = new List<SyntaxSymbol>();
////            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParenthesizedExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConditionalExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InvocationExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElementAccessExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArgumentList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BracketedArgumentList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Argument, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NameColon, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CastExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousMethodExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleLambdaExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParenthesizedLambdaExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ObjectInitializerExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CollectionInitializerExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrayInitializerExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousObjectMemberDeclarator, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ComplexElementInitializerExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ObjectCreationExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousObjectCreationExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrayCreationExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitArrayCreationExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StackAllocArrayCreationExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OmittedArraySizeExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolatedStringExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitElementAccess, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IsPatternExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RangeExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitObjectCreationExpression, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadBinaryExpressions()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SubtractExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MultiplyExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DivideExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ModuloExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LeftShiftExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RightShiftExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalOrExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalAndExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseOrExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseAndExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExclusiveOrExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NotEqualsExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanOrEqualExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanOrEqualExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IsExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AsExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CoalesceExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleMemberAccessExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PointerMemberAccessExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConditionalAccessExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsignedRightShiftExpression, ""));
//            return list;
//        }

//        //internal static List<SyntaxSymbol> LoadBindingExpressions()
//        //{
//        //    var list = new List<SyntaxSymbol>();
//        //    TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MemberBindingExpression, ""));
//        //    TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElementBindingExpression, ""));
//        //    return list;
//        //}

//        internal static List<SyntaxSymbol> LoadBinaryAssignmentExpressions()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SubtractAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MultiplyAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DivideAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ModuloAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AndAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExclusiveOrAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LeftShiftAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RightShiftAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CoalesceAssignmentExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsignedRightShiftAssignmentExpression, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadUnaryExpressions()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnaryPlusExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnaryMinusExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseNotExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalNotExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PreIncrementExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PreDecrementExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PointerIndirectionExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddressOfExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PostIncrementExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PostDecrementExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AwaitExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IndexExpression, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadPrimaryExpressions()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThisExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArgListExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NumericLiteralExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StringLiteralExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CharacterLiteralExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TrueLiteralExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FalseLiteralExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NullLiteralExpression, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultLiteralExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Utf8StringLiteralExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FieldExpression, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadPrimaryFunctionExpressions()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeOfExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SizeOfExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CheckedExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UncheckedExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MakeRefExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefValueExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefTypeExpression, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadQueryExpressions()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryExpression, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryBody, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FromClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LetClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.JoinClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.JoinIntoClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhereClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrderByClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AscendingOrdering, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DescendingOrdering, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SelectClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GroupClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryContinuation, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadStatements()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Block, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LocalDeclarationStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VariableDeclaration, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VariableDeclarator, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsValueClause, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExpressionStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EmptyStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LabeledStatement, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadJumpStatements()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoCaseStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoDefaultStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BreakStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ContinueStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReturnStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.YieldReturnStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.YieldBreakStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThrowStatement, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadLoopStatements()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhileStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForEachStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UsingStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FixedStatement, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadCheckedStatements()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CheckedStatement, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UncheckedStatement, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> FlowControlStatements()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsafeStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LockStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IfStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElseClause, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SwitchStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SwitchSection, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CaseSwitchLabel, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultSwitchLabel, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TryStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchClause, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchDeclaration, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchFilterClause, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FinallyClause, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadAdditionalStatements()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LocalFunctionStatement, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadDeclarations()
//        {
//            var list = new List<SyntaxSymbol>();
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CompilationUnit, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GlobalStatement, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NamespaceDeclaration, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UsingDirective, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExternAliasDirective, ""));
//            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FileScopedNamespaceDeclaration, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadAttributes()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeTargetSpecifier, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Attribute, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeArgumentList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeArgument, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NameEquals, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadTypeDeclarations()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ClassDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StructDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterfaceDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EnumDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DelegateDeclaration, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadTypeConstraints()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleBaseType, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameterConstraintClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConstructorConstraint, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ClassConstraint, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StructConstraint, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeConstraint, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExplicitInterfaceSpecifier, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EnumMemberDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FieldDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EventFieldDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MethodDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OperatorDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConversionOperatorDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConstructorDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AllowsConstraintClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefStructConstraint, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadMemberDeclarations()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseConstructorInitializer, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThisConstructorInitializer, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DestructorDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PropertyDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EventDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IndexerDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AccessorList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GetAccessorDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SetAccessorDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddAccessorDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RemoveAccessorDeclaration, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnknownAccessorDeclaration, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadParameters()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParameterList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BracketedParameterList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Parameter, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameterList, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameter, ""));
//            return list;
//        }

//        internal static List<SyntaxSymbol> LoadMiscellaneous()
//        {
//            var list = new List<SyntaxSymbol>();
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IncompleteMember, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrowExpressionClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Interpolation, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolatedStringText, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolationAlignmentClause, ""));
//            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolationFormatClause, ""));
//            return list;
//        }

//    }
//}
