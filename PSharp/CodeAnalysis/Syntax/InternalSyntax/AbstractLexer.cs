using PSharp.CodeAnalysis.Text;

namespace PSharp.CodeAnalysis.Syntax.InternalSyntax;

internal abstract class AbstractLexer
{
    protected readonly TextWindow TextWindow;

    protected AbstractLexer(SourceText text)
    {
        TextWindow = new TextWindow(text);
    }

    protected char PeekChar() => TextWindow.PeekChar();
    protected char PeekChar(int delta) => TextWindow.PeekChar(delta);
    protected char NextChar() => TextWindow.NextChar();
    protected void AdvanceChar() => TextWindow.AdvanceChar();
    protected void AdvanceChar(int count) => TextWindow.AdvanceChar(count);
    protected void StartLexeme() => TextWindow.StartLexeme();
    protected bool IsAtEnd => TextWindow.IsAtEnd;
    protected string GetText() => TextWindow.GetText(false);

}


