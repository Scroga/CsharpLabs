using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Excel;

#nullable disable

public record struct Address
{
    public int Column, Row;
    public Address(int column, int row)
    {
        Column = column; Row = row;
    }

    public bool IsInvalid()
    {
        return Column < 0 || Row < 0;
    }
}

public class ExcelTable
{
    TableReader _reader = new();
    string[][] _table;
    bool[][] _visited;
    Dictionary<Address, int> _cycleDetector = new();
    HashSet<Address> _cycle = new();

    const int MAX_CYCLE_COUNT = 4;

    const string ERROR = "#ERROR";     // výpočet nelze provést; nejméně jeden operand nemá platnou hodnotu nebo nejde spočítat
    const string DIV0 = "#DIV0";      // při výpočtu došlo k dělení nulou
    const string CYCLE = "#CYCLE";     // nalezen cyklus (všechny buňky na cyklu musí mít tuto hodnotu)
    const string MISSOP = "#MISSOP";    // v zápisu vzorce chybí operátor
    const string FORMULA = "#FORMULA";   // nejméně jeden z operandů je chybně zapsán nebo je ve vzorci jiná chyba než #MISSOP
    const string INVVAL = "#INVVAL";    // pokud nějaká buňka neobsahuje žádný platný vstup (např. obsahuje řetězec)

    const string EMPTY_CELL = "[]";
    readonly char[] BINARY_OPERATORS = new char[4] { '+', '-', '*', '/' };


    public ExcelTable(string fileName)
    {
        _table = _reader.LoadTable(fileName);

        _visited = new bool[_table.Length][];
        for (int i = 0; i < _table.Length; i++)
        {
            _visited[i] = new bool[_table[i].Length];
        }
    }

    public void Evaluate()
    {
        for (int row = 0; row < _table!.Length; row++)
        {
            for (int column = 0; column < _table[row].Length; column++)
            {
                if (!_visited[row][column])
                {
                    EvaluateCell(new Address(column, row));
                }

                MarkCycles();
            }
        }
    }

    public string EvaluateCell(Address address)
    {
        if (_table!.Length <= address.Row || _table[address.Row].Length <= address.Column) return EMPTY_CELL;

        string cell = _table![address.Row][address.Column];
        if (cell[0] == '=')
        {
            _table[address.Row][address.Column] = EvaluateFormula(address);
            return _table[address.Row][address.Column];
        }

        _visited[address.Row][address.Column] = true;

        if ((cell == EMPTY_CELL) || (int.TryParse(cell, out _)) || (cell[0] == '#'))
            return cell;

        _table[address.Row][address.Column] = INVVAL;
        return INVVAL;
    }

    private bool DetectCycle(Address address)
    {
        if (!_cycleDetector.ContainsKey(address))
        {
            _cycleDetector.Add(address, 1);
        }
        else if (_cycleDetector[address] <= MAX_CYCLE_COUNT - 1)
        {
            _cycleDetector[address]++;
        }
        else
        {
            _cycle.Add(address);
            foreach (Address elementAdd in _cycleDetector.Keys)
            {
                if (_cycleDetector[elementAdd] >= _cycleDetector[address])
                {
                    _cycle.Add(elementAdd);
                }
            }
            _cycleDetector.Clear();
            return true;
        }
        return false;
    }

    private void MarkCycles()
    {
        if (_cycle.Count == 0) return;

        foreach (Address address in _cycle)
        {
            _table[address.Row][address.Column] = CYCLE;
        }
        _cycle.Clear();
    }

    private string EvaluateFormula(Address address)
    {
        if (DetectCycle(address)) return ERROR;

        string cell = _table![address.Row][address.Column];

        string[] formula = cell.Substring(1).Split(BINARY_OPERATORS);

        if (formula.Length != 2) return MISSOP;

        string leftOperand = GetOpernad(formula[0]);
        string rightOperand = GetOpernad(formula[1]);

        if (leftOperand == ERROR || rightOperand == ERROR) return FORMULA;
        if (leftOperand[0] == '#' || rightOperand[0] == '#') return ERROR;

        int leftNumber = 0;
        int rightNumber = 0;

        if (leftOperand != EMPTY_CELL)
        {
            if (int.TryParse(leftOperand, out int number)) leftNumber = number;
            else return FORMULA;
        }

        if (rightOperand != EMPTY_CELL)
        {
            if (int.TryParse(rightOperand, out int number)) rightNumber = number;
            else return FORMULA;
        }

        char binOperator = cell[1 + formula[0].Length];

        switch (binOperator)
        {
            case '+':
                return (leftNumber + rightNumber).ToString();

            case '-':
                return (leftNumber - rightNumber).ToString();

            case '*':
                return (leftNumber * rightNumber).ToString();

            case '/':
                if (rightNumber == 0) return DIV0;
                return (leftNumber / rightNumber).ToString();

            default:
                return FORMULA;
        }
    }

    public static Address ParseAddress(string addressOpernad)
    {
        int row = 0;
        int column = 0;

        int i = 0;
        while (i < addressOpernad.Length && char.IsLetter(addressOpernad[i]))
        {
            if (!char.IsUpper(addressOpernad[i])) return new Address(-1, -1);

            column = column * 26 + (addressOpernad[i] - 'A' + 1);
            i++;
        }

        while (i < addressOpernad.Length && char.IsDigit(addressOpernad[i]))
        {
            row = row * 10 + (addressOpernad[i] - '0');
            i++;
        }

        return new Address(column - 1, row - 1);
    }

    private string GetOpernad(string address)
    {
        if (address.Length == 0) return ERROR;
        if (address.Split('!').Length == 2)
        {
            string[] newAddress = address.Split('!');
            if (newAddress.Length != 2) return ERROR;

            ExcelTable newTable;

            try
            {
                newTable = new ExcelTable(newAddress[0]);
            }
            catch
            {
                return ERROR;
            }

            Address operandAdd = ParseAddress(newAddress[1]);
            if (operandAdd.IsInvalid()) return ERROR;

            return newTable.EvaluateCell(operandAdd);
        }
        else
        {
            Address operandAdd = ParseAddress(address);
            if (operandAdd.IsInvalid()) return ERROR;

            return EvaluateCell(operandAdd);
        }
    }

    public string[][] GetTable()
    {
        return _table!;
    }
}
