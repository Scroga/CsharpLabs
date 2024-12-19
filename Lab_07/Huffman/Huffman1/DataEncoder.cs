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

    public static List<byte> Encode(HuffmanTreeNode root, List<byte> data)
    {
        var leafPaths = GetDataPaths(root);
        var bitStream = new List<bool>();

        foreach (var symbol in data)
        {
            var (path, length) = leafPaths[symbol];
            for (int i = length - 1; i >= 0; i--)
            {
                bitStream.Add(((path >> i) & 1) == 1);
            }
        }

        var byteList = new List<byte>();
        int bitCount = bitStream.Count;

        while (bitStream.Count % 8 != 0)
        {
            bitStream.Add(false);
        }

        for (int i = 0; i < bitStream.Count; i += 8)
        {
            byte b = 0;
            for (int j = 0; j < 8; j++)
            {
                if (bitStream[i + j])
                {
                    b |= (byte)(1 << j);
                }
            }
            byteList.Add(b);
        }

        return byteList;
    }
}
