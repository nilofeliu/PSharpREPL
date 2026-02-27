using PSharp.CodeAnalysis.Syntax.Kind;


namespace PSharp.CodeAnalysis.Syntax.Green
{
    internal sealed class GreenCompilationUnit : GreenNode
    {
        public GreenStatement Statement { get; }
        public GreenToken EndOfFileToken { get; }

        public override int SlotCount => 2;

        public override GreenNode? GetSlot(int index) => index switch
        {
            0 => Statement,
            1 => EndOfFileToken,
            _ => null
        };

        public GreenCompilationUnit(GreenStatement statement, GreenToken endOfFileToken)
            : base(SyntaxKind.CompilationUnit)
        {
            Statement = statement;
            EndOfFileToken = endOfFileToken;
        }

        public override SyntaxKind Kind => SyntaxKind.CompilationUnit;

        protected override GreenNode CreateWithDiagnostics(PSharp.CodeAnalysis.Diagnostics.DiagnosticInfo[]? diagnostics)
        {
            var node = new GreenCompilationUnit(Statement, EndOfFileToken);
            node.Diagnostics = diagnostics;
            return node;
        }

        public override string ToFullString()
        {
            var sb = new System.Text.StringBuilder();
            foreach (var child in GetChildren())
                sb.Append(child.ToFullString());
            return sb.ToString();
        }
    }
}
