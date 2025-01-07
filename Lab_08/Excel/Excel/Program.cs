using System;

namespace Excel;

internal class Program 
{
    static void Main(string[] args) 
    {
        ApplicationErrorHandler errorHandler = new ApplicationErrorHandler(Console.Out);
        errorHandler.ExicuteProgram(new ExcelProgram(args));
    }
}