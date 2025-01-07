using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public class DataEncoder
{
    static Dictionary<byte, (ulong path, int length)> GetDataPaths(HuffmanTreeNode root)
    {
        var leafPaths = new Dictionary<byte, (ulong path, int length)>();

        void DFS(HuffmanTreeNode node, ulong path, int depth)
        {
            if (node == null) return;

            if (node.IsLeaf())
            {
                leafPaths[node.Symbol] = (path, depth);
                return;
            }

            DFS(node.Left!, path << 1, depth + 1);
            DFS(node.Right!, (path << 1) | 1, depth + 1);
        }

        DFS(root, 0, 0);
        return leafPaths;
    }

    public static List<byte> Encode(HuffmanTreeNode root, Span<byte> data)
    {
        var leafPaths = GetDataPaths(root);
        var byteList = new List<byte>();
        byte currentByte = 0;
        int bitPosition = 0;

        foreach (var symbol in data)
        {
            var (path, length) = leafPaths[symbol];
            for (int i = length - 1; i >= 0; i--)
            {
                currentByte |= (byte)(((path >> i) & 1) << bitPosition);
                bitPosition++;

                if (bitPosition == 8)
                {
                    byteList.Add(currentByte);
                    currentByte = 0;
                    bitPosition = 0;
                }
            }
        }

        if (bitPosition > 0)
        {
            byteList.Add(currentByte);
        }

        return byteList;
    }

}
