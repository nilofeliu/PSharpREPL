//using Minsk.Core.Types;
//using Minsk.Core.Types.Metadata;
//using global::Minsk.CodeAnalysis.Syntax.Kind;
//using System.Collections.Immutable;
//using Minsk.CodeAnalysis.Binding.Kind;

//namespace Minsk.CodeAnalysis.Binding.Semantics.Operators;

//internal sealed class BinaryOperatorOverloadResolution
//{
//    private readonly ImmutableArray<BuiltInBinaryOperator> _builtInOperators;

//    public BinaryOperatorOverloadResolution()
//    {
//        _builtInOperators = GetBuiltInOperators();
//    }

//    private ImmutableArray<BuiltInBinaryOperator> GetBuiltInOperators()
//    {
//        var builder = ImmutableArray.CreateBuilder<BuiltInBinaryOperator>();

//        // Arithmetic operators for all numeric types
//        foreach (var type in GetNumericTypes())
//        {
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.PlusToken, BinaryOperatorKind.Addition, type, type, type));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.MinusToken, BinaryOperatorKind.Subtraction, type, type, type));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.StarToken, BinaryOperatorKind.Multiplication, type, type, type));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.SlashToken, BinaryOperatorKind.Division, type, type, type));
//        }

//        // Bitwise operators for integer types
//        foreach (var type in GetIntegerTypes())
//        {
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.BitwiseAnd, type, type, type));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.BitwiseOr, type, type, type));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.BitwiseXor, type, type, type));
//        }

//        // Bitwise operators for bool
//        var boolType = GetBoolType();
//        builder.Add(new BuiltInBinaryOperator(SyntaxKind.AmpersandToken, BinaryOperatorKind.BitwiseAnd, boolType, boolType, boolType));
//        builder.Add(new BuiltInBinaryOperator(SyntaxKind.PipeToken, BinaryOperatorKind.BitwiseOr, boolType, boolType, boolType));
//        builder.Add(new BuiltInBinaryOperator(SyntaxKind.HatToken, BinaryOperatorKind.BitwiseXor, boolType, boolType, boolType));

//        // Logical operators for bool
//        builder.Add(new BuiltInBinaryOperator(SyntaxKind.AmpersandAmpersandToken, BinaryOperatorKind.LogicalAnd, boolType, boolType, boolType));
//        builder.Add(new BuiltInBinaryOperator(SyntaxKind.PipePipeToken, BinaryOperatorKind.LogicalOr, boolType, boolType, boolType));

//        // Comparison operators for all numeric types
//        foreach (var type in GetNumericTypes())
//        {
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.EqualsEqualsToken, BinaryOperatorKind.Equals, type, type, boolType));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.BangEqualsToken, BinaryOperatorKind.NotEquals, type, type, boolType));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.LessToken, BinaryOperatorKind.Less, type, type, boolType));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.LessOrEqualsToken, BinaryOperatorKind.LessOrEquals, type, type, boolType));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.GreaterToken, BinaryOperatorKind.Greater, type, type, boolType));
//            builder.Add(new BuiltInBinaryOperator(SyntaxKind.GreaterOrEqualsToken, BinaryOperatorKind.GreaterOrEquals, type, type, boolType));
//        }

//        return builder.ToImmutable();
//    }

//    private ImmutableArray<TypeData> GetNumericTypes()
//    {
//        return ImmutableArray.Create(
//            Types.typeOf(SpecialType.Byte),
//            Types.typeOf(SpecialType.SByte),
//            Types.typeOf(SpecialType.Short),
//            Types.typeOf(SpecialType.UShort),
//            Types.typeOf(SpecialType.Int),
//            Types.typeOf(SpecialType.UInt),
//            Types.typeOf(SpecialType.Long),
//            Types.typeOf(SpecialType.ULong),
//            Types.typeOf(SpecialType.Float),
//            Types.typeOf(SpecialType.Double),
//            Types.typeOf(SpecialType.Decimal)
//        );
//    }

//    private ImmutableArray<TypeData> GetIntegerTypes()
//    {
//        return ImmutableArray.Create(
//            Types.typeOf(SpecialType.Byte),
//            Types.typeOf(SpecialType.SByte),
//            Types.typeOf(SpecialType.Short),
//            Types.typeOf(SpecialType.UShort),
//            Types.typeOf(SpecialType.Int),
//            Types.typeOf(SpecialType.UInt),
//            Types.typeOf(SpecialType.Long),
//            Types.typeOf(SpecialType.ULong)
//        );
//    }

//    private TypeData GetBoolType()
//    {
//        return Types.typeOf(SpecialType.Bool);
//    }

//    public BinaryOperatorResolutionResult Resolve(
//        SyntaxKind syntaxKind,
//        BinaryOperatorKind operatorKind,
//        TypeData leftType,
//        TypeData rightType)
//    {
//        var candidates = ImmutableArray.CreateBuilder<ApplicableOperator>();

//        // Get applicable built-in operators
//        foreach (var op in _builtInOperators)
//        {
//            if (op.SyntaxKind != syntaxKind || op.OperatorKind != operatorKind)
//                continue;

//            // Check conversions from operand types to operator parameter types
//            var leftConversion = Conversions.ClassifyConversion(leftType, op.LeftType);
//            var rightConversion = Conversions.ClassifyConversion(rightType, op.RightType);

//            if (leftConversion.Exists && rightConversion.Exists)
//            {
//                candidates.Add(new ApplicableOperator(
//                    op,
//                    leftConversion,
//                    rightConversion,
//                    op.ResultType));
//            }
//        }

//        // Check for user-defined operators (if you have them)
//        // var userDefinedCandidates = GetUserDefinedOperators(leftType, rightType, syntaxKind);
//        // candidates.AddRange(userDefinedCandidates);

//        if (candidates.Count == 0)
//            return BinaryOperatorResolutionResult.NotFound;

//        if (candidates.Count == 1)
//            return BinaryOperatorResolutionResult.Success(candidates[0]);

//        // Find the best candidate
//        var best = FindBestCandidate(candidates.ToImmutable());
//        if (best == null)
//            return BinaryOperatorResolutionResult.Ambiguous;

//        return BinaryOperatorResolutionResult.Success(best);
//    }

//    private ApplicableOperator FindBestCandidate(ImmutableArray<ApplicableOperator> candidates)
//    {
//        // Implementation of overload resolution rules
//        // This is where you'd implement "betterness" rules:
//        // - Exact match better than conversion
//        // - Non-lifted better than lifted
//        // - More specific type better than less specific

//        ApplicableOperator best = null;
//        foreach (var candidate in candidates)
//        {
//            if (best == null)
//            {
//                best = candidate;
//                continue;
//            }

//            if (IsBetterThan(candidate, best))
//                best = candidate;
//            else if (!IsBetterThan(best, candidate))
//                return null; // ambiguous
//        }

//        return best;
//    }

//    private bool IsBetterThan(ApplicableOperator x, ApplicableOperator y)
//    {
//        // Check if x is better than y based on conversion quality
//        var leftBetter = IsBetterConversion(x.LeftConversion, y.LeftConversion);
//        var rightBetter = IsBetterConversion(x.RightConversion, y.RightConversion);

//        return leftBetter && rightBetter;
//    }

//    private bool IsBetterConversion(Conversion x, Conversion y)
//    {
//        if (x.IsIdentity && !y.IsIdentity)
//            return true;
//        if (!x.IsIdentity && y.IsIdentity)
//            return false;
//        if (x.IsImplicit && y.IsExplicit)
//            return true;
//        if (x.IsExplicit && y.IsImplicit)
//            return false;

//        // Add more rules for numeric promotion hierarchy
//        return false;
//    }

//    private class BuiltInBinaryOperator
//    {
//        public SyntaxKind SyntaxKind { get; }
//        public BinaryOperatorKind OperatorKind { get; }
//        public TypeData LeftType { get; }
//        public TypeData RightType { get; }
//        public TypeData ResultType { get; }

//        public BuiltInBinaryOperator(
//            SyntaxKind syntaxKind,
//            BinaryOperatorKind operatorKind,
//            TypeData leftType,
//            TypeData rightType,
//            TypeData resultType)
//        {
//            SyntaxKind = syntaxKind;
//            OperatorKind = operatorKind;
//            LeftType = leftType;
//            RightType = rightType;
//            ResultType = resultType;
//        }
//    }

//    public class ApplicableOperator
//    {
//        public BuiltInBinaryOperator Operator { get; }
//        public Conversion LeftConversion { get; }
//        public Conversion RightConversion { get; }
//        public TypeData ResultType { get; }

//        public ApplicableOperator(
//            BuiltInBinaryOperator op,
//            Conversion leftConversion,
//            Conversion rightConversion,
//            TypeData resultType)
//        {
//            Operator = op;
//            LeftConversion = leftConversion;
//            RightConversion = rightConversion;
//            ResultType = resultType;
//        }
//    }
//}
