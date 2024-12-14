using System;
using System.IO;

using TokenProcessingFramework;

#nullable enable

namespace TableSummatorApp;

// It should be it the HeaderProcessor.cs file, but im leaving it here, because its easier to send it to recodex :) 
public class HeaderProcessor : ITokenProcessor
{
    private readonly string _targetColumnName;
    private bool _foundTargetColumn = false;
    private int _currentColumn = 0;
    private int _targetColumnIndex;

    public bool FoundTargetColumn => _foundTargetColumn;
    public int TargetColumnIndex => _targetColumnIndex;
    public int HeaderColumnCount { get; private set; }

    public HeaderProcessor(string targetColumnName)
    {
        _targetColumnName = targetColumnName;
    }

    public void ProcessToken(Token token)
    {
        switch (token.Type)
        {
            case TokenType.Word:
                if (!_foundTargetColumn && StringComparer.CurrentCultureIgnoreCase.Compare(token.Value, _targetColumnName) == 0)
                {
                    _targetColumnIndex = _currentColumn;
                    _foundTargetColumn = true;
                }
                _currentColumn++;
                break;
            case TokenType.EndOfLine:
                if (_currentColumn == 0)
                {
                    throw new InvalidFileFormatApplicationException();
                }
                else if (!_foundTargetColumn)
                {
                    throw new NonExistentColumnNameApplicationException();
                }
                HeaderColumnCount = _currentColumn;
                _currentColumn = 0;
                break;
            default:
                throw new InvalidFileFormatApplicationException();
        }
    }

    public void Finish() { } // Im not sure if its ok to leave method like this
}

// This should be it the DataProcessor.cs file
public class DataProcessor : ITokenProcessor
{
    private readonly int _targetColumnIndex;
    private readonly int _headerColumnCount;
    private int _currentColumn = 0;
    private long _sum = 0;

    public long Sum => _sum;

    public DataProcessor(int targetColumnIndex, int headerColumnCount)
    {
        _targetColumnIndex = targetColumnIndex;
        _headerColumnCount = headerColumnCount;
    }

    public void ProcessToken(Token token)
    {
        switch (token.Type)
        {
            case TokenType.Word:
                if (_currentColumn == _targetColumnIndex)
                {
                    if (int.TryParse(token.Value!, out int value))
                    {
                        _sum += value;
                    }
                    else
                    {
                        throw new InvalidIntegerValueApplicationException();
                    }
                }
                _currentColumn++;
                break;
            case TokenType.EndOfLine:
                if (_currentColumn == 0 || _currentColumn != _headerColumnCount)
                {
                    throw new InvalidFileFormatApplicationException();
                }
                _currentColumn = 0;
                break;
            default:
                throw new InvalidFileFormatApplicationException();
        }
    }

    public void Finish() {} // Im not sure if its ok to leave method like this
}

// This should be it the OutputFormatter.cs file
public class OutputFormatter
{
    private readonly TextWriter _outputWriter;
    private readonly string _targetColumnName;

    public OutputFormatter(TextWriter outputWriter, string targetColumnName)
    {
        _outputWriter = outputWriter;
        _targetColumnName = targetColumnName;
    }

    public void WriteOutput(long sum)
    {
        _outputWriter.WriteLine(_targetColumnName);
        _outputWriter.WriteLine(new string('-', _targetColumnName.Length));
        _outputWriter.WriteLine(sum);
    }
}

public record class TableSummatorProcessor : ITokenProcessor
{
    private readonly HeaderProcessor _headerProcessor;
    private readonly OutputFormatter _outputFormatter;
    private DataProcessor? _dataProcessor;

    private bool _processingColumnHeaders = true;

    public TableSummatorProcessor(TextWriter outputWriter, string targetColumnName)
    {
        _headerProcessor = new HeaderProcessor(targetColumnName);
        _outputFormatter = new OutputFormatter(outputWriter, targetColumnName);
    }

    public void ProcessToken(Token token)
    {
        if (_processingColumnHeaders)
        {
            _headerProcessor.ProcessToken(token);

            if (token.Type == TokenType.EndOfLine)
            {
                _processingColumnHeaders = false;
                if (_headerProcessor.FoundTargetColumn)
                {
                    _dataProcessor = new DataProcessor(_headerProcessor.TargetColumnIndex, _headerProcessor.HeaderColumnCount);
                }
                else
                {
                    throw new NonExistentColumnNameApplicationException();
                }
            }
        }
        else
        {
            _dataProcessor?.ProcessToken(token);
        }
    }

    public void Finish()
    {
        if (_processingColumnHeaders || _dataProcessor == null)
        {
            throw new InvalidFileFormatApplicationException();
        }

        _outputFormatter.WriteOutput(_dataProcessor.Sum);
    }
}
