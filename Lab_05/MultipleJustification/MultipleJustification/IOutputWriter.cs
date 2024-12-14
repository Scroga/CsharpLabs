using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace MultipleJustification;

public interface IOutputWriter
{
    public void WriteFromBuffer(int maxLineWidth, List<string> wordBuffer, int _bufferWidth, bool justify = false);
    public void WriteLine(string text);
    public void WriteLine();
}

public class DefaultWriter : IOutputWriter 
{
    TextWriter _writer;
    public DefaultWriter(TextWriter writer) 
    {
        _writer = writer;
    }

    public void WriteFromBuffer(int maxLineWidth, List<string> wordBuffer, int _bufferWidth, bool justify = false)
    {
        int spaces = wordBuffer.Count - 1;

        if (justify && spaces > 0)
        {
            int totalSpaces = maxLineWidth - _bufferWidth;
            int baseSpaceCount = totalSpaces / spaces;
            int extraSpaces = totalSpaces % spaces;

            for (int i = 0; i < spaces; i++)
            {
                wordBuffer[i] += new string(' ', baseSpaceCount + (extraSpaces > 0 ? 1 : 0));
                if (extraSpaces > 0) extraSpaces--;
            }
        }

        for (int i = 0; i < wordBuffer.Count; i++)
        {
            if (i < spaces)
            {
                wordBuffer[i] += ' ';
            }
            _writer.Write(wordBuffer[i]);
        }

        _writer.WriteLine();
    }

    public void WriteLine(string text) 
    {
        _writer.WriteLine(text);
    }
    public void WriteLine()
    {
        _writer.WriteLine();
    }
}

public class HighlightSpacesWriter : IOutputWriter
{
    TextWriter _writer;
    public HighlightSpacesWriter(TextWriter writer)
    {
        _writer = writer;
    }

    public void WriteFromBuffer(int maxLineWidth, List<string> wordBuffer, int _bufferWidth, bool justify = false)
    {
        int spaces = wordBuffer.Count - 1;

        if (justify && spaces > 0)
        {
            int totalSpaces = maxLineWidth - _bufferWidth;
            int baseSpaceCount = totalSpaces / spaces;
            int extraSpaces = totalSpaces % spaces;

            for (int i = 0; i < spaces; i++)
            {
                wordBuffer[i] += new string('.', baseSpaceCount + (extraSpaces > 0 ? 1 : 0));
                if (extraSpaces > 0) extraSpaces--;
            }
        }

        for (int i = 0; i < wordBuffer.Count; i++)
        {
            if (i < spaces)
            {
                wordBuffer[i] += '.';
            }
            _writer.Write(wordBuffer[i]);
        }

        _writer.Write("<-");
        _writer.WriteLine();
    }

    public void WriteLine(string text)
    {
        _writer.WriteLine(text + "<-");
    }
    public void WriteLine()
    {
        _writer.WriteLine("<-");
    }
}