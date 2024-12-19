using Huffman1;
using System.Data;
using System.Globalization;

namespace Huffman1Tests
{
    public class HuffmanOneTests
    {
        const string FloderName = "huffman1-data/";
        static void Setup(string[] args, StringWriter outputWriter)
        {
            var state = new ArgsToInputOutputState();

            state.CheckArgumentCount(args, 1);
            state.OpenInputFile(args[0]);

            var reader = new HuffmanTreeBinaryFileReader(state.InputReader!);
            var writer = new HuffmanTreePrefixWriter(outputWriter);

            HuffmanTreeProgram.EncodeTree(reader, writer);
        }

        static void RunTest(string fileName)
        {
            string inputFilePath = FloderName + fileName + ".in";
            string outputFilePath = FloderName + fileName + ".out";

            string[] inputArgs = { inputFilePath };

            var outputWriter = new StringWriter();

            Setup(inputArgs, outputWriter);

            string expectedOutput = File.ReadAllText(outputFilePath);

            Assert.Equal(expectedOutput, outputWriter.ToString());
        }

        [Fact]
        public void InputToOutputFile_Simple()
        {
            string fileName = "simple";
            RunTest(fileName);
        }

        [Fact]
        public void InputToOutputFile_Binary()
        {
            string fileName = "binary";
            RunTest(fileName);
        }

        [Fact]
        public void InputToOutputFile_Simple2()
        {
            string fileName = "simple2";
            RunTest(fileName);
        }

        [Fact]
        public void InputToOutputFile_Simple3()
        {
            string fileName = "simple3";
            RunTest(fileName);
        }

        [Fact]
        public void InputToOutputFile_Simple4()
        {
            string fileName = "simple4";
            RunTest(fileName);
        }
    }

    public class HuffmanTwoTests
    {
        const string FloderName = "huffman2-data/";
        static void Setup(string[] args, FileStream outputWriter)
        {
            var state = new ArgsToInputOutputState();

            state.CheckArgumentCount(args, 1);
            state.OpenInputFile(args[0]);

            var reader = new HuffmanTreeBinaryFileReader(state.InputReader!);
            var writer = new HuffmanTreeBinaryFileWriter(outputWriter);

            HuffmanTreeProgram.EncodeTree(reader, writer);
        }

        static void RunTest(string fileName)
        {
            string inputFilePath = FloderName + fileName + ".in";
            string expectedOutputFilePath = FloderName + fileName + ".in.huff";
            string tempOutputFilePath = "temp_" + fileName + ".in.huff";

            string[] inputArgs = { inputFilePath };

            try
            {
                using (var outputWriter = new FileStream(tempOutputFilePath, FileMode.Create, FileAccess.Write))
                {
                    Setup(inputArgs, outputWriter);
                }

                byte[] actualOutput;
                byte[] expectedOutput;

                using (var actualStream = new FileStream(tempOutputFilePath, FileMode.Open, FileAccess.Read))
                {
                    actualOutput = new byte[actualStream.Length];
                    actualStream.Read(actualOutput, 0, (int)actualStream.Length);
                }

                using (var expectedStream = new FileStream(expectedOutputFilePath, FileMode.Open, FileAccess.Read))
                {
                    expectedOutput = new byte[expectedStream.Length];
                    expectedStream.Read(expectedOutput, 0, (int)expectedStream.Length);
                }

                Assert.Equal(expectedOutput, actualOutput);
            }
            finally
            {
                if (File.Exists(tempOutputFilePath))
                {
                    File.Delete(tempOutputFilePath);
                }
            }
        }

        [Fact]
        public void InputToOutputFile_Simple()
        {
            string fileName = "simple";
            RunTest(fileName);
        }

        [Fact]
        public void InputToOutputFile_Binary()
        {
            string fileName = "binary";
            RunTest(fileName);
        }

        [Fact]
        public void InputToOutputFile_Simple2()
        {
            string fileName = "simple2";
            RunTest(fileName);
        }

        [Fact]
        public void InputToOutputFile_Simple3()
        {
            string fileName = "simple3";
            RunTest(fileName);
        }

        [Fact]
        public void InputToOutputFile_Simple4()
        {
            string fileName = "simple4";
            RunTest(fileName);
        }
    }
}