using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Huffman1;

public class ApplicationErrorHandler
{
    protected TextWriter ErrorOutput { get; private init; }

    public ApplicationErrorHandler(TextWriter errorOutput)
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
