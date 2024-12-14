using System;
using System.IO;
using System.Linq;

#nullable enable

namespace MultipleJustification;

public class ArgsToInputOutputState : IDisposable
{
    public readonly string[] Args;
    public bool IsEnabledHighlightWhitespace = false;
    public int MaxTextWidth = 0;
    public TextReader? InputReader { get; private set; } = null;
    public TextWriter? OutputWriter { get; private set; } = null;

    public ArgsToInputOutputState(string[] args)
    {
        Args = args;
    }
    
    public bool CheckArgumentCout(int expectedArgumentCount)
    {
        if (Args.Length >= expectedArgumentCount)
        {
            return true;
        }
        else
        {
            throw new InvalidArgumentsApplicationException();
        }
    }

    public bool CheckTextWidth(int textWidthArgIndex) 
    {
        int maxTextWidth;
        if (int.TryParse(Args[textWidthArgIndex], out maxTextWidth))
        {
            MaxTextWidth = maxTextWidth;
            return true;
        }
        else
        {
            throw new InvalidArgumentsApplicationException();
        }
    }

    public bool CheckHighlightWhitespaceFunction() 
    {
        if (Args.Any())
        {
            IsEnabledHighlightWhitespace = Args[0] == "--highlight-spaces";
            return IsEnabledHighlightWhitespace;
        }
        else
        {
            throw new InvalidArgumentsApplicationException();
        }
    }

    public bool IsFileAvailable(string fileName) 
    {
        try
        {
            InputReader?.Dispose();
            InputReader = new StreamReader(fileName);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public void OpenInputFile(int fileNameArgIndex)
    {
        try
        {
            InputReader = new StreamReader(Args[fileNameArgIndex]);
        }
        catch (IOException)
        {
            throw new FileAccesErrorApplicationExeption();
        }
        catch (UnauthorizedAccessException)
        {
            throw new FileAccesErrorApplicationExeption();
        }
        catch (ArgumentException)
        {
            throw new FileAccesErrorApplicationExeption();
        }
    }

    public void OpenOutputFile(int fileNameArgIndex)
    {
        try
        {
            OutputWriter = new StreamWriter(Args[fileNameArgIndex]);
        }
        catch (IOException)
        {
            throw new FileAccesErrorApplicationExeption();
        }
        catch (UnauthorizedAccessException)
        {
            throw new FileAccesErrorApplicationExeption();
        }
        catch (ArgumentException)
        {
            throw new FileAccesErrorApplicationExeption();
        }
    }

    public void Dispose()
    {
        InputReader?.Dispose();
        OutputWriter?.Dispose();
    }
}