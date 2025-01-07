using System.Reflection.PortableExecutable;
using Excel;

namespace ExcelTests;

public class ExcelInputArgsTests
{
    internal class FakeProgram : IProgram
    {
        string[]? _args;
        public FakeProgram(string[] args)
        {
            _args = args;
        }

        public void Run()
        {
            if (_args!.Length != 2) throw new InvalidArgumentsApplicationException();
            TableReader reader = new();
            reader.OpenInputFile(_args[0]);
            TableWriter writer = new(_args[1]);

            reader.Dispose();
            writer.Dispose();
        }
    }

    static void TestsSetup(string[] args, TextWriter errorWriter)
    {
        ApplicationErrorHandler errorHandler = new ApplicationErrorHandler(errorWriter);
        errorHandler.ExicuteProgram(new FakeProgram(args));
    }

    [Fact]
    public void CorretArgsTest()
    {
        StringWriter errorWriter = new();
        string[] args = { "Data/sample.sheet", "Data/temp.eval" };

        TestsSetup(args, errorWriter);
        string expected = "";

        Assert.Equal(expected, errorWriter.ToString());
        if (File.Exists(args[1])) File.Delete(args[1]);
    }

    [Fact]
    public void NoArgs()
    {
        StringWriter errorWriter = new();
        string[] args = { };

        TestsSetup(args, errorWriter);
        string expected = "Argument Error\r\n";

        Assert.Equal(expected, errorWriter.ToString());
    }

    [Fact]
    public void InvalidArgsCount_01()
    {
        StringWriter errorWriter = new();
        string[] args = { "Data/sample.sheet" };

        TestsSetup(args, errorWriter);
        string expected = "Argument Error\r\n";

        Assert.Equal(expected, errorWriter.ToString());
    }

    [Fact]
    public void InvalidArgsCount_02()
    {
        StringWriter errorWriter = new();
        string[] args = { "Data/sample.sheet", "Data/sample.sheet", "Data/sample.sheet" };

        TestsSetup(args, errorWriter);
        string expected = "Argument Error\r\n";

        Assert.Equal(expected, errorWriter.ToString());
    }

    [Fact]
    public void InvalidInputFile()
    {
        StringWriter errorWriter = new();
        string[] args = { "Data/unknown.sheet", "Data/temp.eval" };

        TestsSetup(args, errorWriter);
        string expected = "File Error\r\n";

        Assert.Equal(expected, errorWriter.ToString());
        if (File.Exists(args[1])) File.Delete(args[1]);
    }


}

public class AddressParserTests()
{
    [Fact]
    public void SimpleAddres_01()
    {
        Assert.Equal(new Address(0, 0), ExcelTable.ParseAddress("A1"));
    }

    [Fact]
    public void SimpleAddres_MultipleDigits()
    {
        Assert.Equal(new Address(25, 23122), ExcelTable.ParseAddress("Z23123"));
    }

    [Fact]
    public void SimpleAddres_MultipleLetters()
    {
        Assert.Equal(new Address(18278, 0), ExcelTable.ParseAddress("AAAA1"));
    }

    [Fact]
    public void ComplexAddress()
    {
        Assert.Equal(new Address(109, 2024), ExcelTable.ParseAddress("DF2025"));
    }

    [Fact]
    public void ValidAddress()
    {
        Assert.False(ExcelTable.ParseAddress("DF2025").IsInvalid());
    }
    [Fact]
    public void InvalidAddress_01()
    {
        Assert.True(ExcelTable.ParseAddress("2025DF").IsInvalid());
    }

    [Fact]
    public void InvalidAddress_02()
    {
        Assert.True(ExcelTable.ParseAddress("Df25").IsInvalid());
    }

    [Fact]
    public void InvalidAddress_03()
    {
        Assert.True(ExcelTable.ParseAddress("#ERROR").IsInvalid());
    }
}

public class SimpleProgramTests
{
    static void RunTest(string inputTable, string expectedTable)
    {

        string inputFilePath = Path.GetTempFileName();
        string outputFilePath = Path.GetTempFileName();
        string expectedOutputFilePath = Path.GetTempFileName();

        try
        {
            File.WriteAllText(inputFilePath, inputTable);
            File.WriteAllText(expectedOutputFilePath, expectedTable);
            string[] args = { inputFilePath, outputFilePath };
            var program = new ExcelProgram(args);
            program.Run();

            string actualOutput = File.ReadAllText(outputFilePath);
            string expectedOutput = File.ReadAllText(expectedOutputFilePath);

            Assert.Equal(expectedOutput, actualOutput);
        }
        finally
        {
            File.Delete(inputFilePath);
            File.Delete(outputFilePath);
            File.Delete(expectedOutputFilePath);
        }
    }

    [Fact]
    public void EmptyTable()
    {
        var table = "";

        var expectedTable =
            """

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void OneRow_OneColumn_EmptyCellTable()
    {
        var table =
            """
            []
            """;

        var expectedTable =
            """
            [] 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void OneRow_OneColumn_OneNumberTable()
    {
        var table =
            """
            3
            """;

        var expectedTable =
            """
            3 

            """;
        RunTest(table, expectedTable);
    }


    [Fact]
    public void OneRow_OneColumn_OneNegativeNumberTable()
    {
        var table =
            """
            -3
            """;

        var expectedTable =
            """
            -3 

            """;
        RunTest(table, expectedTable);
    }


    [Fact]
    public void OneRow_ThreeColumns_Table()
    {
        var table =
            """
            3 [] 0
            """;

        var expectedTable =
            """
            3 [] 0 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void OneRowTable_SimpleExpressionTable()
    {
        var table =
            """
            3 [] =A1+A2
            """;

        var expectedTable =
            """
            3 [] 3 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void OneRowTable_NegativeResult_SimpleExpressionTable()
    {
        var table =
            """
            1 5 =A1-B1
            """;

        var expectedTable =
            """
            1 5 -4 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void OneRowTable_NegativeOperand_SimpleExpressionTable()
    {
        var table =
            """
            1 -5 =A1+B1
            """;

        var expectedTable =
            """
            1 -5 -4 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void OneRowTable_UndefCell_SimpleExpressionTable()
    {
        var table =
            """
            1 5 =A1*B10
            """;

        var expectedTable =
            """
            1 5 0 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void OneRowTable_Division_01_SimpleExpressionTable()
    {
        var table =
            """
            1 5 =A1/B1
            """;

        var expectedTable =
            """
            1 5 0 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void OneRowTable_Division_02_SimpleExpressionTable()
    {
        var table =
            """
            4 2 =A1/B1
            """;

        var expectedTable =
            """
            4 2 2 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void SimpleTableTest()
    {
        var table =
            """
            [] 3 =B1*A2
            19 =C1+C2 42
            """;

        var expectedTable =
            """
            [] 3 57 
            19 99 42 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void SimpleTableTest_Fibonacci()
    {
        var table =
            """
            []
            =B2+C2 =C2+D2 =D2+E2 =E2+A3 =A3+B3
            1 0
            """;

        var expectedTable =
            """
            [] 
            8 5 3 2 1 
            1 0 

            """;
        RunTest(table, expectedTable);
    }
}

public class ProgramWithErrorsTests
{
    static void RunTest(string inputTable, string expectedTable)
    {

        string inputFilePath = Path.GetTempFileName();
        string outputFilePath = Path.GetTempFileName();
        string expectedOutputFilePath = Path.GetTempFileName();

        try
        {
            File.WriteAllText(inputFilePath, inputTable);
            File.WriteAllText(expectedOutputFilePath, expectedTable);
            string[] args = { inputFilePath, outputFilePath };
            var program = new ExcelProgram(args);
            program.Run();

            string actualOutput = File.ReadAllText(outputFilePath);
            string expectedOutput = File.ReadAllText(expectedOutputFilePath);

            Assert.Equal(expectedOutput, actualOutput);
        }
        finally
        {
            File.Delete(inputFilePath);
            File.Delete(outputFilePath);
            File.Delete(expectedOutputFilePath);
        }
    }

    [Fact]
    public void OneInvalidCell_SimpleTable()
    {
        var table =
            """
            auto
            """;

        var expectedTable =
            """
            #INVVAL 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void MultipleInvalidCells_Div0_SimpleTable()
    {
        var table =
            """
            auto 
            1 0 
            =A2/B2

            """;

        var expectedTable =
            """
            #INVVAL 
            1 0 
            #DIV0 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void Recursion5Cells_OneError_SimpleTable()
    {
        var table =
            """
            []
            =B2+C2 =C2+D2 =D2+E2 =E2+A3 =A1+B2
            """;

        var expectedTable =
            """
            [] 
            #ERROR #CYCLE #CYCLE #CYCLE #CYCLE 

            """;
        RunTest(table, expectedTable);

    }

    [Fact]
    public void Recursion6Cells_SimpleTable()
    {
        var table =
            """
            []
            =B2+C2 =C2+D2 =D2+E2 =E2+A3 =A1+A2
            """;

        var expectedTable =
            """
            [] 
            #CYCLE #CYCLE #CYCLE #CYCLE #CYCLE 

            """;
        RunTest(table, expectedTable);

    }

    [Fact]
    public void MissedOperator_01_SimpleTable()
    {
        var table =
            """
            [] 3 =B1A2

            """;

        var expectedTable =
            """
            [] 3 #MISSOP 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void MissedOperator_02_SimpleTable()
    {
        var table =
            """
            [] 3 =B1&A2

            """;

        var expectedTable =
            """
            [] 3 #MISSOP 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void Formula_ValidInput_SimpleTable()
    {
        var table =
            """
            [] 1 =A1+B1

            """;

        var expectedTable =
            """
            [] 1 1 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void FormulaError_NumberOperand_SimpleTable()
    {
        var table =
            """
            [] 1 =A1+2

            """;

        var expectedTable =
            """
            [] 1 #FORMULA 

            """;
        RunTest(table, expectedTable);
    }

    [Fact]
    public void MultipleErrors_SimpleTable()
    {
        var table =
            """
            []
            =B2+C2 =C2+D2 =D2+E2 =E2+A1 =A1+B2
            =A1+C2 =A2+A3 1
            """;

        var expectedTable =
            """
            [] 
            #ERROR #CYCLE #CYCLE #CYCLE #CYCLE 
            #ERROR #FORMULA 1 

            """;
        RunTest(table, expectedTable);

    }

    [Fact]
    public void MultipleInvalidCells_ComplexTable()
    {
        var table =
            """
            [] 3 =B1*A2
            19 =C1+C2 42
            auto
            =B2/A1 =A1-B4 =C2+A4
            =chyba =A1+autobus

            """;

        var expectedTable =
            """
            [] 3 57 
            19 99 42 
            #INVVAL 
            #DIV0 #CYCLE #ERROR 
            #MISSOP #FORMULA 

            """;
        RunTest(table, expectedTable);
    }
}