using System;
using System.IO;
using System.Reflection.PortableExecutable;

namespace ParagraphWordCounting;
public class Program
{
    public static void ProcessAllWords(ITokenReader reader, ITokenProcessor processor)
    {
        Token token;
        do
        {
            token = reader.GetNextToken();
            processor.ProcessToken(token);
        } 
        while (token.Type != TokenType.EndOfFile);

        processor.PrintResult();
    }

    public static void ProgramProcess(string[] args)
    {
        var state = new ProgramInputState();
        if (!state.InitializeFromCommandLineArgs(args, 1)) return;

        var reader = new ByLineTextReader(state.Reader!);
        var processor = new ParagraphWordProcessor(state.Writer!);

        ProcessAllWords(reader, processor);

        state.Dispose();
    }

    static void Main(string[] args)
    {
        ProgramProcess(args);
    }
}
