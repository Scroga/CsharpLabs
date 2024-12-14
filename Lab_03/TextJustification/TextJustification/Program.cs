namespace TextJustification;
public class Program
{
    public static void ProcessAllWords(ITokenReader reader, ITokenProcessor processor)
    {
        Token token;
        var debugDecorator = new DebugTokenReaderDecorator();
        do
        {
            token = debugDecorator.Print(reader.GetNextToken(), printStats: false);
            processor.ProcessToken(token);
        }
        while (token.Type != TokenType.EndOfFile);

        processor.PrintResult();
    }

    public static void ProgramProcess(string[] args)
    {
        var state = new ProgramInputState(3, hasStreamWriter: true);
        if (!state.InitializeFromCommandLineArgs(args)) return;

        var reader       = new ByCharTextReader(state.Reader!);
        var processor    = new LineJustifier(state.Writer!, state.MaxTextWidth);

        ProcessAllWords(reader, processor);

        state.Dispose();
    }

    static void Main(string[] args)
    {
        ProgramProcess(args);
    }
}
