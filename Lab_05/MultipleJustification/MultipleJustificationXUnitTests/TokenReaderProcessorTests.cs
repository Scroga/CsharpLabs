//using Newtonsoft.Json.Linq;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Reflection.PortableExecutable;
//using MultipleJustification;

//namespace MultipleJustificationXUnitTests;

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

//        var expectedTokens = new List<Token>()
//        {
//            new Token(TokenType.EndOfWord, "First"),
//            new Token(TokenType.EndOfWord, "line."),
//            new Token(TokenType.EndOfParagraph, string.Empty)
//        };

//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                               .Select(_ => reader.GetNextToken())
//                               .ToList();

//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//            Assert.Equal(expectedTokens[i].Word, tokens[i].Word);
//        }
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

//        var expectedTokens = new List<Token>()
//        {
//            new Token(TokenType.EndOfWord, "HereIs"),
//            new Token(TokenType.EndOfWord, "Another"),
//            new Token(TokenType.EndOfParagraph, string.Empty),
//            new Token(TokenType.EndOfWord, "ShortText"),
//            new Token(TokenType.EndOfFile, string.Empty)
//        };

//        var tokens = Enumerable.Range(0, expectedTokens.Count)
//                               .Select(_ => reader.GetNextToken())
//                               .ToList();

//        for (int i = 0; i < expectedTokens.Count; i++)
//        {
//            Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
//            Assert.Equal(expectedTokens[i].Word, tokens[i].Word);
//        }
//    }
//}

//public class ProgramTests
//{
//    const string KeyWord = "--highlight-spaces";
//    const string InvalidInputFileName = "xx.in";

//    [Fact]
//    public void SingleFile_HighlightSpaces_NoInvalidFiles()
//    {
//        string inputFileName = "testFiles/01.in";
//        string outputFileName = "testFiles/my01.out";
//        string expectedOutputFileName = "testFiles/ex01.out";

//        string[] args = { KeyWord,
//                          inputFileName,
//                          outputFileName, "17" };

//        var state = new ArgsToInputOutputState(args);
//        state.CheckHighlightWhitespaceFunction();
//        state.CheckArgumentCout(state.IsEnabledHighlightWhitespace ? 4 : 3);
//        state.CheckTextWidth(args.Length - 1);
//        state.OpenOutputFile(args.Length - 2);

//        MultipleFileProcessing.ProcessFiles(state);
//        state.Dispose();

//        var actualOutput = File.ReadAllText(outputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFileName);
//    }

//    [Fact]
//    public void MultipleFiles_HighlightSpaces_NoInvalidFiles()
//    {
//        string inputFileName = "testFiles/01.in";
//        string outputFileName = "testFiles/my02.out";
//        string expectedOutputFileName = "testFiles/ex02.out";

//        string[] args = { KeyWord,
//                          inputFileName,
//                          inputFileName,
//                          inputFileName,
//                          outputFileName, "17" };

//        var state = new ArgsToInputOutputState(args);
//        state.CheckHighlightWhitespaceFunction();
//        state.CheckArgumentCout(state.IsEnabledHighlightWhitespace ? 4 : 3);
//        state.CheckTextWidth(args.Length - 1);
//        state.OpenOutputFile(args.Length - 2);

//        MultipleFileProcessing.ProcessFiles(state);
//        state.Dispose();

//        var actualOutput = File.ReadAllText(outputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFileName);
//    }

//    [Fact]
//    public void MultipleFiles_HighlightSpaces_MultipleInvalidFiles()
//    {
//        string inputFileName = "testFiles/01.in";
//        string outputFileName = "testFiles/my08.out";
//        string expectedOutputFileName = "testFiles/ex08.out";

//        string[] args = { KeyWord,
//                          InvalidInputFileName,
//                          InvalidInputFileName,
//                          InvalidInputFileName,
//                          inputFileName,
//                          InvalidInputFileName,
//                          InvalidInputFileName,
//                          outputFileName, "80" };

//        var state = new ArgsToInputOutputState(args);
//        state.CheckHighlightWhitespaceFunction();
//        state.CheckArgumentCout(state.IsEnabledHighlightWhitespace ? 4 : 3);
//        state.CheckTextWidth(args.Length - 1);
//        state.OpenOutputFile(args.Length - 2);

//        MultipleFileProcessing.ProcessFiles(state);
//        state.Dispose();

//        var actualOutput = File.ReadAllText(outputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFileName);
//    }

//    [Fact]
//    public void MultipleFiles_NoHighlightSpaces_NoInvalidFiles()
//    {
//        string inputFileName = "testFiles/01.in";
//        string outputFileName = "testFiles/my12.out";
//        string expectedOutputFileName = "testFiles/ex12.out";

//        string[] args = { inputFileName,
//                          inputFileName,
//                          inputFileName,
//                          outputFileName, "17" };

//        var state = new ArgsToInputOutputState(args);
//        state.CheckHighlightWhitespaceFunction();
//        state.CheckArgumentCout(state.IsEnabledHighlightWhitespace ? 4 : 3);
//        state.CheckTextWidth(args.Length - 1);
//        state.OpenOutputFile(args.Length - 2);

//        MultipleFileProcessing.ProcessFiles(state);
//        state.Dispose();

//        var actualOutput = File.ReadAllText(outputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFileName);
//    }

//    [Fact]
//    public void SingleFile_HighlightSpaces_NoInvalidFiles_LongWordsCheck()
//    {
//        string inputFileName = "testFiles/02.in";
//        string outputFileName = "testFiles/my03.out";
//        string expectedOutputFileName = "testFiles/ex03.out";

//        string[] args = { KeyWord,
//                          inputFileName,
//                          outputFileName, "12" };

//        var state = new ArgsToInputOutputState(args);
//        state.CheckHighlightWhitespaceFunction();
//        state.CheckArgumentCout(state.IsEnabledHighlightWhitespace ? 4 : 3);
//        state.CheckTextWidth(args.Length - 1);
//        state.OpenOutputFile(args.Length - 2);

//        MultipleFileProcessing.ProcessFiles(state);
//        state.Dispose();

//        var actualOutput = File.ReadAllText(outputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFileName);
//    }


//    [Fact]
//    public void SingleBigFile_NoHighlightSpaces_NoInvalidFiles()
//    {
//        string inputFileName = "testFiles/LoremIpsum.txt";
//        string outputFileName = "testFiles/LoremIpsumOutput.txt";
//        string expectedOutputFileName = "testFiles/LoremIpsum_Aligned.txt";

//        string[] args = { inputFileName,
//                          outputFileName, "40" };

//        var state = new ArgsToInputOutputState(args);
//        state.CheckHighlightWhitespaceFunction();
//        state.CheckArgumentCout(state.IsEnabledHighlightWhitespace ? 4 : 3);
//        state.CheckTextWidth(args.Length - 1);
//        state.OpenOutputFile(args.Length - 2);

//        MultipleFileProcessing.ProcessFiles(state);
//        state.Dispose();

//        var actualOutput = File.ReadAllText(outputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        var expectedOutput = File.ReadAllText(expectedOutputFileName)
//                                 .Replace("\r\n", "\n")
//                                 .Replace("\r", "\n");

//        Assert.Equal(expectedOutput, actualOutput);
//        File.Delete(outputFileName);
//    }
//}