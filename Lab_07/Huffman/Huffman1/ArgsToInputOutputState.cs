using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Huffman1
{
    public class ArgsToInputOutputState : IDisposable
    {
        public FileStream? InputReader { get; private set; } = null;
        public TextWriter? OutputWriter { get; private set; } = null;

        public bool CheckArgumentCount(string[] args, int expectedArgCount) 
        {
            if (args.Length == expectedArgCount) 
            {
                return true;
            }
            throw new InvalidArgumentsApplicationException();
        }

        public void OpenInputFile(string fileName) 
        {
            try 
            {
                InputReader = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                throw new FileAccesErrorApplicationException();
            }
        }

        public void OpenOutputFile(string inputFileName)
        {
            string outputFileName = ""; // TODO:

            try
            {
                // TODO:
            }
            catch
            {
                throw new FileAccesErrorApplicationException();
            }
        }

        public void Dispose()
        { 
            InputReader?.Dispose();
            OutputWriter?.Dispose();
        } 
    }
}
