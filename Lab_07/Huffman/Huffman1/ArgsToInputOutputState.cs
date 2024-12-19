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
        public FileStream? OutputWriter { get; private set; } = null;
        const string OutputFileType = ".huff";

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
            string outputFileName = inputFileName + OutputFileType;
            try
            {
                OutputWriter = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
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
