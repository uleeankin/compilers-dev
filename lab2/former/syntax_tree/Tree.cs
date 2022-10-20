using System.Net.Sockets;
using lab2.utils;

namespace lab2.syntax_tree;

public class Tree
{
    private Tree _parentNode;
    private Tree _leftNode;
    private Tree _rightNode;
    private string _value;

    public string Value
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
        _value = "";
        _parentNode = null;
        _leftNode = null;
        _rightNode = null;
    }
    
    public Tree(Tree parentNode)
    {
        _value = "";
        _parentNode = parentNode;
        _leftNode = null;
        _rightNode = null;
    }
}