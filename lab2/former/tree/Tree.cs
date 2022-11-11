using System.Net.Sockets;
using lab2.utils;

namespace lab2.syntax_tree;

public class Tree
{
    private Tree _parentNode;
    private Tree _leftNode;
    private Tree _rightNode;
    private Element? _value;

    public Element? Value
    {
        get => _value;
        set => _value = value;
    }

    public Tree ParentNode
    {
        get => _parentNode;
        set => _parentNode = value;
    }

    public Tree LeftNode
    {
        get => _leftNode;
        set => _leftNode = value;
    }

    public Tree RightNode
    {
        get => _rightNode;
        set => _rightNode = value;
    }

    public Tree()
    {
        _value = new Element("", "", -1, ElementType.UNKNOWN);
        _parentNode = null;
        _leftNode = null;
        _rightNode = null;
    }
    
    public Tree(Tree parentNode)
    {
        _value = new Element("", "", -1, ElementType.UNKNOWN);
        _parentNode = parentNode;
        _leftNode = null;
        _rightNode = null;
    }
}