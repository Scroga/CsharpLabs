using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public sealed record class HuffmanTreeNode
{
    public byte Symbol { get; init; } = 0;
    public long Weight { get; init; } = 0;
    public HuffmanTreeNode? Left { get; init; } = null;
    public HuffmanTreeNode? Right { get; init; } = null;
    public long CreationTime { get; }

    private static long counter = 0;

    public HuffmanTreeNode(byte symbol = 0, long weight = 0)
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
        Symbol = 0;
        CreationTime = counter++;
    }

    public bool IsLeaf() 
    {
        return Left == null;
    }

    public int CompareTo(HuffmanTreeNode other) 
    {
        if (this.Weight != other.Weight) return this.Weight.CompareTo(other.Weight);
        if (this.IsLeaf() && other.IsLeaf()) return this.Symbol.CompareTo(other.Symbol);
        if (this.IsLeaf() != other.IsLeaf()) return this.IsLeaf() ? -1 : 1;

        return this.CreationTime.CompareTo(other.CreationTime);
    }
}