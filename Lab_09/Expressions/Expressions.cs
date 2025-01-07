using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Expressions;

public class Constant : IExpression
{
    Value _value;

    public Constant(int number)
    {
        _value = new Value(number);
    }

    public Constant(ValueType type) 
    {
        _value = new Value(type);
    }

    public Constant(Value value)
    { 
        _value = value;
    }

    public Value Evaluate()
    {
        return _value;
    }
}

public class BinaryOperator : IExpression
{
    IExpression? _left;
    IExpression? _right;

    char _operator;

    public BinaryOperator(char op) 
    {
        _operator = op;
    }

    public BinaryOperator(IExpression left, IExpression right, char op)
    {
        _left = left;
        _right = right;
        _operator = op;
    }

    public Value Evaluate()
    {
        if (_left == null || _right == null)
            return new Value(ValueType.FormatError);

        Value left = _left!.Evaluate();
        Value right = _right!.Evaluate();

        try
        {
            switch (_operator)
            {
                case '+':
                    return left + right;

                case '-':
                    return left - right;

                case '*':
                    return left * right;

                case '/':
                    ;
                    return left / right;

                default:
                    return new Value(ValueType.FormatError);
            }
        }
        catch (OverflowException)
        {
            return new Value(ValueType.OverflowError);
        }
    }
}