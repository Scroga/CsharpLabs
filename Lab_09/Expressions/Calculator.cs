using System;
using System.IO;
using System.Reflection.PortableExecutable;

namespace Expressions;

public class Calculator : IProgram
{
    TextWriter _output;
    TextReader _input;
    public Calculator(TextWriter output, TextReader input) 
    {
        _output = output;
        _input = input;
    }

    public void Run() 
    {
        while (true)
        {
            string? line = _input.ReadLine();
            if (line == null) break;

            var parser = new ExpressionParser();

            string[] expression = line.Split(' ');

            var result = parser.Parse(expression);

            if (result != null)
            {
                var value = result.Evaluate();
                if (value != null)
                {
                    _output.WriteLine(value.GetAsString());
                }
            }
        }
    }
}
