using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MultipleJustification.Legacy;
public class LegacyLineJustifier : ITokenProcessor
{
    readonly TextWriter     _textWriter;
    List<string>            _wordBuffer;
    readonly int            _maxLineWidth;
    int                     _bufferWidth;
    bool                    _wasPreviousLongWord = false;
    bool                    _wasPreviousEndOfParagraph = false;

    public LegacyLineJustifier(TextWriter textWriter, int lineWidth)
    {
        this._textWriter = textWriter;
        _maxLineWidth = lineWidth;
        _wordBuffer = new List<string>();
        _bufferWidth = 0;
    }

    void FromBufferToWriter(bool justify = false)
    {
        int spaces = _wordBuffer.Count - 1;

        if (justify && spaces > 0)
        {
            int totalSpaces = _maxLineWidth - _bufferWidth;
            int baseSpaceCount = totalSpaces / spaces;
            int extraSpaces = totalSpaces % spaces;

            for (int i = 0; i < spaces; i++)
            {
                _wordBuffer[i] += new string(' ', baseSpaceCount + (extraSpaces > 0 ? 1 : 0));
                if (extraSpaces > 0) extraSpaces--;
            }
        }

        for (int i = 0; i < _wordBuffer.Count; i++)
        {
            if (i < spaces)
            {
                _wordBuffer[i] += ' ';
            }
            _textWriter.Write(_wordBuffer[i]);
        }

        _wordBuffer.Clear();
        _bufferWidth = 0;
    }

    public void ProcessToken(Token token)
    {
        if (_wasPreviousEndOfParagraph && token.Type != TokenType.EndOfFile)
        {
            _textWriter.WriteLine();
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
                    FromBufferToWriter(justify: true);
                    _textWriter.WriteLine();
                }

                if (token.Word.Length >= _maxLineWidth)
                {
                    _textWriter.WriteLine(token.Word);
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
                _textWriter.WriteLine();
            }
            else if (_wordBuffer.Count > 0)
            {
                FromBufferToWriter();
                _textWriter.WriteLine();
                _wasPreviousEndOfParagraph = true;
            }
        }
    }

    public void WriteResult()
    {
        if (_wordBuffer.Count > 0)
        {
            FromBufferToWriter();
            _textWriter.WriteLine();
        }
    }
}
