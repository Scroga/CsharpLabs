using MultipleJustification;
using MultipleJustification.FileProcessing;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;

#nullable enable

namespace MultipleJustification;

internal class Program
{
    static void Main(string[] args)
    {
        var appErrorHandler = new AppErrorHandler(Console.Out);
        appErrorHandler.ExecuteProgram(new MultipleTextJustifier(), args);
    }
}

public class MultipleTextJustifier : IProgramCore 
{
    public void Run(string[] args) 
    {
        var state = new ArgsToInputOutputState(args);
        try
        {
            state.CheckHighlightWhitespaceFunction();
            state.CheckArgumentCout(state.IsEnabledHighlightWhitespace ? 4 : 3);
            state.CheckTextWidth(args.Length - 1);
            state.OpenOutputFile(args.Length - 2);

            MultipleFileProcessing.ProcessFiles(state);
        }
        finally 
        {
            state.Dispose();
        }
    }
}