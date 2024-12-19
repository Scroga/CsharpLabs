using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public class HuffmanTreeBinaryFileWriter : IHuffmanTreeWriter
{
    FileStream _writer;


    readonly byte[] Header = { 0x7B, 0x68, 0x75, 0x7C, 0x6D, 0x7D, 0x66, 0x66 };

    public HuffmanTreeBinaryFileWriter(FileStream writer)
    {
        _writer = writer;
    }

    void WriteHeader() 
    {
        foreach (byte b in Header)
        {  
            _writer.WriteByte(b);
        }
    }

    void WriteEncodedTree(HuffmanTreeNode root)
    {
        foreach (byte b in TreeEncoder.Encode(root!)) 
        {
            _writer.WriteByte(b);
        }
    }

    void WriteEncodedData(HuffmanTreeNode root, List<byte> data) 
    {
        foreach (byte b in DataEncoder.Encode(root!, data)) 
        {
            _writer.WriteByte(b);
        }
    }

    public void WriteFile(HuffmanTreeNode root, List<byte>? data = null) 
    {
        WriteHeader();
        WriteEncodedTree(root);
        WriteEncodedData(root, data!);
    }
}
