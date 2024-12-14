using System;
using System.IO;

namespace InternetoveKnihkupectvi;

public class AppErrorHandler
{
    protected TextWriter ErrorOutput { get; private init; }
    public AppErrorHandler(TextWriter errorOutput)
    {
        ErrorOutput = errorOutput;
    }

    public void ExecuteProgram(IProgramCore programCore)
    {
        try
        {
            programCore.Run();
        }
        catch (InvalidEShopDataException)
        {
            ErrorOutput.WriteLine("Data error.");
            return;
        }
        catch (Exception ex) when (ex is FormatException || ex is IndexOutOfRangeException)
        {
            ErrorOutput.WriteLine("Data error.");
            return;
        }
    }
}
