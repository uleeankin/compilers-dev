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

    public Tree Form(List<Element> elements)
    {
        new SyntaxAnalyzer().Analyze(elements);
        return FormTree(elements);
    }

    private Tree FormTree(List<Element> elements)
    {
        Tree currentTree = new Tree();
        List<Element> tokens = AddBrackets(elements);

        foreach (Element token in tokens)
        {
            if (token.Type == ElementType.BRACKET)
            {
                if (BracketTypeDefiner.Define(
                        token.Token.Substring(1, token.Token.Length - 2)) 
                    == BracketType.OPENED)
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
            else if (token.Type == ElementType.INTEGER
                     || token.Type == ElementType.FLOAT
                     || token.Type == ElementType.FLOAT_VARIABLE
                     || token.Type == ElementType.INTEGER_VARIABLE)
            {
                currentTree.Value = token;
                if (currentTree.ParentNode == null)
                {
                    currentTree.ParentNode = new Tree();
                    currentTree.ParentNode.LeftNode = currentTree;
                }
                currentTree = currentTree.ParentNode;

            } else if (token.Type 
                       == ElementType.OPERATION_SIGN)
            {
                if (currentTree.Value.Token != "")
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

    private List<Element> AddBrackets(List<Element> elements)
    {
        if (!elements[0].Token.Contains("(") 
            && !elements[elements.Count - 1].Token.Contains(")"))
        {
            elements.Insert(0, new Element(_firstBracket, "", 0, ElementType.BRACKET));
            elements.Add(new Element(_lastBracket, "", 0, ElementType.BRACKET));
        }

        return elements;
    }
    
}