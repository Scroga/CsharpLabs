using System;

namespace Huffman1;

internal class Program
{
    static void Main(string[] args)
    {
        var applicationErrorHandle = new ApplicationErrorHandler(Console.Out);
        applicationErrorHandle.ExecuteProgram(new HuffmanTreeProgram(), args);
    }
}