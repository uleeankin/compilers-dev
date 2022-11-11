using lab2.syntax_tree;
using lab2.utils;

namespace lab2.analyzer.semantic;

public class SemanticTreeModifier
{

    private Tree _tree;

    public SemanticTreeModifier(Tree tree)
    {
        this._tree = tree;
    }

    public Tree Modify()
    {
        if (IsContainsFloat(_tree))
        {
            ModifyTree(_tree);    
        }
        return _tree;
    }
    
    private void ModifyTree(Tree tree)
    {
        
        if (tree == null)
        {
            return;
        }

        if (tree.Value.Type == ElementType.OPERATION_SIGN)
        {
            ConvertLeftNodeType(tree);
            ConvertRightNodeType(tree);
        }

        ModifyTree(tree.LeftNode);
        ModifyTree(tree.RightNode);
    }

    private void ConvertRightNodeType(Tree tree)
    {
        if (tree.RightNode != null && (tree.RightNode.Value.Type == ElementType.INTEGER 
                                       || tree.RightNode.Value.Type == ElementType.INTEGER_VARIABLE))
        {
            Tree temp = tree.RightNode;
            Element? element = new Element("Int2Float", "", -1, ElementType.INT_TO_FLOAT);
                
            Tree modifiedTree = new Tree();
            tree.RightNode = modifiedTree;
            temp.ParentNode = modifiedTree;
            modifiedTree.RightNode = temp;
                
            modifiedTree.Value = element;
            modifiedTree.ParentNode = tree;
        }
    }

    private void ConvertLeftNodeType(Tree tree)
    {
        if (tree.LeftNode != null && (tree.LeftNode.Value.Type == ElementType.INTEGER 
                                      || tree.LeftNode.Value.Type == ElementType.INTEGER_VARIABLE))
        {
            Tree temp = tree.LeftNode;
            Element? element = new Element("Int2Float", "", -1, ElementType.INT_TO_FLOAT);
                
            Tree modifiedTree = new Tree();
            tree.LeftNode = modifiedTree;
            temp.ParentNode = modifiedTree;
            modifiedTree.LeftNode = temp;
                
            modifiedTree.Value = element;
            modifiedTree.ParentNode = tree;
        }
    }

    private bool IsContainsFloat(Tree tree)
    {
        bool returned = false;
        if (tree.LeftNode != null && 
            (tree.LeftNode.Value.Type == ElementType.FLOAT_VARIABLE
            || tree.LeftNode.Value.Type == ElementType.FLOAT))
        {
            return true;
        }
        
        if (tree.RightNode != null && 
            (tree.RightNode.Value.Type == ElementType.FLOAT_VARIABLE
            || tree.RightNode.Value.Type == ElementType.FLOAT))
        {
            return true;
        }

        if (tree.LeftNode != null)
        {
            returned = IsContainsFloat(tree.LeftNode);
        }

        if (returned)
        {
            return true;
        }

        if (tree.RightNode != null)
        {
            returned = IsContainsFloat(tree.RightNode);
        }

        return returned;
    }
}