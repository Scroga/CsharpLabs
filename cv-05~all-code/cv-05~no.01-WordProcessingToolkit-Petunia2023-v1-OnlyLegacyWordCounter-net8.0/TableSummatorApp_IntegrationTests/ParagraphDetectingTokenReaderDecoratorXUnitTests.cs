//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.PortableExecutable;
//using System.Text;
//using System.Threading.Tasks;

//using TokenProcessingFramework;

//namespace TokenProcessingFramework_IntegrationTests;

//public class ParagraphDetectingTokenReaderDecoratorXUnitTests
//{
//    [Fact]
//    public void SingleLine()
//    {
//        // Arrange
//        var input = """
//                    First  paragraph.
//                    """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//                {
//                    new Token(TokenType.Word, "First"),
//                    new Token(TokenType.Word, "paragraph."),
//                    new Token(TokenType.EndOfInput, null)
//                };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                               .Select(_ => decorator.ReadToken())
//                               .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//        }
//    }

//    [Fact]
//    public void SingleLine_EmptyLine()
//    {
//        // Arrange
//        var input = """

//                    """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//                {
//                    new Token(TokenType.EndOfInput, null),
//                };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                               .Select(_ => decorator.ReadToken())
//                               .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//        }
//    }

//    [Fact]
//    public void MultipleLines_FirstLineIsEmpty()
//    {
//        // Arrange
//        var input = """

//                    First(Second) paragraph.
//                    """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//                {
//                    new Token(TokenType.Word, "First(Second)"),
//                    new Token(TokenType.Word, "paragraph."),
//                    new Token(TokenType.EndOfInput, null),
//                };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                               .Select(_ => decorator.ReadToken())
//                               .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//        }
//    }

//    /*
//     * This test is FALSE
//     * It doesnt skip first empty lines,
//     * but defines them as EndOfParagraph.
//     */
//    [Fact]
//    public void MultipleLines_FirstLinesAreEmpty()
//    {
//        // Arrange
//        var input = """







//                    First(NotFirst) paragraph.
//                    """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//                {
//                    //new Token(TokenType.EndOfParagraph, null), ??
//                    new Token(TokenType.Word, "First(NotFirst)"),
//                    new Token(TokenType.Word, "paragraph."),
//                    new Token(TokenType.EndOfInput, null),
//                };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                               .Select(_ => decorator.ReadToken())
//                               .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type); // Expected type: Word, Actual type: EndOfParagraph
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//        }
//    }

//    [Fact]
//    public void TwoLines_NoEmptyLines()
//    {
//        // Arrange
//        var input = """
//                    First  paragraph.
//                    Second  paragraph.
//                    """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//                {
//                    new Token(TokenType.Word, "First"),
//                    new Token(TokenType.Word, "paragraph."),
//                    new Token(TokenType.Word, "Second"),
//                    new Token(TokenType.Word, "paragraph.")
//                };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                       .Select(_ => decorator.ReadToken())
//                       .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//        }
//    }

//    [Fact]
//    public void MultipleLines_OneEmptyLineBetweenParagraphs()
//    {
//        // Arrange
//        var input = """
//                First paragraph.

//                Second paragraph.

//                Third paragraph.
//                """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//            {
//                new Token(TokenType.Word, "First"),
//                new Token(TokenType.Word, "paragraph."),
//                new Token(TokenType.EndOfParagraph, null),
//                new Token(TokenType.Word, "Second"),
//                new Token(TokenType.Word, "paragraph."),
//                new Token(TokenType.EndOfParagraph, null),
//                new Token(TokenType.Word, "Third"),
//                new Token(TokenType.Word, "paragraph."),
//                new Token(TokenType.EndOfInput, null)
//            };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                               .Select(_ => decorator.ReadToken())
//                               .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//        }
//    }


//    [Fact]
//    public void MultipleLines_MultipleEmptyLinesBetweenParagraphs()
//    {
//        // Arrange
//        var input = """
//                    First  paragraph.






//                    Second paragraph.
//                    """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//                {
//                    new Token(TokenType.Word, "First"),
//                    new Token(TokenType.Word, "paragraph."),
//                    new Token(TokenType.EndOfParagraph, null),
//                    new Token(TokenType.Word, "Second")
//                };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                       .Select(_ => decorator.ReadToken())
//                       .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//        }
//    }

//    [Fact]
//    public void SingleLine_EndOfInputWithOneEmptyLine()
//    {
//        // Arrange
//        var input = """
//                    bam

//                    """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//                {
//                    new Token(TokenType.Word, "bam"),
//                    new Token(TokenType.EndOfInput, null)
//                };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                       .Select(_ => decorator.ReadToken())
//                       .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//        }
//    }


//    /*
//     * This test is FALSE
//     * Maybe it should skip empty lines after
//     * the last paragraph and just get EndOfInput.
//     */
//    [Fact]
//    public void MultipleLines_EndOfInputWithMultipleEmptyLines()
//    {
//        // Arrange
//        var input = """
//                    bam


//                    bim.




//                    """;

//        var reader = new ByLinesTokenReader(new StringReader(input));
//        var decorator = new ParagraphDetectingTokenReaderDecorator(reader);

//        var expectedTokens = new List<Token>()
//                {
//                    new Token(TokenType.Word, "bam"),
//                    new Token(TokenType.EndOfParagraph, null),
//                    new Token(TokenType.Word, "bim."),
//                    //new Token(TokenType.EndOfParagraph, null), // Maybe it shouldnt be here
//                    new Token(TokenType.EndOfInput, null)
//                };

//        // Act
//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                       .Select(_ => decorator.ReadToken())
//                       .ToList();

//        // Assert
//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type); // Expected type: EndOFInput, Actual type: EndOfParagraph
//            Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
//        }
//    }
//}

