//using MultipleJustification.FileProcessing;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MultipleJustificationXUnitTests;

//public class ArgsToInputOutputStateTests
//{
//    public class FakeFileProcessing
//    {
//        public StringWriter writer = new StringWriter();

//        public void Run(string[] args)
//        {
//            var state = new ArgsToInputOutputState(args);
//            try
//            {
//                state!.CheckHighlightWhitespaceFunction();
//                state.CheckArgumentCout(state.IsEnabledHighlightWhitespace ? 4 : 3);
//                state.CheckTextWidth(args.Length - 1);
//                state.OpenOutputFile(args.Length - 2);

//                for (int i = 0; i < state!.Args.Length - 2; i++)
//                {
//                    if (i == 0 && state.IsEnabledHighlightWhitespace) continue;

//                    if (state.IsFileAvailable(state.Args[i]))
//                    {
//                        writer.Write(state.Args[i] + ' ');
//                    }
//                }
//            }
//            finally
//            {
//                state!.Dispose();
//            }
//        }
//    }

//    const string tempFileName1 = "file1.in";
//    const string tempFileName2 = "file2.in";
//    const string tempFileName3 = "file3.in";
//    const string invalidFileName = "invalid.in";
//    const string outputFileName = "output.out";

//    private void CreateTempFiles()
//    {
//        File.WriteAllText(tempFileName1, "Bam bam");
//        File.WriteAllText(tempFileName2, "Bim bim");
//        File.WriteAllText(tempFileName3, "Boom");
//    }

//    private void DeleteTempFiles()
//    {
//        if (File.Exists(tempFileName1)) File.Delete(tempFileName1);
//        if (File.Exists(tempFileName2)) File.Delete(tempFileName2);
//        if (File.Exists(tempFileName3)) File.Delete(tempFileName3);
//        if (File.Exists(outputFileName)) File.Delete(outputFileName);
//    }

//    [Fact]
//    public void CheckAvailableFiles_TwoValidFilesInput()
//    {
//        CreateTempFiles();
//        string[] args = { tempFileName1, tempFileName2, invalidFileName, outputFileName, "20" };
//        var fakeFileProcessing = new FakeFileProcessing();
//        string expectedOutput = tempFileName1 + ' ' + tempFileName2 + ' ';

//        fakeFileProcessing.Run(args);

//        Assert.Equal(expectedOutput, fakeFileProcessing.writer.ToString());

//        DeleteTempFiles();
//    }

//    [Fact]
//    public void CheckAvailableFiles_MoreInputFiles()
//    {
//        CreateTempFiles();
//        string[] args = { tempFileName1, tempFileName2, invalidFileName, invalidFileName, tempFileName3, outputFileName, "20" };
//        var fakeFileProcessing = new FakeFileProcessing();
//        string expectedOutput = tempFileName1 + ' ' + tempFileName2 + ' ' + tempFileName3 + ' ';

//        fakeFileProcessing.Run(args);

//        Assert.Equal(expectedOutput, fakeFileProcessing.writer.ToString());

//        DeleteTempFiles();
//    }

//    [Fact]
//    public void CheckHighlightWhitespaceFunction_True()
//    {
//        const string keyWord = "--highlight-spaces";
//        string[] args = { keyWord, outputFileName, "20" };
//        var state = new ArgsToInputOutputState(args);

//        Assert.True(state.CheckHighlightWhitespaceFunction());
//    }

//    [Fact]
//    public void CheckHighlightWhitespaceFunction_False()
//    {
//        string[] args = { outputFileName, "20" };
//        var state = new ArgsToInputOutputState(args);

//        Assert.False(state.CheckHighlightWhitespaceFunction());
//    }

//    [Fact]
//    public void CheckHighlightWhitespaceFunction_Invalid()
//    {
//        const string invalidKeyWord = "-highlight-spaces";
//        string[] args = { invalidKeyWord, outputFileName, "20" };
//        var state = new ArgsToInputOutputState(args);

//        Assert.False(state.CheckHighlightWhitespaceFunction());
//    }
//}

///*
//Assert.Throws<InvalidArgumentsApplicationException>(() =>
//    {
//        state.CheckHighlightWhitespaceFunction();
//    });
//*/
