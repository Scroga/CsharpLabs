using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

#nullable enable

namespace MultipleJustification;

public static class MultipleFileProcessing 
{
    public static void ProcessTokensUntilEndOfFile(ITokenReader reader, ITokenProcessor processor)
    {
        while (reader.GetNextToken() is { Type: not TokenType.EndOfFile } token)
        {
            processor.ProcessToken(token);
        }
    }
    public static void ProcessFiles(ArgsToInputOutputState state) 
    {
        var tokenProcessor = state.IsEnabledHighlightWhitespace ?
                             new LineJustifier(new HighlightSpacesWriter(state.OutputWriter!), state.MaxTextWidth):
                             new LineJustifier(new DefaultWriter(state.OutputWriter!), state.MaxTextWidth);

        for (int i = 0; i < state.Args.Length - 2; i++)
        {
            if (i == 0 && state.IsEnabledHighlightWhitespace) continue;

            if (state.IsFileAvailable(state.Args[i]))
            {
                var tokenReader = new ByCharTextReader(state.InputReader!);
                ProcessTokensUntilEndOfFile(tokenReader, tokenProcessor);
            }
        }
        tokenProcessor.WriteResult();
    }
}
