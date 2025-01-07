using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Excel;

#nullable disable
public class TableWriter : ITableWriter, IDisposable
{
    private StreamWriter _writer;

    public TableWriter(string fileName) 
    {
        OpenOutputFile(fileName);
    }
    public void OpenOutputFile(string fileName)
    {
        try
        {
            _writer = new StreamWriter(fileName);
        }
        catch
        {
            throw new FileAccesErrorApplicationException();
        }
    }

    public void WriteTable(string[][] table) 
    {
        foreach (var row in table)
        {
            foreach (var cell in row) 
            {
                _writer!.Write($"{cell} ");
            }
            _writer!.WriteLine();
        }
        Dispose();
    }

    public void DebugPrint(string[][] table)
    {
        foreach (var row in table) 
        {
            foreach (var cell in row) 
            {
                Console.Write($"|{cell}|_");
            }
            Console.WriteLine();
        }
    }

    public void Dispose() 
    {
        _writer?.Dispose();
    }
}
