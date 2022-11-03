using lab2.syntax_tree;
using lab2.utils;

namespace lab2.analyzer.semantic;

public class SemanticAnalyzer
{
    public void Analyze(List<Element> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].Type == ElementType.OPERATION_SIGN)
            {
                if (tokens[i].Token.Contains("/"))
                {
                    if (tokens[i + 1].Type == ElementType.INTEGER
                        || tokens[i + 1].Type == ElementType.FLOAT)
                    {
                        int element = Int32.Parse(tokens[i + 1].Token.Substring(
                            1, tokens[i + 1].Token.Length - 2));
                        if (element == 0)
                        {
                            throw new ArgumentException($"Divide by zero on position {tokens[i + 1].Position}");
                        }
                    }
                }
            }
        }
    }
    
    public void Analyze(Tree tree)
    {
        if (tree == null || tree.RightNode == null)
        {
            return;
        }
        
        if (tree.Value.Type == ElementType.OPERATION_SIGN && tree.Value.Token.Contains("/") &&
            (tree.RightNode.Value.Token.Substring(1, tree.RightNode.Value.Token.Length - 2).Equals("0") ||
             tree.RightNode.Value.Token.Substring(1, tree.RightNode.Value.Token.Length - 2).Equals("0.0")))
        {
            throw new ArgumentException($"Divide by zero on position {tree.RightNode.Value.Position}");
        }

        if (tree.LeftNode != null)
        {
            Analyze(tree.LeftNode);
        }

        if (tree.RightNode != null)
        {
            Analyze(tree.RightNode);
        }
    }
}