using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagraphWordCounting;

public class ProgramInputState : IDisposable
{
    public const string ArgumentErrorMessage = "Argument Error";
    public const string FileErrorMessage     = "File Error";

    public TextReader? Reader { get; private set; }
    public TextWriter? Writer { get; private set; }

    public bool InitializeFromCommandLineArgs(string[] args, int requiredArgumentCount, bool hasStreamWriter = false)
    {
        if (args.Length < requiredArgumentCount)
        {
            Console.WriteLine(ArgumentErrorMessage);
            return false;
        }

        try
        {
            Reader = new StreamReader(args[0]);
        }
        catch (IOException)
        {
            Console.WriteLine(FileErrorMessage);
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine(FileErrorMessage);
            return false;
        }
        catch (ArgumentException)
        {
            Console.WriteLine(FileErrorMessage);
            return false;
        }

        if (!hasStreamWriter)
        {
            Writer = new StringWriter();
            return true;
        }

        try
        {
            Writer = new StreamWriter(args[1]);
        }
        catch (IOException)
        {
            Console.WriteLine(FileErrorMessage);
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine(FileErrorMessage);
            return false;
        }
        catch (ArgumentException)
        {
            Console.WriteLine(FileErrorMessage);
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        Reader?.Dispose();
        Writer?.Dispose();
    }
}