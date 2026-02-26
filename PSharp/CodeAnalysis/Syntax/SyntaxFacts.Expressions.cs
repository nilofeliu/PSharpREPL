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
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParenthesizedExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConditionalExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InvocationExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElementAccessExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArgumentList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BracketedArgumentList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Argument, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NameColon, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CastExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousMethodExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleLambdaExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParenthesizedLambdaExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ObjectInitializerExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CollectionInitializerExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrayInitializerExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousObjectMemberDeclarator, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ComplexElementInitializerExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ObjectCreationExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AnonymousObjectCreationExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrayCreationExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitArrayCreationExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StackAllocArrayCreationExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OmittedArraySizeExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolatedStringExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitElementAccess, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IsPatternExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RangeExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ImplicitObjectCreationExpression, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadBinaryExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SubtractExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MultiplyExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DivideExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ModuloExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LeftShiftExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RightShiftExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalOrExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalAndExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseOrExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseAndExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExclusiveOrExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NotEqualsExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LessThanOrEqualExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GreaterThanOrEqualExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IsExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AsExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CoalesceExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleMemberAccessExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PointerMemberAccessExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConditionalAccessExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsignedRightShiftExpression, ""));
            return list;
        }

        //internal static List<SyntaxSymbol> LoadBindingExpressions()
        //{
        //    var list = new List<SyntaxSymbol>();
        //    TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MemberBindingExpression, ""));
        //    TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElementBindingExpression, ""));
        //    return list;
        //}

        internal static List<SyntaxSymbol> LoadBinaryAssignmentExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SubtractAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MultiplyAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DivideAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ModuloAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AndAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExclusiveOrAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LeftShiftAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RightShiftAssignmentExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CoalesceAssignmentExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsignedRightShiftAssignmentExpression, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadUnaryExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnaryPlusExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnaryMinusExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BitwiseNotExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LogicalNotExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PreIncrementExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PreDecrementExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PointerIndirectionExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddressOfExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PostIncrementExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PostDecrementExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AwaitExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IndexExpression, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadPrimaryExpressions()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThisExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArgListExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NumericLiteralExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StringLiteralExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CharacterLiteralExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TrueLiteralExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FalseLiteralExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NullLiteralExpression, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultLiteralExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Utf8StringLiteralExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FieldExpression, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadPrimaryFunctionExpressions()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeOfExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SizeOfExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CheckedExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UncheckedExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MakeRefExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefValueExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefTypeExpression, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadQueryExpressions()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryExpression, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryBody, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FromClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LetClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.JoinClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.JoinIntoClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhereClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OrderByClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AscendingOrdering, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DescendingOrdering, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SelectClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GroupClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.QueryContinuation, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Block, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LocalDeclarationStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VariableDeclaration, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.VariableDeclarator, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EqualsValueClause, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExpressionStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EmptyStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LabeledStatement, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadJumpStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoCaseStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GotoDefaultStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BreakStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ContinueStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ReturnStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.YieldReturnStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.YieldBreakStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThrowStatement, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadLoopStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.WhileStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DoStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ForEachStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UsingStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FixedStatement, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadCheckedStatements()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CheckedStatement, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UncheckedStatement, ""));
            return list;
        }

        internal static List<SyntaxSymbol> FlowControlStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnsafeStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LockStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IfStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ElseClause, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SwitchStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SwitchSection, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CaseSwitchLabel, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DefaultSwitchLabel, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TryStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchClause, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchDeclaration, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CatchFilterClause, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FinallyClause, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadAdditionalStatements()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.LocalFunctionStatement, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadDeclarations()
        {
            var list = new List<SyntaxSymbol>();
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.CompilationUnit, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GlobalStatement, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NamespaceDeclaration, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UsingDirective, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExternAliasDirective, ""));
            TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FileScopedNamespaceDeclaration, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadAttributes()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeTargetSpecifier, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Attribute, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeArgumentList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AttributeArgument, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.NameEquals, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadTypeDeclarations()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ClassDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StructDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterfaceDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EnumDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DelegateDeclaration, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadTypeConstraints()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SimpleBaseType, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameterConstraintClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConstructorConstraint, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ClassConstraint, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.StructConstraint, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeConstraint, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ExplicitInterfaceSpecifier, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EnumMemberDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.FieldDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EventFieldDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.MethodDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.OperatorDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConversionOperatorDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ConstructorDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AllowsConstraintClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RefStructConstraint, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadMemberDeclarations()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BaseConstructorInitializer, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ThisConstructorInitializer, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.DestructorDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.PropertyDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.EventDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IndexerDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AccessorList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.GetAccessorDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.SetAccessorDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.AddAccessorDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.RemoveAccessorDeclaration, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.UnknownAccessorDeclaration, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadParameters()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ParameterList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.BracketedParameterList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Parameter, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameterList, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.TypeParameter, ""));
            return list;
        }

        internal static List<SyntaxSymbol> LoadMiscellaneous()
        {
            var list = new List<SyntaxSymbol>();
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.IncompleteMember, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.ArrowExpressionClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.Interpolation, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolatedStringText, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolationAlignmentClause, ""));
            //TryAddSymbol(list, new SyntaxSymbol(SyntaxKind.InterpolationFormatClause, ""));
            return list;
        }

    }
}
