using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace Expressions;

#nullable enable

public class ExpressionParser
{
    Stack<IExpression>? _operands = new();
    readonly char   NEGATION = '~';
    readonly char[] OPERATORS = { '+', '-', '*', '/', '~' };

    private bool IsOperator(char op) 
    {
        foreach (char o in OPERATORS) 
        {
            if (o == op) return true;
        }
        return false;
    }

    public void BuildExpressionTree(string[] expression)
    {
        for (int i = expression.Length - 1; i >= 0; i--) 
        {
            string token = expression[i];
            if (token.Length == 1 && IsOperator(token[0]))
            {
                char op = token[0];
                if (op == NEGATION && _operands!.Count > 0)
                {
                    var operand = _operands.Pop();
                    _operands.Push(new Constant(operand.Evaluate() * -1));
                    continue;
                }
                else if(_operands!.Count > 1)
                {
                    var left = _operands.Pop();
                    var right = _operands.Pop();

                    _operands.Push(new BinaryOperator(left, right, op));

                    continue;
                }
            }
            else if (int.TryParse(token, out int number))
            {
                _operands!.Push(new Constant(number));
                continue;
            }

            throw new FormatErrorApplicationException();
        }
    }

    public IExpression Parse(string[] expression)
    {
        if (expression.Length < 3) throw new FormatErrorApplicationException();

        BuildExpressionTree(expression);

        if (_operands!.Count == 1) return _operands.Pop();
        _operands.Clear();
        throw new FormatErrorApplicationException();
    }
}
