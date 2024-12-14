namespace TextJustification;

#nullable enable

public enum TokenType { EndOfWord = 0, EndOfParagraph, EndOfFile }

public readonly record struct Token(TokenType Type, string? Word = null)
{
    public Token(string word) : this(TokenType.EndOfWord, word) { }
}
