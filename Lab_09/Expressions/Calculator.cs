using System;
using System.IO;
using System.Reflection.PortableExecutable;

namespace Expressions;

#nullable enable

public class Calculator : IProgram
{
    const string DIVIDE_ERROR   = "Divide Error";
    const string OVERFLOW_ERROR = "Overflow Error";
    const string FORMAT_ERROR   = "Format Error";


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

            try
            {
                var result = parser.Parse(expression);
                _output.WriteLine(result.Evaluate());
            }
            catch (DivisionErrorApplicationException)
            {
                _output.WriteLine(DIVIDE_ERROR);
            }
            catch (OverflowException)
            {
                _output.WriteLine(OVERFLOW_ERROR);
            }
            catch (FormatErrorApplicationException)
            {
                _output.Write(FORMAT_ERROR);
            }
        }
    }
}
