using System;

namespace Expressions;

internal class Program
{
    static void Main(string[] args)
    {
        var errorHandler = new ApplicationErrorHandler(Console.Error);
        errorHandler.ExecuteProgram(new Calculator(Console.Out, Console.In));
    }
}
