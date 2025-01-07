using Expressions;
using System.Net.Http.Headers;
using System.Reflection.Metadata;

namespace Expressions;

public class ExpressionsTests
{
    [Fact]
    public void ConstantEvaluationTest_Number()
    {
        var constant = new Constant(1);

        Assert.Equal("1", constant.Evaluate().GetAsString());
    }

    [Fact]
    public void ConstantEvaluationTest_FormatError()
    {
        var constant = new Constant(ValueType.FormatError);

        Assert.Equal("Format Error", constant.Evaluate().GetAsString());
    }

    [Fact]
    public void BinaryOperatorEvaluationTest_ValidOperands() 
    {
        var left = new Constant(2);
        var right = new Constant(2);
        var binaryOp = new BinaryOperator(left, right, '+');

        Assert.Equal("4", binaryOp.Evaluate().GetAsString());
    }

    [Fact]
    public void BinaryOperatorEvaluationTest_Overflow()
    {
        var left = new Constant(2100000000);
        var right = new Constant(2100000000);
        var binaryOp = new BinaryOperator(left, right, '+');

        Assert.Equal("Overflow Error", binaryOp.Evaluate().GetAsString());
    }
}