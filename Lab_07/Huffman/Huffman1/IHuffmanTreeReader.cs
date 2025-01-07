using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public interface IHuffmanTreeReader
{
    public Span<byte> Data { get; }
    Dictionary<byte, long> GetSymbolsDict();
}
