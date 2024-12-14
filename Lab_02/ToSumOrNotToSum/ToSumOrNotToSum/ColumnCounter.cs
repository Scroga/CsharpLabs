using System;
using System.IO;

namespace ToSumOrNotToSum;

public class ColumnSumCounter
{
    private TextReader _reader;
    private TextWriter _writer;
    private string _columnName;
    private int _columnCount = 0;
    private int _columnIndex = -1;
    private long _counter = 0;

    public const string FormatErrorMessage = "Invalid File Format";
    public const string NonExistentErrorMessage = "Non-existent Column Name";
    public const string InvalidValueErrorMessage = "Invalid Integer Value";

    public ColumnSumCounter(TextReader reader, TextWriter writer, string columnName)
    {
        _reader = reader;
        _writer = writer;
        _columnName = columnName;
    }

    private bool InitializeColumnIndex()
    {
        string? line = _reader.ReadLine();

        if (line == null)
        {
            Console.WriteLine(FormatErrorMessage);
            return false;
        }

        string[] words = line.Split(new char[] { ' ', '\n', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        _columnCount = words.Length;

        if (_columnCount < 1)
        {
            Console.WriteLine(FormatErrorMessage);
            return false;
        }

        for (int i = 0; i < _columnCount; i++)
        {
            if (string.Equals(words[i], _columnName, StringComparison.Ordinal))
            {
                _columnIndex = i;
                break;
            }
        }

        if (_columnIndex == -1)
        {
            Console.WriteLine(NonExistentErrorMessage);
            return false;
        }
        return true;
    }

    public bool InitializeColumnSumCounter()
    {
        return InitializeColumnIndex();
    }

    /*
    Kazdopadne zavada je v tom, ze nespravne rozlisujete situace 
    „Invalid File Format“ versus „Non-existent Column Name“ – 
    zkuste si znovu projit zadani, a promyslet, zda se vzdy chovate spravne dle nej. 
    */

    public void Execute()
    {
        string? line;
        while ((line = _reader.ReadLine()) is not null)
        {
            string[] words = line.Split(new char[] { ' ', '\n', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            if (_columnCount != words.Length)
            {
                Console.WriteLine(FormatErrorMessage);
                return;
            }

            if (int.TryParse(words[_columnIndex], out int columnValue))
            {
                _counter += columnValue;
            }
            else
            {
                Console.WriteLine(InvalidValueErrorMessage);
                return;
            }

        }
        _writer.WriteLine(_columnName);
        _writer.WriteLine(new string('-', _columnName.Length));
        _writer.WriteLine(_counter);
    }
}
