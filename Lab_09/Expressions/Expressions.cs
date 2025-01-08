using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Expressions;

#nullable enable

public class Constant : IExpression
{
    int _value;

    public Constant(int value)
    {
        _value = value;
    }


    public int Evaluate()
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

    public int Evaluate()
    {
        if (_left == null || _right == null) 
            throw new FormatErrorApplicationException();

        int left = _left!.Evaluate();
        int right = _right!.Evaluate();

        switch (_operator)
        {
            case '+':
                return checked(left + right);

            case '-':
                return checked(left - right);

            case '*':
                return checked(left * right);

            case '/':
                if (right == 0)
                    throw new DivisionErrorApplicationException();
                return checked(left / right);

            default:
                throw new FormatErrorApplicationException();
        }
    }
}