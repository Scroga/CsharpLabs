using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public class MinNodeList
{
    private LinkedList<HuffmanTreeNode> _nodes = new();

    public int Count => _nodes.Count;

    public void Add(HuffmanTreeNode node)
    {
        if (_nodes.Count == 0)
        {
            _nodes.AddFirst(node);
            return;
        }

        var current = _nodes.First;
        while (current != null && current.Value.Weight <= node.Weight)
        {
            current = current.Next;
        }

        if (current == null)
        {
            _nodes.AddLast(node);
        }
        else
        {
            _nodes.AddBefore(current, node);
        }
    }

    public void RemoveMin()
    {
        if (_nodes.Count == 0) return;
        _nodes.RemoveFirst();
    }

    public HuffmanTreeNode? GetMin()
    {
        return _nodes.First?.Value;
    }

    public HuffmanTreeNode? PeekMin()
    {
        if (_nodes.Count == 0) return null;
        return _nodes.First!.Value;
    }
}
