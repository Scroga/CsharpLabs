using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;

namespace TextJustification;

public interface ITokenReader
{
    Token GetNextToken();
}

public class ByLineTextReader : ITokenReader
{
    TextReader _reader;
    bool _endOfParagraph;
    string[] _currentWords;
    int _wordIndex;

    public ByLineTextReader(TextReader textReader)
    {
        this._reader = textReader;
        _currentWords = Array.Empty<string>();
        _wordIndex = 0;
    }

    public Token GetNextToken()
    {
        while (_wordIndex >= _currentWords.Length)
        {
            string? line = _reader.ReadLine();
            if (line == null)
            {
                return new Token(TokenType.EndOfFile, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                if (_endOfParagraph)
                {
                    continue;
                }
                else
                {
                    _endOfParagraph = true;
                    return new Token(TokenType.EndOfParagraph, string.Empty);
                }
            }
            else
            {
                _endOfParagraph = false;
                _currentWords = line.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                _wordIndex = 0;
            }
        }

        return new Token(TokenType.EndOfWord, _currentWords[_wordIndex++]);
    }
}


public class ByCharTextReader : ITokenReader
{
    readonly TextReader     reader;
    readonly StringBuilder  currentWord;
    bool                    endOfParagraph;
    bool                    endOfFile;
    bool                    previousWasNewline;

    public ByCharTextReader(TextReader textReader)
    {
        reader = textReader;
        currentWord = new StringBuilder();
        endOfParagraph = false;
        endOfFile = false;
        previousWasNewline = false;
    }

    public Token GetNextToken()
    {
        if (endOfFile)
        {
            return new Token(TokenType.EndOfFile, string.Empty);
        }

        while (true)
        {
            int charValue = reader.Read();
            if (charValue == -1)
            {
                if (currentWord.Length > 0)
                {
                    var word = currentWord.ToString();
                    currentWord.Clear();
                    return new Token(TokenType.EndOfWord, word);
                }

                endOfFile = true;
                return new Token(TokenType.EndOfFile, string.Empty);
            }

            char currentChar = (char)charValue;

            if (char.IsWhiteSpace(currentChar))
            {
                if (currentChar == '\n')
                {
                    if (previousWasNewline)
                    {
                        endOfParagraph = true;
                        previousWasNewline = false;
                        return new Token(TokenType.EndOfParagraph, string.Empty);
                    }

                    if (currentWord.Length > 0)
                    {
                        var word = currentWord.ToString();
                        currentWord.Clear();
                        previousWasNewline = true;
                        return new Token(TokenType.EndOfWord, word);
                    }

                    previousWasNewline = true;
                    continue;
                }

                if (currentWord.Length > 0)
                {
                    var word = currentWord.ToString();
                    currentWord.Clear();
                    return new Token(TokenType.EndOfWord, word);
                }
            }
            else
            {
                previousWasNewline = false;
                currentWord.Append(currentChar);
            }
        }
    }
}
