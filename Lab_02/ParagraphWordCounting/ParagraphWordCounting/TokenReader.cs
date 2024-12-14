using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagraphWordCounting;

public enum TokenType
{
    EndOfWord,
    EndOfLIne,
    EndOfParagraph,
    EndOfFile
}

public struct Token
{
    public TokenType Type { get; }
    public string Word { get; }

    public Token(TokenType type, string word)
    {
        Type = type;
        Word = word;
    }
}

public interface ITokenReader
{
    Token GetNextToken();
}

public class ByLineTextReader : ITokenReader
{
    private TextReader _reader;
    private bool        _endOfParagraph;
    private string[]    _currentWords;
    private int         _wordIndex;

    public ByLineTextReader(TextReader reader)
    {
        _reader = reader;
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
                _currentWords = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                _wordIndex = 0;
            }
        }

        return new Token(TokenType.EndOfWord, _currentWords[_wordIndex++]);
    }
}


