using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Compilations;
using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Binding.Expressions
{
    internal sealed class BoundBinaryOperator
    {
        private BoundBinaryOperator(SyntaxKind syntaxKind, BinaryOperatorKind kind, TypeSymbol type)
         : this(syntaxKind, kind, type, type, type)
        {

        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BinaryOperatorKind kind, TypeSymbol operandType, TypeSymbol resultType)
         : this(syntaxKind, kind, operandType, operandType, resultType)
        {

        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BinaryOperatorKind kind, TypeSymbol leftType, TypeSymbol rightTye, TypeSymbol resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            LeftType = leftType;
            RightType = rightTye;
            Type = resultType;
        }

        public SyntaxKind SyntaxKind { get; }
        public BinaryOperatorKind Kind { get; }
        public TypeSymbol LeftType { get; }
        public TypeSymbol RightType { get; }
        public TypeSymbol Type { get; }

        private static readonly Dictionary<BinaryOperatorKind, BoundBinaryOperator> _operators = new()
        {
            // Byte arithmetic
            [BinaryOperatorKind.ByteAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.ByteAdd, Compilation.typeOf(SpecialType.System_Byte)),
            [BinaryOperatorKind.ByteSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.ByteSub, Compilation.typeOf(SpecialType.System_Byte)),
            [BinaryOperatorKind.ByteMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.ByteMul, Compilation.typeOf(SpecialType.System_Byte)),
            [BinaryOperatorKind.ByteDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.ByteDiv, Compilation.typeOf(SpecialType.System_Byte)),
            [BinaryOperatorKind.ByteAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.ByteAnd, Compilation.typeOf(SpecialType.System_Byte)),
            [BinaryOperatorKind.ByteOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.ByteOr, Compilation.typeOf(SpecialType.System_Byte)),
            [BinaryOperatorKind.ByteXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.ByteXor, Compilation.typeOf(SpecialType.System_Byte)),
            [BinaryOperatorKind.ByteEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.ByteEqual, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ByteNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.ByteNotEqual, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ByteLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.ByteLT, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ByteLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.ByteLTE, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ByteGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.ByteGT, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ByteGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.ByteGTE, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),

            // SByte arithmetic
            [BinaryOperatorKind.SByteAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.SByteAdd, Compilation.typeOf(SpecialType.System_SByte)),
            [BinaryOperatorKind.SByteSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.SByteSub, Compilation.typeOf(SpecialType.System_SByte)),
            [BinaryOperatorKind.SByteMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.SByteMul, Compilation.typeOf(SpecialType.System_SByte)),
            [BinaryOperatorKind.SByteDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.SByteDiv, Compilation.typeOf(SpecialType.System_SByte)),
            [BinaryOperatorKind.SByteAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.SByteAnd, Compilation.typeOf(SpecialType.System_SByte)),
            [BinaryOperatorKind.SByteOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.SByteOr, Compilation.typeOf(SpecialType.System_SByte)),
            [BinaryOperatorKind.SByteXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.SByteXor, Compilation.typeOf(SpecialType.System_SByte)),
            [BinaryOperatorKind.SByteEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.SByteEqual, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.SByteNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.SByteNotEqual, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.SByteLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.SByteLT, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.SByteLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.SByteLTE, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.SByteGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.SByteGT, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.SByteGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.SByteGTE, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),

            // Short
            [BinaryOperatorKind.ShortAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.ShortAdd, Compilation.typeOf(SpecialType.System_Int16)),
            [BinaryOperatorKind.ShortSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.ShortSub, Compilation.typeOf(SpecialType.System_Int16)),
            [BinaryOperatorKind.ShortMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.ShortMul, Compilation.typeOf(SpecialType.System_Int16)),
            [BinaryOperatorKind.ShortDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.ShortDiv, Compilation.typeOf(SpecialType.System_Int16)),
            [BinaryOperatorKind.ShortAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.ShortAnd, Compilation.typeOf(SpecialType.System_Int16)),
            [BinaryOperatorKind.ShortOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.ShortOr, Compilation.typeOf(SpecialType.System_Int16)),
            [BinaryOperatorKind.ShortXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.ShortXor, Compilation.typeOf(SpecialType.System_Int16)),
            [BinaryOperatorKind.ShortEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.ShortEqual, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ShortNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.ShortNotEqual, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ShortLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.ShortLT, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ShortLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.ShortLTE, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ShortGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.ShortGT, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ShortGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.ShortGTE, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),

            // UShort
            [BinaryOperatorKind.UShortAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.UShortAdd, Compilation.typeOf(SpecialType.System_UInt16)),
            [BinaryOperatorKind.UShortSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.UShortSub, Compilation.typeOf(SpecialType.System_UInt16)),
            [BinaryOperatorKind.UShortMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.UShortMul, Compilation.typeOf(SpecialType.System_UInt16)),
            [BinaryOperatorKind.UShortDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.UShortDiv, Compilation.typeOf(SpecialType.System_UInt16)),
            [BinaryOperatorKind.UShortAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.UShortAnd, Compilation.typeOf(SpecialType.System_UInt16)),
            [BinaryOperatorKind.UShortOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.UShortOr, Compilation.typeOf(SpecialType.System_UInt16)),
            [BinaryOperatorKind.UShortXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.UShortXor, Compilation.typeOf(SpecialType.System_UInt16)),
            [BinaryOperatorKind.UShortEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.UShortEqual, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UShortNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.UShortNotEqual, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UShortLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.UShortLT, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UShortLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.UShortLTE, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UShortGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.UShortGT, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UShortGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.UShortGTE, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),

            // Int
            [BinaryOperatorKind.IntAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.IntAdd, Compilation.typeOf(SpecialType.System_Int32)),
            [BinaryOperatorKind.IntSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.IntSub, Compilation.typeOf(SpecialType.System_Int32)),
            [BinaryOperatorKind.IntMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.IntMul, Compilation.typeOf(SpecialType.System_Int32)),
            [BinaryOperatorKind.IntDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.IntDiv, Compilation.typeOf(SpecialType.System_Int32)),
            [BinaryOperatorKind.IntAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.IntAnd, Compilation.typeOf(SpecialType.System_Int32)),
            [BinaryOperatorKind.IntOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.IntOr, Compilation.typeOf(SpecialType.System_Int32)),
            [BinaryOperatorKind.IntXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.IntXor, Compilation.typeOf(SpecialType.System_Int32)),
            [BinaryOperatorKind.IntEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.IntEqual, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.IntNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.IntNotEqual, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.IntLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.IntLT, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.IntLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.IntLTE, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.IntGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.IntGT, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.IntGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.IntGTE, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),

            // UInt
            [BinaryOperatorKind.UIntAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.UIntAdd, Compilation.typeOf(SpecialType.System_UInt32)),
            [BinaryOperatorKind.UIntSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.UIntSub, Compilation.typeOf(SpecialType.System_UInt32)),
            [BinaryOperatorKind.UIntMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.UIntMul, Compilation.typeOf(SpecialType.System_UInt32)),
            [BinaryOperatorKind.UIntDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.UIntDiv, Compilation.typeOf(SpecialType.System_UInt32)),
            [BinaryOperatorKind.UIntAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.UIntAnd, Compilation.typeOf(SpecialType.System_UInt32)),
            [BinaryOperatorKind.UIntOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.UIntOr, Compilation.typeOf(SpecialType.System_UInt32)),
            [BinaryOperatorKind.UIntXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.UIntXor, Compilation.typeOf(SpecialType.System_UInt32)),
            [BinaryOperatorKind.UIntEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.UIntEqual, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UIntNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.UIntNotEqual, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UIntLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.UIntLT, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UIntLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.UIntLTE, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UIntGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.UIntGT, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.UIntGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.UIntGTE, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),

            // Long
            [BinaryOperatorKind.LongAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.LongAdd, Compilation.typeOf(SpecialType.System_Int64)),
            [BinaryOperatorKind.LongSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.LongSub, Compilation.typeOf(SpecialType.System_Int64)),
            [BinaryOperatorKind.LongMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.LongMul, Compilation.typeOf(SpecialType.System_Int64)),
            [BinaryOperatorKind.LongDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.LongDiv, Compilation.typeOf(SpecialType.System_Int64)),
            [BinaryOperatorKind.LongAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.LongAnd, Compilation.typeOf(SpecialType.System_Int64)),
            [BinaryOperatorKind.LongOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.LongOr, Compilation.typeOf(SpecialType.System_Int64)),
            [BinaryOperatorKind.LongXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.LongXor, Compilation.typeOf(SpecialType.System_Int64)),
            [BinaryOperatorKind.LongEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.LongEqual, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.LongNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.LongNotEqual, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.LongLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.LongLT, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.LongLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.LongLTE, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.LongGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.LongGT, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.LongGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.LongGTE, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),

            // ULong
            [BinaryOperatorKind.ULongAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.ULongAdd, Compilation.typeOf(SpecialType.System_UInt64)),
            [BinaryOperatorKind.ULongSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.ULongSub, Compilation.typeOf(SpecialType.System_UInt64)),
            [BinaryOperatorKind.ULongMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.ULongMul, Compilation.typeOf(SpecialType.System_UInt64)),
            [BinaryOperatorKind.ULongDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.ULongDiv, Compilation.typeOf(SpecialType.System_UInt64)),
            [BinaryOperatorKind.ULongAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.ULongAnd, Compilation.typeOf(SpecialType.System_UInt64)),
            [BinaryOperatorKind.ULongOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.ULongOr, Compilation.typeOf(SpecialType.System_UInt64)),
            [BinaryOperatorKind.ULongXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.ULongXor, Compilation.typeOf(SpecialType.System_UInt64)),
            [BinaryOperatorKind.ULongEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.ULongEqual, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ULongNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.ULongNotEqual, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ULongLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.ULongLT, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ULongLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.ULongLTE, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ULongGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.ULongGT, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.ULongGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.ULongGTE, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),

            // Float
            [BinaryOperatorKind.FloatAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.FloatAdd, Compilation.typeOf(SpecialType.System_Single)),
            [BinaryOperatorKind.FloatSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.FloatSub, Compilation.typeOf(SpecialType.System_Single)),
            [BinaryOperatorKind.FloatMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.FloatMul, Compilation.typeOf(SpecialType.System_Single)),
            [BinaryOperatorKind.FloatDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.FloatDiv, Compilation.typeOf(SpecialType.System_Single)),
            [BinaryOperatorKind.FloatEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.FloatEqual, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.FloatNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.FloatNotEqual, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.FloatLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.FloatLT, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.FloatLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.FloatLTE, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.FloatGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.FloatGT, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.FloatGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.FloatGTE, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),

            // Double
            [BinaryOperatorKind.DoubleAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.DoubleAdd, Compilation.typeOf(SpecialType.System_Double)),
            [BinaryOperatorKind.DoubleSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.DoubleSub, Compilation.typeOf(SpecialType.System_Double)),
            [BinaryOperatorKind.DoubleMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.DoubleMul, Compilation.typeOf(SpecialType.System_Double)),
            [BinaryOperatorKind.DoubleDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.DoubleDiv, Compilation.typeOf(SpecialType.System_Double)),
            [BinaryOperatorKind.DoubleEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.DoubleEqual, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DoubleNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.DoubleNotEqual, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DoubleLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.DoubleLT, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DoubleLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.DoubleLTE, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DoubleGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.DoubleGT, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DoubleGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.DoubleGTE, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),

            // Decimal
            [BinaryOperatorKind.DecimalAdd] = new(SyntaxKind.PlusToken, BinaryOperatorKind.DecimalAdd, Compilation.typeOf(SpecialType.System_Decimal)),
            [BinaryOperatorKind.DecimalSub] = new(SyntaxKind.MinusToken, BinaryOperatorKind.DecimalSub, Compilation.typeOf(SpecialType.System_Decimal)),
            [BinaryOperatorKind.DecimalMul] = new(SyntaxKind.StarToken, BinaryOperatorKind.DecimalMul, Compilation.typeOf(SpecialType.System_Decimal)),
            [BinaryOperatorKind.DecimalDiv] = new(SyntaxKind.SlashToken, BinaryOperatorKind.DecimalDiv, Compilation.typeOf(SpecialType.System_Decimal)),
            [BinaryOperatorKind.DecimalEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.DecimalEqual, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DecimalNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.DecimalNotEqual, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DecimalLT] = new(SyntaxKind.LessToken, BinaryOperatorKind.DecimalLT, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DecimalLTE] = new(SyntaxKind.LessEqualsToken, BinaryOperatorKind.DecimalLTE, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DecimalGT] = new(SyntaxKind.GreaterToken, BinaryOperatorKind.DecimalGT, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.DecimalGTE] = new(SyntaxKind.GreaterEqualsToken, BinaryOperatorKind.DecimalGTE, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),

            // Bool
            [BinaryOperatorKind.BoolAnd] = new(SyntaxKind.AmpersandToken, BinaryOperatorKind.BoolAnd, Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.BoolOr] = new(SyntaxKind.PipeToken, BinaryOperatorKind.BoolOr, Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.BoolXor] = new(SyntaxKind.CaretToken, BinaryOperatorKind.BoolXor, Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.BoolCondAnd] = new(SyntaxKind.AmpersandAmpersandToken, BinaryOperatorKind.BoolCondAnd, Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.BoolCondOr] = new(SyntaxKind.PipePipeToken, BinaryOperatorKind.BoolCondOr, Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.BoolEqual] = new(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.BoolEqual, Compilation.typeOf(SpecialType.System_Boolean)),
            [BinaryOperatorKind.BoolNotEqual] = new(SyntaxKind.BangEqualsToken, BinaryOperatorKind.BoolNotEqual, Compilation.typeOf(SpecialType.System_Boolean)),
        };

        public static BoundBinaryOperator? Bind(SyntaxKind syntaxKind, TypeSymbol leftType, TypeSymbol rightType)
        {
            // temporary until binder handles promotion
            if (leftType == rightType)
                return Bind(syntaxKind, leftType);
            return null;
        }

        public static BoundBinaryOperator? Bind(SyntaxKind syntaxKind, TypeSymbol type)
        {
            var kind = GetOperatorKind(syntaxKind, type.SpecialType);
            return kind.HasValue && _operators.TryGetValue(kind.Value, out var op) ? op : null;
        }

        private static BinaryOperatorKind? GetOperatorKind(SyntaxKind syntax, SpecialType type)
        {
            var typeBit = type switch
            {
                SpecialType.System_Byte => BinaryOperatorKind.Byte,
                SpecialType.System_SByte => BinaryOperatorKind.SByte,
                SpecialType.System_Int16 => BinaryOperatorKind.Short,
                SpecialType.System_UInt16 => BinaryOperatorKind.UShort,
                SpecialType.System_Int32 => BinaryOperatorKind.Int,
                SpecialType.System_UInt32 => BinaryOperatorKind.UInt,
                SpecialType.System_Int64 => BinaryOperatorKind.Long,
                SpecialType.System_UInt64 => BinaryOperatorKind.ULong,
                SpecialType.System_Single => BinaryOperatorKind.Float,
                SpecialType.System_Double => BinaryOperatorKind.Double,
                SpecialType.System_Decimal => BinaryOperatorKind.Decimal,
                SpecialType.System_Boolean => BinaryOperatorKind.Bool,
                _ => (BinaryOperatorKind)0
            };

            var opBit = syntax switch
            {
                SyntaxKind.PlusToken => BinaryOperatorKind.Add,
                SyntaxKind.MinusToken => BinaryOperatorKind.Subtract,
                SyntaxKind.StarToken => BinaryOperatorKind.Multiply,
                SyntaxKind.SlashToken => BinaryOperatorKind.Divide,
                SyntaxKind.AmpersandToken => BinaryOperatorKind.And,
                SyntaxKind.PipeToken => BinaryOperatorKind.Or,
                SyntaxKind.CaretToken => BinaryOperatorKind.ExclusiveOr,
                SyntaxKind.AmpersandAmpersandToken => BinaryOperatorKind.ConditionalAnd,
                SyntaxKind.PipePipeToken => BinaryOperatorKind.ConditionalOr,
                SyntaxKind.EqualsEqualsToken => BinaryOperatorKind.Equal,
                SyntaxKind.BangEqualsToken => BinaryOperatorKind.NotEqual,
                SyntaxKind.LessToken => BinaryOperatorKind.LessThan,
                SyntaxKind.LessEqualsToken => BinaryOperatorKind.LessThanOrEqual,
                SyntaxKind.GreaterToken => BinaryOperatorKind.GreaterThan,
                SyntaxKind.GreaterEqualsToken => BinaryOperatorKind.GreaterThanOrEqual,
                _ => (BinaryOperatorKind)0
            };

            if (typeBit == 0 || opBit == 0) return null;
            return typeBit | opBit;
        }

    }
}


//using Minsk.CodeAnalysis.Binding.Kind;
//using Minsk.CodeAnalysis.Compilations;
//using Minsk.CodeAnalysis.Symbols;
//using Minsk.CodeAnalysis.Syntax.Kind;

//namespace Minsk.CodeAnalysis.Binding.Expressions
//{
//    internal sealed class BoundBinaryOperator
//    {        
//        private BoundBinaryOperator(SyntaxKind syntaxKind, BinaryOperatorKind kind, TypeSymbol type)
//         : this(syntaxKind, kind, type, type, type)
//        {

//        }

//        private BoundBinaryOperator(SyntaxKind syntaxKind, BinaryOperatorKind kind, TypeSymbol operandType, TypeSymbol resultType)
//         : this(syntaxKind, kind, operandType, operandType, resultType)
//        {

//        }

//        private BoundBinaryOperator(SyntaxKind syntaxKind, BinaryOperatorKind kind, TypeSymbol leftType, TypeSymbol rightTye, TypeSymbol resultType)
//        {
//            SyntaxKind = syntaxKind;
//            Kind = kind;
//            LeftType = leftType;
//            RightType = rightTye;
//            Type = resultType;
//        }

//        public SyntaxKind SyntaxKind { get; }
//        public BinaryOperatorKind Kind { get; }
//        public TypeSymbol LeftType { get; }
//        public TypeSymbol RightType { get; }
//        public TypeSymbol Type { get; }

//        private static BoundBinaryOperator[] _operators =
//        {
//            // Arithmetic operators - all numeric types
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_Byte)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_SByte)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_Int16)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_UInt16)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_Int32)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_UInt32)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_Int64)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_UInt64)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_Single)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_Double)),
//            new BoundBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Add, Compilation.typeOf(SpecialType.System_Decimal)),

//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_Byte)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_SByte)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_Int16)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_UInt16)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_Int32)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_UInt32)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_Int64)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_UInt64)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_Single)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_Double)),
//            new BoundBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtract, Compilation.typeOf(SpecialType.System_Decimal)),

//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_Byte)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_SByte)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_Int16)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_UInt16)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_Int32)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_UInt32)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_Int64)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_UInt64)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_Single)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_Double)),
//            new BoundBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiply, Compilation.typeOf(SpecialType.System_Decimal)),

//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_Byte)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_SByte)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_Int16)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_UInt16)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_Int32)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_UInt32)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_Int64)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_UInt64)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_Single)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_Double)),
//            new BoundBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Divide, Compilation.typeOf(SpecialType.System_Decimal)),

//            // Bitwise operators - integer types only
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_Byte)),
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_SByte)),
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_Int16)),
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_UInt16)),
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_Int32)),
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_UInt32)),
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_Int64)),
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_UInt64)),

//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_Byte)),
//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_SByte)),
//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_Int16)),
//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_UInt16)),
//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_Int32)),
//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_UInt32)),
//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_Int64)),
//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_UInt64)),

//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_Byte)),
//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_SByte)),
//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_Int16)),
//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_UInt16)),
//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_Int32)),
//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_UInt32)),
//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_Int64)),
//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_UInt64)),

//            // Comparison operators - all numeric types, return bool
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),

//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),

//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.LessThan, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),

//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessThanOrEquals, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),

//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.GreaterThan, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),

//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_Byte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_SByte), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_Int16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_UInt16), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_Int32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_UInt32), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_Int64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_UInt64), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_Single), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_Double), Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterThanOrEquals, Compilation.typeOf(SpecialType.System_Decimal), Compilation.typeOf(SpecialType.System_Boolean)),

//            // Bitwise operators for bool (already had these)
//            new BoundBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.And, Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.Or, Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.ExclusiveOr, Compilation.typeOf(SpecialType.System_Boolean)),

//            // Logical operators for bool (if you have them)


//            new BoundBinaryOperator(SyntaxKind.AmpersandAmpersandToken, BinaryOperatorKind.ConditionalAnd, Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.PipePipeToken, BinaryOperatorKind.ConditionalOr, Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, Compilation.typeOf(SpecialType.System_Boolean)),
//            new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, Compilation.typeOf(SpecialType.System_Boolean)),
//        };

//        public static BoundBinaryOperator Bind(SyntaxKind syntaxKind, TypeSymbol leftType, TypeSymbol rightType)
//        {
//            foreach (var op in _operators)
//            {
//                if (op.SyntaxKind == syntaxKind && op.LeftType == leftType && op.RightType == rightType)
//                    return op;
//            }

//            return null;
//        }
//    }
//}



