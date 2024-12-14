//namespace ParagraphWordCountingXUnitTests;

//public class TokenReaderTest
//{
//    [Fact]
//    public void TokenTest_EndOfWord()
//    {
//        // Arrange
//        var input = """
//                    OneWord
//                    """;

//        using var reader = new StringReader(input);
//        var tokenReader = new ByLineTextReader(reader!);

//        // Act
//        Token token = tokenReader.GetNextToken();

//        // Assert
//        Assert.Equal("OneWord", token.Word);
//        Assert.Equal(TokenType.EndOfWord, token.Type);

//    }

//    [Fact]
//    public void TokenTest_EndOfParagraph()
//    {
//        // Arrange
//        var input = """


//                    """;

//        var reader = new StringReader(input);

//        var tReader = new ByLineTextReader(reader!);

//        // Act
//        Token token = tReader.GetNextToken();

//        // Assert
//        Assert.Equal("", token.Word);
//        Assert.Equal(TokenType.EndOfParagraph, token.Type);

//    }

//    [Fact]
//    public void TestByLineTextReader_GetNextToken()
//    {
//        // Arrange
//        var input = """
//                    Two words

//                    OneWord
//                    """;

//        using var reader = new StringReader(input);
//        var tokenReader = new ByLineTextReader(reader);

//        // Act & Assert
//        Assert.Equal(TokenType.EndOfWord, tokenReader.GetNextToken().Type);
//        Assert.Equal(TokenType.EndOfWord, tokenReader.GetNextToken().Type);
//        Assert.Equal(TokenType.EndOfParagraph, tokenReader.GetNextToken().Type);
//        Assert.Equal(TokenType.EndOfWord, tokenReader.GetNextToken().Type);
//        Assert.Equal(TokenType.EndOfFile, tokenReader.GetNextToken().Type);
//    }

//    [Fact]
//    public void TestInitializeFromCommandLineArgs_InvalidArguments()
//    {
//        // Arrange
//        var programState = new ProgramInputState();
//        string[] args = Array.Empty<string>();

//        // Act
//        bool result = programState.InitializeFromCommandLineArgs(args, 1);

//        // Assert
//        Assert.False(result);
//    }

//    [Fact]
//    public void TestInitializeFromCommandLineArgs_ValidArguments()
//    {
//        // Arrange
//        var programState = new ProgramInputState();
//        string[] args = { "test1.txt" }; // file should be in the /net8.0 directory

//        // Act
//        bool result = programState.InitializeFromCommandLineArgs(args, 1);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public void TestInitializeFromCommandLineArgs_InvalidFilePath()
//    {
//        // Arrange
//        var programState = new ProgramInputState();
//        string[] args = { "do_not_exist.txt" };

//        // Act
//        bool result = programState.InitializeFromCommandLineArgs(args, 1);

//        // Assert
//        Assert.False(result);
//    }

//    [Fact]
//    public void TestParagraphWordProcessor_CountWords()
//    {
//        // Arrange
//        using var writer = new StringWriter();
//        var processor = new ParagraphWordProcessor(writer);
//        var tokens = new[]
//        {
//            new Token(TokenType.EndOfWord, "some"),
//            new Token(TokenType.EndOfWord, "words"),
//            new Token(TokenType.EndOfParagraph, string.Empty)
//        };

//        // Act
//        foreach (var token in tokens)
//        {
//            processor.ProcessToken(token);
//        }
//        processor.PrintResult();

//        // Assert
//        Assert.Equal("2" + writer.NewLine, writer.ToString());
//    }

//    [Fact]
//    public void TestProcessAllWords_OneLineInput()
//    {
//        // Arrange
//        var input = """
//                    If a train station is where the train stops, what is a work station?
                    
//                    """;

//        using var reader = new StringReader(input);
//        using var writer = new StringWriter();

//        var tokenReader = new ByLineTextReader(reader);
//        var tokenProcessor = new ParagraphWordProcessor(writer);

//        // Act
//        Program.ProcessAllWords(tokenReader, tokenProcessor);

//        // Assert
//        Assert.Equal("14" + writer.NewLine, writer.ToString());
//    }

//    [Fact]
//    public void TestProcessAllWords_SimpleTextInput()
//    {
//        // Arrange
//        var input = """
//                    If a train station is where the train stops, what is a work station?

//                    A work station?
//                    Yes!
//                    """;

//        using var reader = new StringReader(input);
//        using var writer = new StringWriter();

//        var tokenReader = new ByLineTextReader(reader);
//        var tokenProcessor = new ParagraphWordProcessor(writer);

//        // Act
//        Program.ProcessAllWords(tokenReader, tokenProcessor);

//        // Assert
//        Assert.Equal("14\r\n4" + writer.NewLine, writer.ToString());
//    }

//    [Fact]
//    public void TestProcessAllWords_ComplexTextInput()
//    {
//        // Arrange
//        var input = """
//                    To be, or not to be, that is the question:
//                    Whether 'tis nobler in the mind to suffer
//                    The slings and arrows of outrageous fortune,
//                    Or to take Arms against a Sea of troubles,
//                    And by opposing end them: to die, to sleep
//                    No more; and by a sleep, to say we end
//                    The heart-ache, and the thousand natural shocks
//                    That Flesh is heir to? 'Tis a consummation
//                    Devoutly to be wished. To die, to sleep,
//                    To sleep, perchance to Dream; aye, there's the rub,
//                    For in that sleep of death, what dreams may come,
//                    When we have shuffled off this mortal coil,
//                    Must give us pause.

//                    There's the respect
//                    That makes Calamity of so long life:
//                    For who would bear the Whips and Scorns of time,
//                    The Oppressor's wrong, the proud man's Contumely,
//                    The pangs of dispised Love, the Law’s delay,
//                    The insolence of Office, and the spurns
//                    That patient merit of th'unworthy takes,
//                    When he himself might his Quietus make
//                    With a bare Bodkin? Who would Fardels bear,
//                    To grunt and sweat under a weary life,
//                    But that the dread of something after death,
//                    The undiscovered country, from whose bourn
//                    No traveller returns, puzzles the will,
//                    And makes us rather bear those ills we have,
//                    Than fly to others that we know not of?

//                    Thus conscience does make cowards of us all,
//                    And thus the native hue of Resolution
//                    Is sicklied o'er, with the pale cast of Thought,
//                    And enterprises of great pitch and moment,
//                    With this regard their Currents turn awry,
//                    And lose the name of Action. Soft you now,
//                    The fair Ophelia? Nymph, in thy Orisons
//                    Be all my sins remember'd.
                    

//                    """;

//        using var reader = new StringReader(input);
//        using var writer = new StringWriter();

//        var tokenReader = new ByLineTextReader(reader);
//        var tokenProcessor = new ParagraphWordProcessor(writer);

//        // Act
//        Program.ProcessAllWords(tokenReader, tokenProcessor);

//        // Assert
//        Assert.Equal("107\r\n109\r\n59" + writer.NewLine, writer.ToString());
//    }
//}