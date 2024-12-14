using System;
using System.IO;
using System.Text;
using MultipleJustification.FileProcessing;

#nullable enable

namespace MultipleJustification;

public class AppErrorHandler
{
    protected TextWriter ErrorOutput { get; private init; }
    public AppErrorHandler(TextWriter errorOutput)
    {
        ErrorOutput = errorOutput;
    }

    public void ExecuteProgram(IProgramCore programCore, string[] args)
    {
        try
        {
            programCore.Run(args);
        }
        catch (InvalidArgumentsApplicationException)
        {
            ErrorOutput.WriteLine("Argument Error");
        }
        catch (FileAccesErrorApplicationExeption)
        {
            ErrorOutput.WriteLine("File Error");
        }
    }
}