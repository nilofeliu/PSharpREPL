//namespace Minsk.CodeAnalysis.Binding.Semantics.Operators;

//public class BinaryOperatorResolutionResult
//{
//    public bool IsSuccess { get; }
//    public bool IsNotFound { get; }
//    public bool IsAmbiguous { get; }
//    public BinaryOperatorOverloadResolution.ApplicableOperator Operator { get; }

//    private BinaryOperatorResolutionResult(
//        bool isSuccess,
//        bool isNotFound,
//        bool isAmbiguous,
//        BinaryOperatorOverloadResolution.ApplicableOperator op)
//    {
//        IsSuccess = isSuccess;
//        IsNotFound = isNotFound;
//        IsAmbiguous = isAmbiguous;
//        Operator = op;
//    }

//    public static BinaryOperatorResolutionResult Success(BinaryOperatorOverloadResolution.ApplicableOperator op) =>
//        new BinaryOperatorResolutionResult(true, false, false, op);

//    public static BinaryOperatorResolutionResult NotFound =>
//        new BinaryOperatorResolutionResult(false, true, false, null);

//    public static BinaryOperatorResolutionResult Ambiguous =>
//        new BinaryOperatorResolutionResult(false, false, true, null);
//}
