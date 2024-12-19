using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

static class TreeEncoder
{
    public static List<byte> Encode(HuffmanTreeNode root)
    {
        var encodedTree = new List<byte>();
        TraversePrefixOrder(root, encodedTree);
        encodedTree.AddRange(new byte[8]);
        return encodedTree;
    }

    static void TraversePrefixOrder(HuffmanTreeNode? root, List<byte> buffer)
    {
        if (root == null) return;

        buffer.AddRange(EncodeNode(root));
        TraversePrefixOrder(root.Left, buffer);
        TraversePrefixOrder(root.Right, buffer);
    }

    static List<byte> EncodeNode(HuffmanTreeNode node)
    {
        var encodedNode = new List<byte>();
        ulong value = 0;

        if (node.IsLeaf())
        {
            value |= 1UL;

            value |= (ulong)node.Symbol << 56;
        }
        value |= ((ulong)node.Weight & 0x7FFFFFFFFFFFFF) << 1;

        encodedNode.AddRange(BitConverter.GetBytes(value));

        return encodedNode;
    }
}
