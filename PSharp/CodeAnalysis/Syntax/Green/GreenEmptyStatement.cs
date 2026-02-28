using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green;

internal sealed class GreenEmptyStatement : GreenStatement
{
    public GreenToken Token { get; }

    public GreenEmptyStatement(GreenToken endOfFileToken)
        : base(SyntaxKind.EmptyStatement)
    {
        Token = endOfFileToken;
    }

    public override int SlotCount => 1;

    public override GreenNode? GetSlot(int index)
        => index == 0 ? Token : null;

    protected override GreenNode CreateWithDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        throw new NotImplementedException();
    }

    public override string ToFullString()
    {
        throw new NotImplementedException();
    }
}
