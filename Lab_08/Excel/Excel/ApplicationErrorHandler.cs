using System;
using System.IO;

namespace Excel;

public class ApplicationErrorHandler
{
    protected TextWriter ErrorOutput { get; init; }

    public ApplicationErrorHandler(TextWriter errorOutput)
    {
        ErrorOutput = errorOutput;
    }

    public void ExicuteProgram(IProgram program) 
    {
        try
        {
            program.Run();
        }
        catch (InvalidArgumentsApplicationException)
        {
            ErrorOutput.WriteLine("Argument Error");
        }
        catch (FileAccesErrorApplicationException) 
        {
            ErrorOutput.WriteLine("File Error");
        }
        catch (Exception ex)
        {
            ErrorOutput.WriteLine(ex.Message);
        }
    }
}
