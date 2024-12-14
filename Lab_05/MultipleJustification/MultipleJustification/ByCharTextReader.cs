using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;

namespace MultipleJustification;

public class ByCharTextReader : ITokenReader
{
    readonly TextReader     _reader;
    readonly StringBuilder  _currentWord = new StringBuilder();
    bool                    _isEndOfFile = false;
    bool                    _wasPreviousNewLine = false;

    public ByCharTextReader(TextReader reader) 
    {
        _reader = reader;
    }

    public Token GetNextToken()
    {
        if (_isEndOfFile)
        {
            return new Token(TokenType.EndOfFile, string.Empty);
        }

        while (true)
        {
            int charValue = _reader.Read();
            if (charValue == -1)
            {
                if (_currentWord.Length > 0)
                {
                    var word = _currentWord.ToString();
                    _currentWord.Clear();
                    return new Token(TokenType.EndOfWord, word);
                }

                _isEndOfFile = true;
                return new Token(TokenType.EndOfFile, string.Empty);
            }

            char currentChar = (char)charValue;

            if (char.IsWhiteSpace(currentChar))
            {
                if (currentChar == '\n')
                {
                    if (_wasPreviousNewLine)
                    {
                        _wasPreviousNewLine = false;
                        return new Token(TokenType.EndOfParagraph, string.Empty);
                    }

                    if (_currentWord.Length > 0)
                    {
                        var word = _currentWord.ToString();
                        _currentWord.Clear();
                        _wasPreviousNewLine = true;
                        return new Token(TokenType.EndOfWord, word);
                    }

                    _wasPreviousNewLine = true;
                    continue;
                }

                if (_currentWord.Length > 0)
                {
                    var word = _currentWord.ToString();
                    _currentWord.Clear();
                    return new Token(TokenType.EndOfWord, word);
                }
            }
            else
            {
                _wasPreviousNewLine = false;
                _currentWord.Append(currentChar);
            }
        }
    }
}