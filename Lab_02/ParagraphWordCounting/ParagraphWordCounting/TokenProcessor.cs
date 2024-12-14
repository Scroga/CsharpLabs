using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagraphWordCounting;
public interface ITokenProcessor
{
    void ProcessToken(Token token);
    void PrintResult();
}

public class ParagraphWordProcessor : ITokenProcessor
{
    private TextWriter _writer;
    private int _counter;

    public ParagraphWordProcessor(TextWriter writer)
    {
        _writer = writer;
    }

    public void ProcessToken(Token token)
    {
        if (token.Type == TokenType.EndOfWord)
        {
            _counter++;
        }
        else if (token.Type == TokenType.EndOfParagraph)
        {
            if (_counter > 0)
            {
                _writer.WriteLine(_counter);
                _counter = 0;
            }
        }
    }

    public void PrintResult()
    {
        if (_counter > 0)
        {
            _writer.WriteLine(_counter);
            _counter = 0;
        }
        Console.WriteLine(_writer.ToString());
    }
}