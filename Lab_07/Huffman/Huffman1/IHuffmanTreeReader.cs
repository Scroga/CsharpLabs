using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public interface IHuffmanTreeReader
{
    List<byte> Data { get; }
    Dictionary<byte, long> GetSymbolsDict();
}
