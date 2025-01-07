using System;
using System.Numerics;

namespace Expressions;

public enum ValueType 
{
    Valid = 0, DividError, OverflowError, FormatError
}

public class Value
{
    int          _value;
    ValueType    _type;

    public Value(int value)
    {
        _value = value;
        _type = ValueType.Valid;
    }

    public Value(ValueType type)
    {
        _value = 0;
        _type = type;
    }

    private static bool AreValid(Value left, Value right)
    {
        return left._type == ValueType.Valid && right._type == ValueType.Valid;
    }

    public static Value operator +(Value left, Value right) 
    {
        if(AreValid(left, right)) return left._type != ValueType.Valid ? left : right;

        return new Value(left._value + right._value);
    }
    public static Value operator -(Value left, Value right)
    {
        if (AreValid(left, right)) return left._type != ValueType.Valid ? left : right;

        return new Value(left._value - right._value);
    }
    public static Value operator *(Value left, Value right)
    {
        if (AreValid(left, right)) return left._type != ValueType.Valid ? left : right;

        return new Value(left._value * right._value);
    }
    public static Value operator /(Value left, Value right)
    {
        if (AreValid(left, right)) return left._type != ValueType.Valid ? left : right;
        if (right._value == 0) return new Value(ValueType.DividError);

        return new Value(left._value * right._value);
    }

    public string GetAsString() 
    {
        if(_type == ValueType.Valid) return _value.ToString();

        switch (_type) 
        {
            case ValueType.DividError:
                return "Divide Error";

            case ValueType.OverflowError:
                return "Overflow Error";

            case ValueType.FormatError:
                return "Format Error";
            default:
                throw new Exception("Unknown ValueType");
        }
    }
}
