using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman1;

public class MinNodeStack
{
    private Stack<HuffmanTreeNode> _stack = new Stack<HuffmanTreeNode>();
    private Stack<HuffmanTreeNode> _minStack = new Stack<HuffmanTreeNode>();

    public int Count => _stack.Count;

    public void Push(HuffmanTreeNode node)
    {
        if (_minStack.Count == 0 || node.Weight < _minStack.Peek().Weight)
        {
            _minStack.Push(node);
        }
        else if(node.Weight == _minStack.Peek().Weight) 
        {
            if(node.Symbol < _minStack.Peek().Symbol) 
            {
                _minStack.Push(node);
            }
        }
        _stack.Push(node);
    }

    public void Pop()
    {
        if (_stack.Count == 0) return;

        if (_minStack.Peek().Equals(_stack.Peek()))
        {
            _minStack.Pop();
        }
        _stack.Pop();
    }

    public HuffmanTreeNode? GetMin()
    {
        return _minStack.Count == 0 ? null : _minStack.Peek();
    }
}