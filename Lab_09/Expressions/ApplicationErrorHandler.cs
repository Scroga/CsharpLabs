using System;
using System.IO;

namespace Expressions;

#nullable enable

public class ApplicationErrorHandler
{
    TextWriter? ErrorOutput;

    public ApplicationErrorHandler(TextWriter errorOutput)
    {
        ErrorOutput = errorOutput;
    }

    public void ExecuteProgram(IProgram program) 
    {
        try 
        {
            program.Run();
        }
        catch (Exception ex)
        {
            ErrorOutput!.WriteLine(ex.Message);
        }
    }
}
