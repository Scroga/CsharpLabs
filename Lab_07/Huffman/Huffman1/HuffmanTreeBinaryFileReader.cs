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

    public HuffmanTreeBinaryFileReader(FileStream reader)
    {
        _reader = reader;
    }

    public Dictionary<byte,long> GetSymbolsDict()
    {
        var dict = new Dictionary<byte, long>();
        const int bufferSize = 1024;
        byte[] buffer = new byte[bufferSize];

        int bytesRead = 0;
        while((bytesRead = _reader.Read(buffer, 0, bufferSize)) > 0) 
        {
            for(int i = 0; i < bytesRead; i++) 
            {
                if (dict.ContainsKey(buffer[i]))
                {
                    dict[buffer[i]]++;
                }
                else
                {
                    dict.Add(buffer[i], 1);
                }
            }
        }
        return dict;
    }
}
