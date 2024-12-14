using System;
using System.IO;
using System.Text;

namespace MultipleJustification.Legacy;

public class LegacyByCharTextReader : ITokenReader
{
    readonly TextReader reader;
    readonly StringBuilder currentWord;
    bool endOfFile;
    bool previousWasNewline;

    public LegacyByCharTextReader(TextReader textReader)
    {
        reader = textReader;
        currentWord = new StringBuilder();
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
