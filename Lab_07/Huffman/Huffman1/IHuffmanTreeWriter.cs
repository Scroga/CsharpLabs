using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public interface IHuffmanTreeWriter
{
    void WriteFile(HuffmanTreeNode root, List<byte>? data = null);
}
