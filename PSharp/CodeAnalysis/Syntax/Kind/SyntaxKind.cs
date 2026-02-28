namespace PSharp.CodeAnalysis.Syntax.Kind
{
    public enum SyntaxKind
    {
        None,

        //Trivia Tokens
        WhiteSpaceTrivia,
        NewLineTrivia,
        TabTrivia,
        SingleLineCommentTrivia,
        MultiLineCommentTrivia,
        ShebangDirectiveTrivia,
        LoadDirectiveTrivia,
        TriviaList,

        // Tokens
        BadToken,
        EndOfFileToken,
        NumericLiteralToken,
        StringLiteralToken,
        InterpolatedStringLiteralToken,
        VerbatimStringLiteralToken,

        // Scope Tokens
        OpenParenthesisToken,
        CloseParenthesisToken,
        CloseBraceToken,
        OpenBraceToken,

        // Other Tokens
        IdentifierToken,

        //Punctuation Token
        ColonToken,

        // Nodes
        CompilationUnit,
        ElseClause,
        ElseIfClause,
        CaseSwitchLabel,
        DefaultSwitchLabel,
        // Operators

        // Arithmetic
        StarToken,
        SlashToken,
        PercentToken,
        PlusToken,
        MinusToken,
        PlusPlusToken,
        MinusMinusToken,

        // Comparison
        EqualsEqualsToken,
        BangEqualsToken,
        GreaterToken,
        GreaterOrEqualsToken,
        LessToken,
        LessThanEqualsToken,
        GreaterThanToken,
        GreaterThanEqualsToken,
        LessThanToken,
        LessThanOrEqualsToken,

        // Logical
        AmpersandAmpersandToken,
        PipePipeToken,
        QuestionQuestionToken,

        // Bitwise
        AmpersandToken,
        PipeToken,
        CaretToken,
        TildeToken,

        // Unary
        BangToken,

        // Assignment
        EqualsToken,

        // Compound Assignment
        PlusEqualsToken,
        MinusEqualsToken,
        StarEqualsToken,
        SlashEqualsToken,
        PercentEqualsToken,
        AmpersandEqualsToken,
        PipeEqualsToken,
        CaretEqualsToken,
        LessThanLessThanEqualsToken,
        GreaterThanGreaterThanEqualsToken,
        QuestionQuestionEqualsToken,

        // Literal Tokens


        // Boolean Keywords
        TrueKeyword,
        FalseKeyword,

        // Assignment Keywords
        LetKeyword,
        VarKeyword,

        // Control Keywords
        DoKeyword,
        ForKeyword,
        WhileKeyword,
        IfKeyword,
        ElseIfKeyword,
        ElseKeyword,
        ToKeyword,
        SwitchKeyword,
        CaseKeyword,
        DefaultKeyword,
        MatchKeyword,
        ForEachKeyword,

        // Iterators
        YieldKeyword,
        ContinueKeyword,
        BreakKeyword,
        EndKeyword,
        ReturnKeyword,

        // Namespace & Imports
        UsingKeyword,

        //Exceptions Keywords
        TryKeyword,
        CatchKeyword,
        FinallyKeyword,
        LockKeyword,
        GotoKeyword,
        ThrowKeyword,

        //Special Types Keywords
        CharKeyword,
        BoolKeyword,
        ByteKeyword,
        SByteKeyword,
        ShortKeyword,
        UShortKeyword,
        IntegerKeyword,
        UIntegerKeyword,
        LongKeyword,
        ULongKeyword,
        FloatKeyword,
        DoubleKeyword,
        DecimalKeyword,
        StringKeyword,
        VoidKeyword,


        // Literal Type Tokens
        //HexLongLiteralToken,
        //HexIntegerLiteralToken,
        FloatLiteralToken,
        DoubleLiteralToken,
        DecimalLiteralToken,
        LongLiteralToken,
        ULongLiteralToken,
        UIntLiteralToken,
        IntLiteralToken,
        BinaryIntegerLiteralToken,
        ByteLiteralToken,
        SByteLiteralToken,
        ShortLiteralToken,
        UShortLiteralToken,
       // IntLiteralToken,
        NullLiteralToken,
        DefaultLiteralToken,
        VoidLiteralToken,
        TrueLiteralToken,
        FalseLiteralToken,

        // Type Kind Keywords
        ClassKeyword,
        StructKeyword,
        InterfaceKeyword,
        EnumKeyword,
        DelegateKeyword,
        RecordKeyword,
        NamespaceKeyword,
        ObjectKeyword,
        NullKeyword,

        // Type Operators
        TypeOfKeyword,
        SizeOfKeyword,

        // Access Modifiers
        PublicKeyword,
        PrivateKeyword,
        InternalKeyword,
        ProtectedKeyword,

        // Type Modifiers
        StaticKeyword,
        AbstractKeyword,
        SealedKeyword,
        VirtualKeyword,
        OverrideKeyword,
        NewKeyword,
        ReadOnlyKeyword,
        ConstKeyword,

        // Type Definitions
        PartialKeyword,
        AliasKeyword,
        GlobalKeyword,

        // Member Accessors
        GetKeyword,
        SetKeyword,
        InitKeyword,
        EventKeyword,
        PropertyKeyword,

        // Memory & Pointers
        FixedKeyword,
        StackAllocKeyword,
        VolatileKeyword,

        // Reference Parameters
        RefKeyword,
        OutKeyword,
        InKeyword,

        // External/Unmanaged
        ExternKeyword,

        // Type Testing
        IsKeyword,
        AsKeyword,

        // Special Parameters
        ParamsKeyword,

        // Low-level Operations
        ArgListKeyword,
        MakeRefKeyword,
        RefTypeKeyword,
        RefValueKeyword,

        // Instance References
        ThisKeyword,
        BaseKeyword,

        // Checked Context
        CheckedKeyword,
        UncheckedKeyword,
        UnsafeKeyword,

        // Operator Overloading
        OperatorKeyword,
        ExplicitKeyword,
        ImplicitKeyword,

        // Metadata Attributes
        AssemblyKeyword,
        ModuleKeyword,
        TypeKeyword,
        FieldKeyword,
        MethodKeyword,
        ParamKeyword,
        TypeVarKeyword,


        // Expressions ───────────────────────────────────────────────────────
        AssignmentExpression,
        BinaryExpression,
        LiteralExpression,
        IdentifierName,
        ParenthesisedExpression,
        UnaryExpression,
        SwitchSection,
        TryStatement,
        CatchClause,
        CatchDeclaration,
        CatchFilterClause,
        FinallyClause,
        ParenthesizedExpression,

        // Other Expressions

        UnaryPlusExpression,
        UnaryMinusExpression,
        BitwiseNotExpression,
        LogicalNotExpression,
        PreIncrementExpression,
        PreDecrementExpression,
        PointerIndirectionExpression,
        AddressOfExpression,
        PostIncrementExpression,
        PostDecrementExpression,
        AwaitExpression,
        IndexExpression,
        CharacterLiteralToken,
        SimpleAssignmentExpression,
        AddAssignmentExpression,
        SubtractAssignmentExpression,
        MultiplyAssignmentExpression,
        DivideAssignmentExpression,
        AndAssignmentExpression,
        ExclusiveOrAssignmentExpression,
        LeftShiftAssignmentExpression,
        ModuloAssignmentExpression,
        RightShiftAssignmentExpression,
        CoalesceAssignmentExpression,
        UnsignedRightShiftAssignmentExpression,
        OrAssignmentExpression,
        ByteLiteralExpression,
        SByteLiteralExpression,
        ShortLiteralExpression,
        IntLiteralExpression,
        UIntLiteralExpression,
        ULongLiteralExpression,
        UShortLiteralExpression,
        LongLiteralExpression,
        FloatLiteralExpression,
        DoubleLiteralExpression,
        DecimalLiteralExpression,
        VoidLiteralExpression,


        // ── Literal Expressions ───────────────────────────────────────────────────────
        NumericLiteralExpression,
        StringLiteralExpression,
        CharacterLiteralExpression,
        TrueLiteralExpression,
        FalseLiteralExpression,
        NullLiteralExpression,
        DefaultLiteralExpression,

        // ── Primary Expressions ───────────────────────────────────────────────────────
        ThisExpression,
        BaseExpression,
        ArgListExpression,

        // ── Binary Expressions ────────────────────────────────────────────────────────
        AddExpression,
        SubtractExpression,
        MultiplyExpression,
        DivideExpression,
        ModuloExpression,
        LeftShiftExpression,
        RightShiftExpression,
        UnsignedRightShiftExpression,
        LogicalAndExpression,
        LogicalOrExpression,
        BitwiseAndExpression,
        BitwiseOrExpression,
        ExclusiveOrExpression,
        CoalesceExpression,

        // ── Comparison Expressions ────────────────────────────────────────────────────
        EqualsExpression,
        NotEqualsExpression,
        LessThanExpression,
        LessThanOrEqualExpression,
        GreaterThanExpression,
        GreaterThanOrEqualExpression,
        IsExpression,
        AsExpression,

        // ── Member Access Expressions ─────────────────────────────────────────────────
        SimpleMemberAccessExpression,
        PointerMemberAccessExpression,
        ConditionalAccessExpression,



        //Statements
        BlockStatement,
        ExpressionStatement,
        ForStatement,
        IfStatement,
        WhileStatement,
        DoWhileStatement,
        VariableDeclaration,
        SwitchStatement,
        GlobalStatement,
        NamespaceDeclaration,
        UsingDirective,
        ExternAliasDirective,
        FileScopedNamespaceDeclaration,
        LocalFunctionStatement,


        // ── Statements ───────────────────────────────────────────────────────────────
        UnsafeStatement,
        LockStatement,
        Block,
        LocalDeclarationStatement,
        VariableDeclarator,
        EqualsValueClause,
        EmptyStatement,
        LabeledStatement,
        GotoStatement,
        GotoCaseStatement,
        GotoDefaultStatement,
        BreakStatement,
        ContinueStatement,
        ReturnStatement,
        ThrowStatement,
        YieldReturnStatement,
        YieldBreakStatement,
        DoStatement,
        ForEachStatement,
        UsingStatement,
        FixedStatement,
        GreenNodeList,
        CommaToken,
    }
}
