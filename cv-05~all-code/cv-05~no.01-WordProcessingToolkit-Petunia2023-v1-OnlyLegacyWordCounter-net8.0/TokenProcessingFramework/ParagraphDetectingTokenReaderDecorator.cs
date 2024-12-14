using System;

namespace TokenProcessingFramework;

#nullable enable

// Now the code passes all my tests

public record class ParagraphDetectingTokenReaderDecorator(ITokenReader Reader) : ITokenReader 
{
	private Token? _nextToken = null;
    private bool _hasStartedReading = false;

    public Token ReadToken()
    {
        Token token;

        if (_nextToken is not null)
        {
            token = _nextToken.Value;
            _nextToken = null;
            return token;
        }

        if (!_hasStartedReading)
        {
            _hasStartedReading = true;
            do
            {
                token = Reader.ReadToken();
            } while (token.Type == TokenType.EndOfLine);

            return token;
        }

        int newLinesFound = 0;

        while ((token = Reader.ReadToken()).Type == TokenType.EndOfLine)
        {
            newLinesFound++;
        }

        if (token.Type == TokenType.EndOfInput && newLinesFound > 0)
        {
            return token;
        }

        if (newLinesFound > 1)
        {
            _nextToken = token;
            return new Token(TokenType.EndOfParagraph);
        }

        return token;
    }
}