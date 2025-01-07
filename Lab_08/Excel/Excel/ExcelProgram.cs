using System;

namespace Excel;

public class ExcelProgram : IProgram
{
    private readonly string[] _args;
    private void CheckArgsCount(int count, string[] args)
    {
        if (args.Length != count)
        {
            throw new InvalidArgumentsApplicationException();
        }
    }
    
    public ExcelProgram(string[] args)
    {
        _args = args;
    }

    public void Run() 
    {
        CheckArgsCount(2, _args);
        var table = new ExcelTable(_args[0]);
        var writer = new TableWriter(_args[1]);

        table.Evaluate();
        writer.WriteTable(table.GetTable());
        writer.DebugPrint(table.GetTable());
    }
}
