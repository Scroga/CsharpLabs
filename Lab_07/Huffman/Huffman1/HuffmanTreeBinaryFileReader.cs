using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public class HuffmanTreeBinaryFileReader : IHuffmanTreeReader
{
    FileStream _reader;
    public List<byte> Data { get; } = new List<byte>();
    public HuffmanTreeBinaryFileReader(FileStream reader)
    {
        _reader = reader;
        Init();
    }

    void Init() 
    {
        const int bufferSize = 1024;
        byte[] buffer = new byte[bufferSize];

        int bytesRead = 0;
        while ((bytesRead = _reader.Read(buffer, 0, bufferSize)) > 0)
        {
            for (int i = 0; i < bytesRead; i++)
            {
                Data.Add(buffer[i]);
            }
        }
        _reader.Dispose();
    }

    public Dictionary<byte,long> GetSymbolsDict()
    {
        var dict = new Dictionary<byte, long>();

        foreach(var symbol in Data)
        {
            if (dict.ContainsKey(symbol))
            {
                dict[symbol]++;
            }
            else
            {
                dict.Add(symbol, 1);
            }
        }
        return dict;
    }
}
