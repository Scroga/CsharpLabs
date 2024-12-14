using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TextJustification;
public interface ITokenProcessor
{
    void ProcessToken(Token token);
    void PrintResult();
}

public class LegacyLineJustifier : ITokenProcessor
{
    readonly TextWriter textWriter;
    List<string> wordBuffer;
    readonly int maxLineWidth;
    int bufferWidth;
    bool previousWasBigWord = false;
    bool previousWasEndOfPargraph = false;

    public LegacyLineJustifier(TextWriter textWriter, int lineWidth)
    {
        this.textWriter = textWriter;
        this.maxLineWidth = lineWidth;
        this.wordBuffer = new List<string>();
        this.bufferWidth = 0;
    }

    void FromBufferToWriter(bool justify = false)
    {
        int spaces = wordBuffer.Count - 1;

        if (justify && spaces > 0)
        {
            int totalSpaces = maxLineWidth - bufferWidth;
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
            textWriter.Write(wordBuffer[i]);
        }

        wordBuffer.Clear();
        bufferWidth = 0;
    }

    public void ProcessToken(Token token)
    {
        if (previousWasEndOfPargraph && token.Type != TokenType.EndOfFile)
        {
            textWriter.WriteLine();
            previousWasEndOfPargraph = false;
        }

        if (token.Type == TokenType.EndOfWord && token.Word != null)
        {
            if ((bufferWidth + token.Word.Length + (wordBuffer.Count > 0 ? 1 : 0)) <= maxLineWidth)
            {
                wordBuffer.Add(token.Word);
                bufferWidth += token.Word.Length + (wordBuffer.Count > 1 ? 1 : 0);
                previousWasBigWord = false;
            }
            else
            {
                if (wordBuffer.Count != 0)
                {
                    FromBufferToWriter(justify: true);
                    textWriter.WriteLine();
                }

                if (token.Word.Length >= maxLineWidth)
                {
                    textWriter.WriteLine(token.Word);
                    previousWasBigWord = true;
                }
                else
                {
                    wordBuffer.Add(token.Word);
                    bufferWidth += token.Word.Length;
                    previousWasBigWord = false;
                }
            }
        }
        else if (token.Type == TokenType.EndOfParagraph)
        {
            if (wordBuffer.Count == 0 && previousWasBigWord)
            {
                textWriter.WriteLine();
            }
            else if(wordBuffer.Count > 0)
            {
                FromBufferToWriter();
                textWriter.WriteLine();
                previousWasEndOfPargraph = true;
            }
        }
    }

    public void PrintResult()
    {
        if (wordBuffer.Count > 0)
        {
            FromBufferToWriter();
            textWriter.WriteLine();
        }
    }
}
