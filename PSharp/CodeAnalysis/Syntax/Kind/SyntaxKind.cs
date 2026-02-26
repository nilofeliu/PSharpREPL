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

        // Math Operators Tokens
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,

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
        AmpersandToken,
        AmpersandAmpersandToken,
        BangToken,
        BangEqualsToken,
        EqualsToken,
        EqualsEqualsToken,
        LessToken,
        LessOrEqualsToken,
        GreaterToken,
        GreaterOrEqualsToken,
        HatToken,
        PipeToken,
        PipePipeToken,
        TildeToken,

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
        IntKeyword,
        UIntKeyword,
        LongKeyword,
        ULongKeyword,
        FloatKeyword,
        DoubleKeyword,
        DecimalKeyword,
        StringKeyword,
        VoidKeyword,


        // Variable Type Tokens
        //HexLongLiteralToken,
        //HexIntegerLiteralToken,
        FloatLiteralToken,
        DoubleLiteralToken,
        DecimalLiteralToken,
        LongLiteralToken,
        ULongLiteralToken,
        UIntLiteralToken,
        IntegerLiteralToken,
        BinaryIntegerLiteralToken,

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
        CharLiteralToken,
        ParenthesizedExpression,
        SimpleAssignmentExpression,
        AddAssignmentExpression,
        SubtractAssignmentExpression,
        MultiplyAssignmentExpression,
        DivideAssignmentExpression,
        ModuloAssignmentExpression,
        AndAssignmentExpression,
        OrAssignmentExpression,
        ExclusiveOrAssignmentExpression,
        LeftShiftAssignmentExpression,
        RightShiftAssignmentExpression,
        CoalesceAssignmentExpression,
    }
}
