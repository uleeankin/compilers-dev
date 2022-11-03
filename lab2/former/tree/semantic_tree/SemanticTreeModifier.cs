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
        ModifyTree(_tree);
        return _tree;
    }
    
    private void ModifyTree(Tree tree)
    {
        
        if (tree == null)
        {
            return;
        }

        if (tree.Value.Type == ElementType.OPERATION_SIGN && IsContainsFloat(tree))
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
            Element element = new Element("Int2Float", "", -1, ElementType.INT_TO_FLOAT);
                
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
            Element element = new Element("Int2Float", "", -1, ElementType.INT_TO_FLOAT);
                
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
    
    public List<Element> ModifyExpression(List<Element> tokens)
    {

        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].Type == ElementType.OPERATION_SIGN)
            {
                if (CheckNeighbourElementsType(tokens[i - 1], i)
                    && !CheckNeighbourElementsType(tokens[i + 1], i))
                {
                    if (tokens[i + 1].Type == ElementType.INTEGER)
                    {
                        tokens[i + 1].Type = ElementType.FLOAT;
                    }
                    if (tokens[i + 1].Type == ElementType.INTEGER_VARIABLE)
                    {
                        tokens[i + 1].Type = ElementType.FLOAT_VARIABLE;
                    }
                    tokens.Insert(i + 1, new Element("Int2Float", "", i, ElementType.INT_TO_FLOAT));
                    i++;
                }
                else
                {
                    if (!CheckNeighbourElementsType(tokens[i - 1], i)
                        && CheckNeighbourElementsType(tokens[i + 1], i))
                    {
                        if (tokens[i - 1].Type == ElementType.INTEGER)
                        {
                            tokens[i - 1].Type = ElementType.FLOAT;
                        }
                        if (tokens[i - 1].Type == ElementType.INTEGER_VARIABLE)
                        {
                            tokens[i - 1].Type = ElementType.FLOAT_VARIABLE;
                        }
                        tokens.Insert(i - 1, new Element("Int2Float", "", i, ElementType.INT_TO_FLOAT));    
                    }
                }
            }
        }
        
        return tokens;
    }

    private bool CheckNeighbourElementsType(Element element,
        int operationSignIndex)
    {
        return element.Type == ElementType.FLOAT
               || element.Type == ElementType.FLOAT_VARIABLE;
    }
}