using System;
using System.IO;
using System.Numerics;
using System.Collections.Generic;

#nullable enable

namespace MultipleJustification;

public class LineJustifier : ITokenProcessor 
{
    IOutputWriter       _outputWriter;
    readonly int        _maxLineWidth;
    List<string>        _wordBuffer = new List<string>();
    int                 _bufferWidth = 0;

    bool _wasPreviousLongWord       = false;
    bool _wasPreviousEndOfParagraph = false;


    public LineJustifier(IOutputWriter outputWriter, int maxLineWidth) 
    {
        _outputWriter = outputWriter;
        _maxLineWidth = maxLineWidth;
    }

    public void ProcessToken(Token token) 
    {
        if (_wasPreviousEndOfParagraph) 
        {
            _outputWriter.WriteLine();
            _wasPreviousEndOfParagraph = false;
        }

        if (token.Type == TokenType.EndOfWord && token.Word != null)
        {
            if (_bufferWidth + token.Word.Length + (_wordBuffer.Count > 0 ? 1 : 0) <= _maxLineWidth)
            {
                _wordBuffer.Add(token.Word);
                _bufferWidth += token.Word.Length + (_wordBuffer.Count > 1 ? 1 : 0);
                _wasPreviousLongWord = false;
            }
            else
            {
                if (_wordBuffer.Count != 0)
                {
                    _outputWriter.WriteFromBuffer(_maxLineWidth, _wordBuffer, _bufferWidth, justify: true);
                   _wordBuffer.Clear();
                   _bufferWidth = 0;

                }

                if (token.Word.Length >= _maxLineWidth)
                {
                    _outputWriter.WriteLine(token.Word);
                    _wasPreviousLongWord = true;
                }
                else
                {
                    _wordBuffer.Add(token.Word);
                    _bufferWidth += token.Word.Length;
                    _wasPreviousLongWord = false;
                }
            }
        }
        else if (token.Type == TokenType.EndOfParagraph)
        {
            if (_wordBuffer.Count == 0 && _wasPreviousLongWord)
            {
                _outputWriter.WriteLine();
            }
            else if (_wordBuffer.Count > 0)
            {
                _outputWriter.WriteFromBuffer(_maxLineWidth, _wordBuffer, _bufferWidth);
                _wordBuffer.Clear();
                _bufferWidth = 0;
                _wasPreviousEndOfParagraph = true;
            }
        }
    }
    public void WriteResult()
    {
        if (_wordBuffer.Count > 0)
        {
            _outputWriter.WriteFromBuffer(_maxLineWidth, _wordBuffer, _bufferWidth);
            _wordBuffer.Clear();
            _bufferWidth = 0;
        }
    }
}