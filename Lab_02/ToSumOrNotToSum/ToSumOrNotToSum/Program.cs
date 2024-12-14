using System;
using System.IO;


namespace ToSumOrNotToSum;

public class Program
{
    static void Main(string[] args)
    {
        var state = new ProgramInputState();
        if (!state.InitializeFromCommandLineArgs(args)) return;

        var counter = new ColumnSumCounter(state.Reader!, state.Writer!, state.columnName!);
        if (!counter.InitializeColumnSumCounter()) return;

        counter.Execute();
        state.Dispose();
    }
}

public class ProgramInputState : IDisposable
{
    public const string ArgumentErrorMessage = "Argument Error";
    public const string FileErrorMessage = "File Error";

    public TextReader?  Reader { get; private set; }
    public TextWriter?  Writer { get; private set; }
    public string?      columnName { get; private set; }

    public bool InitializeFromCommandLineArgs(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine(ArgumentErrorMessage);
            return false;
        }

        try
        {
            Reader      = new StreamReader(args[0]);
            Writer      = new StreamWriter(args[1]);
            columnName  = new string(args[2]);
        }
        catch (Exception)
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

