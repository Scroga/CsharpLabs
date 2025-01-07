using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel;

public interface ITableReader
{
    public string[][] LoadTable(string fileName);
}
