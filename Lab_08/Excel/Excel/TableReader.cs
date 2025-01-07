using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Excel;

#nullable disable
public class TableReader : ITableReader, IDisposable
{
    StreamReader _reader;

    public void OpenInputFile(string fileName)
    {
        if (_reader != null) _reader.Dispose();

        try
        {
            _reader = new StreamReader(fileName);
        }
        catch
        {
            throw new FileAccesErrorApplicationException();
        }
    }

    public string[][] LoadTable(string fileName) 
    {
        OpenInputFile(fileName);

        List<string[]> tableList = new List<string[]>();

        string line = _reader!.ReadLine();

        while (line != null)
        {
            tableList.Add(line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
            line = _reader!.ReadLine();
        }

        string[][] table = tableList.ToArray();

        Dispose();
        return table;
    }

    public void Dispose() 
    {
        _reader?.Dispose();
    }
}
