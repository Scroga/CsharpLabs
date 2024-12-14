//using Newtonsoft.Json.Linq;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Reflection.PortableExecutable;
//using TextJustification;

//namespace TextJustificationXUnitTests;

//public class ProgramInputTest
//{
//    private readonly string _validFilePath = "testfile.txt";
//    private readonly string _invalidFilePath = "invalidfile.txt";

//    [Fact]
//    public void InitializeReader_InvalidFilePath()
//    {
//        var state = new ProgramInputState(1, true);
//        bool outputState = state.InitializeReader(_invalidFilePath);

//        Assert.False(outputState);
//        Assert.Null(state.Reader);
//    }

//    [Fact]
//    public void InitializeReader_ValidFilePath()
//    {
//        var state = new ProgramInputState(1, true);
//        bool outputState = state.InitializeReader(_validFilePath);

//        Assert.True(outputState);
//        Assert.NotNull(state.Reader);
//    }

//    [Fact]
//    public void InitializeTextWidth_InvalidTextWidth_NotDigit()
//    {
//        string input = "abc";

//        var state = new ProgramInputState(1, false);
//        bool outputState = state.InitializeTextWidth(input);

//        Assert.False(outputState);
//    }

//    [Fact]
//    public void InitializeTextWidth_InvalidTextWidth_LessThenOne()
//    {
//        string input = "-2";

//        var state = new ProgramInputState(1, false);
//        bool outputState = state.InitializeTextWidth(input);

//        Assert.False(outputState);
//    }

//    [Fact]
//    public void InitializeTextWidth_ValidTextWidth()
//    {
//        string input = "3";

//        var state = new ProgramInputState(1, false);
//        bool outputState = state.InitializeTextWidth(input);

//        Assert.True(outputState);
//    }

//    [Fact]
//    public void InitializeFromCommandLineArgs_ValidArguments()
//    {
//        string[] input = { _validFilePath, "validOutput.txt", "3" };

//        var state = new ProgramInputState(3, false);
//        bool outputState = state.InitializeFromCommandLineArgs(input);

//        Assert.True(outputState);
//    }
//}

//public class TokenReaderTests
//{
//    [Fact]
//    public void GetNextToken_ReturnsEndOfFile_WhenReaderIsEmpty()
//    {
//        var reader = new ByCharTextReader(new StringReader(string.Empty));

//        var token = reader.GetNextToken();

//        var expectedToken = new Token(TokenType.EndOfFile, string.Empty);

//        Assert.Equal(expectedToken, token);
//    }

//    [Fact]
//    public void GetNextToken_ReturnsEndOfParagraph_OnEmptyLine()
//    {
//        var inputText = """
//                        First  line.

//                        Second  line
//                        """;
//        var reader = new ByCharTextReader(new StringReader(inputText));

//        var tokens = new List<Token>();
//        tokens.Add(reader.GetNextToken());
//        tokens.Add(reader.GetNextToken());
//        tokens.Add(reader.GetNextToken());

//        var expectedTokens = new List<Token>()
//        {
//            new Token(TokenType.EndOfWord, "First"),
//            new Token(TokenType.EndOfWord, "line."),
//            new Token(TokenType.EndOfParagraph, string.Empty)
//        };

//        Assert.Equal(expectedTokens[0].Word, tokens[0].Word);
//        Assert.Equal(expectedTokens[1].Word, tokens[1].Word);
//        Assert.Equal(expectedTokens[2].Word, tokens[2].Word);

//        Assert.Equal(expectedTokens[0].Type, tokens[0].Type);
//        Assert.Equal(expectedTokens[1].Type, tokens[1].Type);
//        Assert.Equal(expectedTokens[2].Type, tokens[2].Type);
//    }

//    [Fact]
//    public void GetNextToken_ReturnsEndOfFile_SmallText()
//    {
//        var inputText = """
//                        HereIs 
//                           Another


//                        ShortText
//                        """;
//        var reader = new ByCharTextReader(new StringReader(inputText));

//        var tokens = new List<Token>();
//        tokens.Add(reader.GetNextToken());
//        tokens.Add(reader.GetNextToken());
//        tokens.Add(reader.GetNextToken());
//        tokens.Add(reader.GetNextToken());
//        tokens.Add(reader.GetNextToken());

//        var expectedTokens = new List<Token>()
//        {
//            new Token(TokenType.EndOfWord, "HereIs"),
//            new Token(TokenType.EndOfWord, "Another"),
//            new Token(TokenType.EndOfParagraph, string.Empty),
//            new Token(TokenType.EndOfWord, "ShortText"),
//            new Token(TokenType.EndOfFile, string.Empty)
//        };

//        Assert.Equal(expectedTokens[0].Word, tokens[0].Word);
//        Assert.Equal(expectedTokens[1].Word, tokens[1].Word);
//        Assert.Equal(expectedTokens[2].Word, tokens[2].Word);
//        Assert.Equal(expectedTokens[3].Word, tokens[3].Word);
//        Assert.Equal(expectedTokens[4].Word, tokens[4].Word);

//        Assert.Equal(expectedTokens[0].Type, tokens[0].Type);
//        Assert.Equal(expectedTokens[1].Type, tokens[1].Type);
//        Assert.Equal(expectedTokens[2].Type, tokens[2].Type);
//        Assert.Equal(expectedTokens[3].Type, tokens[3].Type);
//        Assert.Equal(expectedTokens[4].Type, tokens[4].Type);
//    }
//}

//public class ProgramTests
//{
//    [Fact]
//    public void InputSimpleTextFile()
//    {
//        string inputFilePath = "simpleText.txt";
//        string outputFilePath = "output.txt";
//        string expectedOutputFilePath = "simpleFormat.txt";

//        using var streamReader = new StreamReader(File.OpenRead(inputFilePath));
//        using (var streamWriter = new StreamWriter(outputFilePath))
//        {
//            var tokenReader = new ByCharTextReader(streamReader);
//            var tokenProcessor = new LineJustifier(streamWriter, 12);

//            Program.ProcessAllWords(tokenReader, tokenProcessor);

//            streamWriter.Flush();
//        }
//        var actualOutput = File.ReadAllText(outputFilePath)
//                               .Replace("\r\n", "\n")
//                               .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFilePath)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFilePath);
//    }

//    [Fact]
//    public void InputPlainFile()
//    {
//        string inputFilePath = "plain.txt";
//        string outputFilePath = "output.txt";
//        string expectedOutputFilePath = "format.txt";

//        using var streamReader = new StreamReader(File.OpenRead(inputFilePath));
//        using (var streamWriter = new StreamWriter(outputFilePath))
//        {
//            var tokenReader = new ByCharTextReader(streamReader);
//            var tokenProcessor = new LineJustifier(streamWriter, 17);

//            Program.ProcessAllWords(tokenReader, tokenProcessor);

//            streamWriter.Flush();
//        }
//        var actualOutput = File.ReadAllText(outputFilePath)
//                               .Replace("\r\n", "\n")
//                               .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFilePath)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFilePath);
//    }

//    [Fact]
//    public void InputLoremIpsumFile()
//    {
//        string inputFilePath = "LoremIpsum.txt";
//        string outputFilePath = "output.txt";
//        string expectedOutputFilePath = "LoremIpsum_Aligned.txt";

//        using var streamReader = new StreamReader(File.OpenRead(inputFilePath));
//        using (var streamWriter = new StreamWriter(outputFilePath))
//        {
//            var tokenReader = new ByCharTextReader(streamReader);
//            var tokenProcessor = new LineJustifier(streamWriter, 40);

//            Program.ProcessAllWords(tokenReader, tokenProcessor);

//            streamWriter.Flush();
//        }
//        var actualOutput = File.ReadAllText(outputFilePath)
//                               .Replace("\r\n", "\n")
//                               .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFilePath)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFilePath);
//    }
//}