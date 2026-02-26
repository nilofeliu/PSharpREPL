using PSharp.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSharp.CodeAnalysis.Syntax.InternalSyntax;


internal sealed class TextWindow
{
    private readonly SourceText _text;
    private int _basis;
    private int _offset;
    private int _lexemeStart;

    internal TextWindow(SourceText text)
    {
        _text = text;
        _basis = 0;
        _offset = 0;
    }

    public TextWindow()
    {
    }

    internal int Position => _basis + _offset;
    internal int LexemeStartPosition => _lexemeStart;
    internal int Width => Math.Max(0, Position - _lexemeStart);

    internal char PeekChar()
    {
        var position = Position;
        return position < _text.Length ? _text[position] : '\0';
    }

    internal char PeekChar(int delta)
    {
        var position = Position + delta;
        return position < _text.Length ? _text[position] : '\0';
    }

    internal char NextChar()
    {
        var c = PeekChar();
        AdvanceChar();
        return c;
    }

    internal void AdvanceChar()
    {
        if (_offset < _text.Length)
            _offset++;
    }
    //internal void AdvanceChar()
    //{
    //    _offset++;
    //}

    internal void AdvanceChar(int count)
    {
        _offset = Math.Min(_offset + count, _text.Length);
    }

    internal void StartLexeme()
    {
        _lexemeStart = Position;
    }

    internal string GetText(bool intern)
    {
        var width = Width;
        if (width == 0)
            return string.Empty;
        return _text.ToString(_lexemeStart, width);
    }

    internal string GetText(int position, int length)
    {
        return _text.ToString(position, length);
    }

    internal bool IsAtEnd => Position >= _text.Length;
}


