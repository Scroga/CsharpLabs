using Expressions;
using System.Net.Http.Headers;
using System.Reflection.Metadata;

namespace Expressions;

public class ProgramTests()
{
    static void SetupTest(string expression, StringWriter writer)
    {
        StringReader reader = new(expression);

        var calculator = new Calculator(writer, reader);

        calculator.Run();
    }

    [Fact]
    public void SimpleExpression()
    {
        StringWriter writer = new();
        string expression = "+ ~ 1 3";
        string expected = "2\r\n";

        SetupTest(expression, writer);

        Assert.Equal(expected, writer.ToString());
    }

    [Fact]
    public void ComplexExpression()
    {
        StringWriter writer = new();
        string expression = "/ + - 5 2 * 2 + 3 3 ~ 2";
        string expected = "-7\r\n";

        SetupTest(expression, writer);

        Assert.Equal(expected, writer.ToString());
    }

    [Fact]
    public void OverflowError_InvalidExpression()
    {
        StringWriter writer = new();
        string expression = "- - 2000000000 2100000000 2100000000";
        string expected = "Overflow Error\r\n";

        SetupTest(expression, writer);

        Assert.Equal(expected, writer.ToString());
    }

    [Fact]
    public void DivideError_InvalidExpression()
    {
        StringWriter writer = new();
        string expression = "/ 100 - + 10 10 20";
        string expected = "Divide Error\r\n";

        SetupTest(expression, writer);

        Assert.Equal(expected, writer.ToString());
    }

    [Fact]
    public void FormatError_InvalidExpression()
    {
        StringWriter writer = new();
        string expression = "+ 1 2 3";
        string expected = "Format Error";

        SetupTest(expression, writer);

        Assert.Equal(expected, writer.ToString());
    }

    [Fact]
    public void FormatError_InvalidOperand_InvalidExpression()
    {
        StringWriter writer = new();
        string expression = "- 2000000000 4000000000";
        string expected = "Format Error";

        SetupTest(expression, writer);

        Assert.Equal(expected, writer.ToString());
    }
}