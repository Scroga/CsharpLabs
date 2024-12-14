using System;
using System.IO;
using System.Collections.Generic;
using System.Data;

namespace Huffman1;
public class HuffmanTreeProgram : IProgramCore
{
    public void Run(string[] args)
    {
        var state = new ArgsToInputOutputState();
        try
        {
            state.CheckArgumentCount(args, 1);
            state.OpenInputFile(args[0]);

            var reader = new HuffmanTreeBinaryFileReader(state.InputReader!);
            var writer = new HuffmanTreePrefixWriter(Console.Out);

            BuildHuffmanTree(reader, writer);
        }
        finally
        {
            state.Dispose();
        }
    }

    public static void BuildHuffmanTree(IHuffmanTreeReader reader, IHuffmanTreeWriter writer)
    {
        var symbolsDict = reader.GetSymbolsDict();
        HuffmanTreeNode root = HuffmanCoding(symbolsDict);
        writer.WritePrefixTree(root);
    }

    static HuffmanTreeNode HuffmanCoding(Dictionary<int, int> dict)
    {
        var priorityQueue = new SortedSet<HuffmanTreeNode>(
            Comparer<HuffmanTreeNode>.Create((a, b) =>
            {
                if (a.Weight != b.Weight) return a.Weight.CompareTo(b.Weight);

                bool isLeafA = a.Symbol != -1;
                bool isLeafB = b.Symbol != -1;

                if (isLeafA && isLeafB) return a.Symbol.CompareTo(b.Symbol);
                if (isLeafA != isLeafB) return isLeafA ? -1 : 1;

                return a.CreationTime.CompareTo(b.CreationTime);
            })
        );

        foreach (var item in dict)
        {
            priorityQueue.Add(new HuffmanTreeNode(item.Key, item.Value));
        }

        while (priorityQueue.Count > 1)
        {
            var leftMin = priorityQueue.Min;
            priorityQueue.Remove(leftMin!);

            var rightMin = priorityQueue.Min;
            priorityQueue.Remove(rightMin!);

            var parentNode = new HuffmanTreeNode(leftMin!, rightMin!);
            priorityQueue.Add(parentNode);
        }

        return priorityQueue.Min!;
    }
}