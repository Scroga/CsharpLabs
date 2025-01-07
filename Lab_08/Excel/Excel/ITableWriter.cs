using System;
using System.IO;    

namespace Excel;

public interface ITableWriter
{
    void WriteTable(string[][] table);
}
