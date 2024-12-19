using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1
{
    public class HuffmanTreePrefixWriter : IHuffmanTreeWriter
    {
        TextWriter _writer;
        public HuffmanTreePrefixWriter(TextWriter writer)
        {
            _writer = writer;
        }

        public void WriteFile(HuffmanTreeNode? root, List<byte>? data = null) 
        {
            var buffer = new List<string>();
            TraversePrefixOrder(root, buffer);
            _writer.Write(string.Join(" ", buffer));
        }

        public void TraversePrefixOrder(HuffmanTreeNode? root, List<string> buffer) 
        {
            if (root == null) return;

            if (!root.IsLeaf())
            {
                buffer.Add($"{root.Weight}");
                TraversePrefixOrder(root.Left, buffer);
                TraversePrefixOrder(root.Right, buffer);
            }
            else 
            {
                buffer.Add($"*{root.Symbol}:{root.Weight}");
            }
        }
    }
}
