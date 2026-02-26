namespace PSharp.CodeAnalysis.Binding.Kind
{
    internal enum BinaryOperatorKind
    {
        // Type bits
        Byte = 0x0100,
        SByte = 0x0200,
        Short = 0x0300,
        UShort = 0x0400,
        Int = 0x0500,
        UInt = 0x0600,
        Long = 0x0700,
        ULong = 0x0800,
        Float = 0x0900,
        Double = 0x0A00,
        Decimal = 0x0B00,
        Bool = 0x0C00,

        // Operator bits
        Add = 0x01,
        Subtract = 0x02,
        Multiply = 0x03,
        Divide = 0x04,
        And = 0x05,
        Or = 0x06,
        ExclusiveOr = 0x07,
        LeftShift = 0x08,
        RightShift = 0x09,
        Equal = 0x0A,
        NotEqual = 0x0B,
        LessThan = 0x0C,
        LessThanOrEqual = 0x0D,
        GreaterThan = 0x0E,
        GreaterThanOrEqual = 0x0F,
        ConditionalAnd = 0x10,
        ConditionalOr = 0x11,

        // Byte
        ByteAdd = Byte | Add, ByteSub = Byte | Subtract, ByteMul = Byte | Multiply, ByteDiv = Byte | Divide,
        ByteAnd = Byte | And, ByteOr = Byte | Or, ByteXor = Byte | ExclusiveOr,
        ByteEqual = Byte | Equal, ByteNotEqual = Byte | NotEqual,
        ByteLT = Byte | LessThan, ByteLTE = Byte | LessThanOrEqual,
        ByteGT = Byte | GreaterThan, ByteGTE = Byte | GreaterThanOrEqual,

        // SByte
        SByteAdd = SByte | Add, SByteSub = SByte | Subtract, SByteMul = SByte | Multiply, SByteDiv = SByte | Divide,
        SByteAnd = SByte | And, SByteOr = SByte | Or, SByteXor = SByte | ExclusiveOr,
        SByteEqual = SByte | Equal, SByteNotEqual = SByte | NotEqual,
        SByteLT = SByte | LessThan, SByteLTE = SByte | LessThanOrEqual,
        SByteGT = SByte | GreaterThan, SByteGTE = SByte | GreaterThanOrEqual,

        // Short
        ShortAdd = Short | Add, ShortSub = Short | Subtract, ShortMul = Short | Multiply, ShortDiv = Short | Divide,
        ShortAnd = Short | And, ShortOr = Short | Or, ShortXor = Short | ExclusiveOr,
        ShortEqual = Short | Equal, ShortNotEqual = Short | NotEqual,
        ShortLT = Short | LessThan, ShortLTE = Short | LessThanOrEqual,
        ShortGT = Short | GreaterThan, ShortGTE = Short | GreaterThanOrEqual,

        // UShort
        UShortAdd = UShort | Add, UShortSub = UShort | Subtract, UShortMul = UShort | Multiply, UShortDiv = UShort | Divide,
        UShortAnd = UShort | And, UShortOr = UShort | Or, UShortXor = UShort | ExclusiveOr,
        UShortEqual = UShort | Equal, UShortNotEqual = UShort | NotEqual,
        UShortLT = UShort | LessThan, UShortLTE = UShort | LessThanOrEqual,
        UShortGT = UShort | GreaterThan, UShortGTE = UShort | GreaterThanOrEqual,

        // Int
        IntAdd = Int | Add, IntSub = Int | Subtract, IntMul = Int | Multiply, IntDiv = Int | Divide,
        IntAnd = Int | And, IntOr = Int | Or, IntXor = Int | ExclusiveOr,
        IntEqual = Int | Equal, IntNotEqual = Int | NotEqual,
        IntLT = Int | LessThan, IntLTE = Int | LessThanOrEqual,
        IntGT = Int | GreaterThan, IntGTE = Int | GreaterThanOrEqual,

        // UInt
        UIntAdd = UInt | Add, UIntSub = UInt | Subtract, UIntMul = UInt | Multiply, UIntDiv = UInt | Divide,
        UIntAnd = UInt | And, UIntOr = UInt | Or, UIntXor = UInt | ExclusiveOr,
        UIntEqual = UInt | Equal, UIntNotEqual = UInt | NotEqual,
        UIntLT = UInt | LessThan, UIntLTE = UInt | LessThanOrEqual,
        UIntGT = UInt | GreaterThan, UIntGTE = UInt | GreaterThanOrEqual,

        // Long
        LongAdd = Long | Add, LongSub = Long | Subtract, LongMul = Long | Multiply, LongDiv = Long | Divide,
        LongAnd = Long | And, LongOr = Long | Or, LongXor = Long | ExclusiveOr,
        LongEqual = Long | Equal, LongNotEqual = Long | NotEqual,
        LongLT = Long | LessThan, LongLTE = Long | LessThanOrEqual,
        LongGT = Long | GreaterThan, LongGTE = Long | GreaterThanOrEqual,

        // ULong
        ULongAdd = ULong | Add, ULongSub = ULong | Subtract, ULongMul = ULong | Multiply, ULongDiv = ULong | Divide,
        ULongAnd = ULong | And, ULongOr = ULong | Or, ULongXor = ULong | ExclusiveOr,
        ULongEqual = ULong | Equal, ULongNotEqual = ULong | NotEqual,
        ULongLT = ULong | LessThan, ULongLTE = ULong | LessThanOrEqual,
        ULongGT = ULong | GreaterThan, ULongGTE = ULong | GreaterThanOrEqual,

        // Float
        FloatAdd = Float | Add, FloatSub = Float | Subtract, FloatMul = Float | Multiply, FloatDiv = Float | Divide,
        FloatEqual = Float | Equal, FloatNotEqual = Float | NotEqual,
        FloatLT = Float | LessThan, FloatLTE = Float | LessThanOrEqual,
        FloatGT = Float | GreaterThan, FloatGTE = Float | GreaterThanOrEqual,

        // Double
        DoubleAdd = Double | Add, DoubleSub = Double | Subtract, DoubleMul = Double | Multiply, DoubleDiv = Double | Divide,
        DoubleEqual = Double | Equal, DoubleNotEqual = Double | NotEqual,
        DoubleLT = Double | LessThan, DoubleLTE = Double | LessThanOrEqual,
        DoubleGT = Double | GreaterThan, DoubleGTE = Double | GreaterThanOrEqual,

        // Decimal
        DecimalAdd = Decimal | Add, DecimalSub = Decimal | Subtract, DecimalMul = Decimal | Multiply, DecimalDiv = Decimal | Divide,
        DecimalEqual = Decimal | Equal, DecimalNotEqual = Decimal | NotEqual,
        DecimalLT = Decimal | LessThan, DecimalLTE = Decimal | LessThanOrEqual,
        DecimalGT = Decimal | GreaterThan, DecimalGTE = Decimal | GreaterThanOrEqual,

        // Bool
        BoolAnd = Bool | And, BoolOr = Bool | Or, BoolXor = Bool | ExclusiveOr,
        BoolCondAnd = Bool | ConditionalAnd, BoolCondOr = Bool | ConditionalOr,
        BoolEqual = Bool | Equal, BoolNotEqual = Bool | NotEqual,
    }



    //internal enum BinaryOperatorKind
    //{
    //    Add,
    //    Subtract,
    //    Multiply,
    //    Divide,

    //    And,
    //    Or,

    //    ExclusiveOr,
    //    LeftShift,
    //    RightShift,
    //    Equals,
    //    NotEquals,

    //    LessThan,
    //    LessThanOrEquals,
    //    GreaterThan,
    //    GreaterThanOrEquals,

    //    ConditionalAnd,
    //    ConditionalOr,
    //}
}

