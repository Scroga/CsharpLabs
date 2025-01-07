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
    private FileStream _reader;
    private Memory<byte> _data;

    public HuffmanTreeBinaryFileReader(FileStream reader)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        Init();
    }

    public Span<byte> Data => _data.Span;

    private void Init()
    {
        const int bufferSize = 1024;
        using var memoryStream = new MemoryStream();

        byte[] buffer = new byte[bufferSize];
        int bytesRead;

        while ((bytesRead = _reader.Read(buffer, 0, bufferSize)) > 0)
        {
            memoryStream.Write(buffer, 0, bytesRead);
        }

        _data = new Memory<byte>(memoryStream.ToArray());

        _reader.Dispose();
    }

    public Dictionary<byte, long> GetSymbolsDict()
    {
        var dict = new Dictionary<byte, long>();

        foreach (var symbol in Data)
        {
            if (dict.ContainsKey(symbol))
            {
                dict[symbol]++;
            }
            else
            {
                dict[symbol] = 1;
            }
        }

        return dict;
    }
}
