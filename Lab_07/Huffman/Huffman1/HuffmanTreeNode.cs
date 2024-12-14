using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public record class HuffmanTreeNode
{
    public int Symbol { get; init; } = -1;
    public int Weight { get; set; } = 0;
    public HuffmanTreeNode? Left { get; set; } = null;
    public HuffmanTreeNode? Right { get; set; } = null;
    public long CreationTime { get; }

    private static long counter = 0;

    public HuffmanTreeNode(int symbol = -1, int weight = 0)
    {
        Symbol = symbol;
        Weight = weight;
        CreationTime = counter++;
    }

    public HuffmanTreeNode(HuffmanTreeNode left, HuffmanTreeNode right) 
    {
        Left = left;
        Right = right;
        Weight = left.Weight + right.Weight;
        Symbol = -1;
        CreationTime = counter++;
    }
}