namespace lab2.syntax_tree;

public class TreeOutputFormer
{
    private readonly string _indent = "    ";
    private readonly string _branchIndent = "|---";
    public List<string> FormOutputTree(Tree tree, string indent)
    {
        List<string> result = new List<string>();
        string prefix = "";

        if (tree.ParentNode != null)
        {
            prefix = _branchIndent;
        }
        
        result.Add(prefix + tree.Value.Token);

        if (tree.LeftNode != null)
        {
            List<string> leftTree = FormOutputTree(tree.LeftNode, _indent);
            foreach (string node in leftTree) 
            {
                result.Add(indent + node);
            }
        }
        
        if (tree.RightNode != null)
        {
            List<string> rightTree = FormOutputTree(tree.RightNode, _indent);
            foreach (string node in rightTree) 
            {
                result.Add(indent + node);
            }
        }
        
        return result;
    }
}