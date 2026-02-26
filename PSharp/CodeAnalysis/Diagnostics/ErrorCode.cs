namespace PSharp.CodeAnalysis.Diagnostics
{
    public enum ErrorCode
    {
        // Lexer
        ERR_InvalidNumber,
        ERR_BadCharacter,
        ERR_UnterminatedStringLiteral,
        ERR_NewlineInStringLiteral,
        ERR_InvalidEscapeSequence,


        // Parser
        ERR_UnexpectedToken,

        // Binding
        ERR_UndefinedUnaryOperator,
        ERR_UndefinedBinaryOperator,
        ERR_UndefinedName,
        ERR_CannotConvert,
        ERR_VariableAlreadyDeclared,
        ERR_CannotAssign,
        ERR_DuplicateCaseLabel,
        ERR_KeywordAsIdentifier,
        ERR_UnterminatedComment,
    }
}
