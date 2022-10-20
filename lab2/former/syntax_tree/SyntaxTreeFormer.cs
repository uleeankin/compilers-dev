using lab2.analyzer.syntax;
using lab2.utils;

namespace lab2.syntax_tree;

public class SyntaxTreeFormer
{

    private readonly string _firstBracket = "<(>";
    private readonly string _lastBracket = "<)>";
    
    public SyntaxTreeFormer()
    {
    }

    public Tree Form(List<string> elements)
    {
        new SyntaxAnalyzer().Analyze(elements);
        return FormTree(elements);
    }

    private Tree FormTree(List<string> elements)
    {
        Tree currentTree = new Tree();
        List<string> tokens = AddBrackets(elements);

        foreach (string token in tokens)
        {
            string element = token.Substring(1, token.Length - 2);
            if (ElementTypeDefiner.Define(element) == ElementType.BRACKET)
            {
                if (BracketTypeDefiner.Define(element) == BracketType.OPENED)
                {
                    currentTree.LeftNode = new Tree(currentTree);
                    currentTree = currentTree.LeftNode;
                }
                else
                {
                    if (currentTree.ParentNode != null)
                    {
                        currentTree = currentTree.ParentNode;
                    }
                }
            }
            else if (ElementTypeDefiner.DefineTreeType(element) == ElementType.OPERAND)
            {
                currentTree.Value = token;
                if (currentTree.ParentNode == null)
                {
                    currentTree.ParentNode = new Tree();
                    currentTree.ParentNode.LeftNode = currentTree;
                }
                currentTree = currentTree.ParentNode;
                
            } else if (ElementTypeDefiner.DefineTreeType(element) 
                       == ElementType.OPERATION_SIGN)
            {
                if (currentTree.Value != "")
                {
                    Tree tempRight = currentTree.RightNode;
                    currentTree.RightNode = new Tree(currentTree);
                    currentTree.RightNode.LeftNode = tempRight;
                    currentTree = currentTree.RightNode;
                }

                currentTree.Value = token;
                currentTree.RightNode = new Tree(currentTree);
                currentTree = currentTree.RightNode;

            }
            
        }

        return GetRoot(currentTree);
    }

    private Tree GetRoot(Tree tree)
    {
        if (tree.ParentNode == null)
        {
            return tree;
        }

        return GetRoot(tree.ParentNode);
    }

    private List<string> AddBrackets(List<string> elements)
    {
        if (elements[0] != "(" && elements[elements.Count - 1] != ")")
        {
            elements.Insert(0, _firstBracket);
            elements.Add(_lastBracket);
        }

        return elements;
    }
    
}