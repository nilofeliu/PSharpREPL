using REPL.ui;
using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;

namespace REPL.ui
{
    internal class InputStream
    {
        private readonly StringReader _reader;
        private readonly StringBuilder _buffer = new StringBuilder();

        private readonly string _text;

        // Constructor takes the raw string input
        public InputStream(string input = "")
        {
            _text = input;
            _reader = new StringReader(_text ?? string.Empty);            
        }

        public string ReadLine()
        {
            return _text;
        }

        public string ReadUntil(char delimiter)
        {
            _buffer.Clear();
            int ch;
            while ((ch = _reader.Read()) != -1 && (char)ch != delimiter)
            {
                _buffer.Append((char)ch);
            }
            return _buffer.ToString();
        }

        public char PeekChar(int index)
        {
           string _text = _reader.ReadToEnd();
            if (index >= _text.Length)
                return '\0';
            return _text[index];
        }

        public char ReadChar()
        {
            int code = _reader.Read();
            return code == -1 ? '\0' : (char)code;
        }

        public char[] ReadCharByIndex(int startIndex, int endIndex)
        {
            char[] chars = _text.ToCharArray();

            if (startIndex < 0 || endIndex > chars.Length || startIndex > endIndex)
                throw new ArgumentOutOfRangeException();
            int length = endIndex - startIndex + 1;
            char[] result = new char[length];

            return result;
        }
        
        public string ReadCommand()
        {           
            return _text.Substring(1);
        }

        public char[] ReadAllChar()
        {
            var output = ReadLine();
            return output.ToCharArray();
        }

        public char[] ReadCharUntil(char delimiter)
        {
            var output = ReadUntil(delimiter);
            return output.ToCharArray();
        }

        public bool HasInput()
        {
            return _reader.Peek() >= 0;
        }

        public bool EndOfInput => _reader.Peek() == -1;
    }
}
