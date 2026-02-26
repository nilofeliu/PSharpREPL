namespace PSharp.CodeAnalysis.Diagnostics
{
    public static class ErrorCodeMessages
    {
        public static string GetMessage(ErrorCode code, params object[] args) => code switch
        {
            ErrorCode.ERR_InvalidNumber => $"The number '{args[0]}' isn't valid {args[1]}.",
            ErrorCode.ERR_BadCharacter => $"Bad character input: '{args[0]}'.",
            ErrorCode.ERR_UnterminatedStringLiteral => "Unterminated string literal.",
            ErrorCode.ERR_UnexpectedToken => $"Unexpected token <{args[0]}>, expected <{args[1]}>.",
            ErrorCode.ERR_UndefinedUnaryOperator => $"Unary operator '{args[0]}' is not defined for type '{args[1]}'.",
            ErrorCode.ERR_UndefinedBinaryOperator => $"Binary operator '{args[0]}' is not defined for types '{args[1]}' and '{args[2]}'.",
            ErrorCode.ERR_UndefinedName => $"Variable '{args[0]}' doesn't exist.",
            ErrorCode.ERR_CannotConvert => $"Cannot convert type '{args[0]}' to '{args[1]}'.",
            ErrorCode.ERR_VariableAlreadyDeclared => $"Variable '{args[0]}' is already declared.",
            ErrorCode.ERR_CannotAssign => $"Variable '{args[0]}' is read-only and cannot be assigned to.",
            ErrorCode.ERR_DuplicateCaseLabel => $"The case label '{args[0]}' already appears in this switch statement.",
            ErrorCode.ERR_KeywordAsIdentifier => $"'{args[0]}' is a keyword and cannot be used as an identifier.",
            ErrorCode.ERR_UnterminatedComment => "Unterminated comment region.",
            ErrorCode.ERR_NewlineInStringLiteral => "Newline not allowed in string literal.",
            ErrorCode.ERR_InvalidEscapeSequence => args.Length > 0
                ? $"Invalid escape sequence: '{args[0]}'."
                : "Invalid escape sequence.",
            _ => "Unknown error."
        };
    }


}

