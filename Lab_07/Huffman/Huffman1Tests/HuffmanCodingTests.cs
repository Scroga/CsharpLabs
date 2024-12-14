using Huffman1;
using System.Globalization;

namespace Huffman1Tests
{
    public class UnitTest1
    {
        const string FloderName = "huffman-data/";
        static void RunTest(string[] args, StringWriter outputWriter) 
        {
            var state = new ArgsToInputOutputState();

            state.CheckArgumentCount(args, 1);
            state.OpenInputFile(args[0]);

            var reader = new HuffmanTreeBinaryFileReader(state.InputReader!);
            var writer = new HuffmanTreePrefixWriter(outputWriter);

            HuffmanTreeProgram.BuildHuffmanTree(reader, writer);
        }

        [Fact]
        public void GetSymbolsDict_Simple()
        {
            string fileName = "simple";

            string inputFilePath = FloderName + fileName + ".in";
            string outputFilePath = FloderName + fileName + ".out";

            string[] inputArgs = { inputFilePath };

            var outputWriter = new StringWriter();

            RunTest(inputArgs, outputWriter);

            string expectedOutput = File.ReadAllText(outputFilePath);

            Assert.Equal(expectedOutput, outputWriter.ToString());
        }

        [Fact]
        public void GetSymbolsDict_Binary()
        {
            string fileName = "binary";

            string inputFilePath = FloderName + fileName + ".in";
            string outputFilePath = FloderName + fileName + ".out";

            string[] inputArgs = { inputFilePath };

            var outputWriter = new StringWriter();

            RunTest(inputArgs, outputWriter);

            string expectedOutput = File.ReadAllText(outputFilePath);

            Assert.Equal(expectedOutput, outputWriter.ToString());
        }

        [Fact]
        public void GetSymbolsDict_Simple2()
        {
            string fileName = "simple2";

            string inputFilePath = FloderName + fileName + ".in";
            string outputFilePath = FloderName + fileName + ".out";

            string[] inputArgs = { inputFilePath };

            var outputWriter = new StringWriter();

            RunTest(inputArgs, outputWriter);

            string expectedOutput = File.ReadAllText(outputFilePath);

            Assert.Equal(expectedOutput, outputWriter.ToString());
        }

        [Fact]
        public void GetSymbolsDict_Simple3()
        {
            string fileName = "simple3";

            string inputFilePath = FloderName + fileName + ".in";
            string outputFilePath = FloderName + fileName + ".out";

            string[] inputArgs = { inputFilePath };

            var outputWriter = new StringWriter();

            RunTest(inputArgs, outputWriter);

            string expectedOutput = File.ReadAllText(outputFilePath);

            Assert.Equal(expectedOutput, outputWriter.ToString());
        }

        [Fact]
        public void GetSymbolsDict_Simple4()
        {
            string fileName = "simple4";

            string inputFilePath = FloderName + fileName + ".in";
            string outputFilePath = FloderName + fileName + ".out";

            string[] inputArgs = { inputFilePath };

            var outputWriter = new StringWriter();

            RunTest(inputArgs, outputWriter);

            string expectedOutput = File.ReadAllText(outputFilePath);

            Assert.Equal(expectedOutput, outputWriter.ToString());
        }


    }
}