using PSharp.CodeAnalysis.Symbols;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Text;
using System.Collections;

namespace PSharp.CodeAnalysis.Diagnostics
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new();

        public bool HasErrors => _diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error);

        private void Report(TextSpan span, ErrorCode code, params object[] args)
        {
            var message = ErrorCodeMessages.GetMessage(code, args);
            var diagnostic = new Diagnostic(span, message, code);
            _diagnostics.Add(diagnostic);
        }

        IEnumerator<Diagnostic> IEnumerable<Diagnostic>.GetEnumerator()
            => ((IEnumerable<Diagnostic>)_diagnostics).GetEnumerator();

        public IEnumerator GetEnumerator()
            => ((IEnumerable)_diagnostics).GetEnumerator();

        public void Add(Diagnostic diagnostic)
            => _diagnostics.Add(diagnostic);

        public void AddRange(DiagnosticBag diagnostics)
            => _diagnostics.AddRange(diagnostics._diagnostics);

        public void ReportInvalidNumber(TextSpan span, string text, TypeSymbol type)
            => Report(span, ErrorCode.ERR_InvalidNumber, text, type);

        public void ReportBadCharacter(int position, char character)
            => Report(new TextSpan(position, 1), ErrorCode.ERR_BadCharacter, character);

        public void ReportUnterminatedString(TextSpan span)
            => Report(span, ErrorCode.ERR_UnterminatedStringLiteral);

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
            => Report(span, ErrorCode.ERR_UnexpectedToken, actualKind, expectedKind);

        public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, TypeSymbol operandType)
            => Report(span, ErrorCode.ERR_UndefinedUnaryOperator, operatorText, operandType);

        public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, TypeSymbol leftType, TypeSymbol rightType)
            => Report(span, ErrorCode.ERR_UndefinedBinaryOperator, operatorText, leftType, rightType);

        public void ReportUndefinedName(TextSpan span, string name)
            => Report(span, ErrorCode.ERR_UndefinedName, name);

        public void ReportCannotConvert(TextSpan span, TypeSymbol fromType, TypeSymbol toType)
            => Report(span, ErrorCode.ERR_CannotConvert, fromType, toType);

        public void ReportVariableAlreadyDeclared(TextSpan span, string name)
            => Report(span, ErrorCode.ERR_VariableAlreadyDeclared, name);

        public void ReportCannotAssign(TextSpan span, string name)
            => Report(span, ErrorCode.ERR_CannotAssign, name);

        public void ReportDuplicateCaseLabel(TextSpan span, object value)
            => Report(span, ErrorCode.ERR_DuplicateCaseLabel, value);

        public void ReportKeywordAsIdentifier(TextSpan span, string keyword)
            => Report(span, ErrorCode.ERR_KeywordAsIdentifier, keyword);
    }
}