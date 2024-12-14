using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TextJustification;

public class ProgramInputState : IDisposable
{
    private const string ArgumentErrorMessage    = "Argument Error";
    private const string FileErrorMessage        = "File Error";

    public int RequiredArgumentCount { get; init; } = 0;
    public bool HasStreamWriter      { get; init; } = false;

    public TextReader? Reader { get; private set; }
    public TextWriter? Writer { get; private set; }

    public int MaxTextWidth = 0;

    public ProgramInputState(int requiredArgumentCount)
    {
        this.RequiredArgumentCount = requiredArgumentCount;
    }

    public ProgramInputState(int requiredArgumentCount, bool hasStreamWriter) 
    {
        this.RequiredArgumentCount = requiredArgumentCount;
        this.HasStreamWriter = hasStreamWriter;
    }

    public bool InitializeReader(string fileName) 
    {
        try
        {
            Reader = new StreamReader(fileName);
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

    public bool InitializeWriter(string fileName)
    {
        try
        {
            Writer = new StreamWriter(fileName);
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

    public bool InitializeTextWidth(string textWidth) 
    {
        if(int.TryParse(textWidth, out int parsedWidth))
        {
            if (parsedWidth >= 1)
            {
                MaxTextWidth = parsedWidth;
                return true;
            }
        }
        Console.WriteLine(ArgumentErrorMessage);
        return false;
    }

    public bool InitializeFromCommandLineArgs(string[] args)
    {
        if (args.Length != RequiredArgumentCount)
        {
            Console.WriteLine(ArgumentErrorMessage);
            return false;
        }

        if(!InitializeReader(args[0])) return false;

        if (HasStreamWriter) 
        {
            if(!InitializeWriter(args[1])) return false;
        }
        else 
        {
            Writer = new StringWriter();
        }

        if(!InitializeTextWidth(args[2])) return false;

        return true;
    }

    public void Dispose()
    {
        Reader?.Dispose();
        Writer?.Dispose();
    }
}