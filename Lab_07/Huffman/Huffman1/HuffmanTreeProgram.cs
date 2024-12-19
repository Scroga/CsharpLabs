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
            state.OpenOutputFile(args[0]);

            var reader = new HuffmanTreeBinaryFileReader(state.InputReader!);
            var writer = new HuffmanTreeBinaryFileWriter(state.OutputWriter!);

            EncodeTree(reader, writer);
        }
        finally 
        {
            state.Dispose();
        }
    }

    public static void EncodeTree(IHuffmanTreeReader reader, IHuffmanTreeWriter writer)
    {
        Dictionary<byte, long> symbolsDict = reader.GetSymbolsDict();
        List<byte> data = reader.Data;

        HuffmanTreeNode root = HuffmanCoding(symbolsDict);
        writer.WriteFile(root, data);
    }

    static HuffmanTreeNode HuffmanCoding(Dictionary<byte, long> dict)
    {
        var priorityQueue = new SortedSet<HuffmanTreeNode>
            (Comparer<HuffmanTreeNode>.Create((a, b) => { return a.CompareTo(b); }));

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