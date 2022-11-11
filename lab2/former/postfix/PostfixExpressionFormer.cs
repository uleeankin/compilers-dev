using lab2.utils;

namespace lab2.former.symbol;

public class PostfixExpressionFormer
{
    public List<Element> ConvertInfixToPostfix(List<Element> tokens)
    {
        List<Element> postfixList = new List<Element>();
        Stack<Element> postfix = new Stack<Element>();
        Dictionary<string, int> operatorsWithPriority 
                                    = GetOperatorPriorities();
        tokens = DeleteBrackets(tokens);
        
        foreach (Element token in tokens)
        {
            if (token.Type == ElementType.FLOAT_VARIABLE
                || token.Type == ElementType.INTEGER_VARIABLE
                || token.Type == ElementType.FLOAT
                || token.Type == ElementType.INTEGER)
            {
                postfixList.Add(token);
            } else if(token.Type == ElementType.BRACKET)
            {
                if (BracketTypeDefiner.Define(
                        GetElementFromToken(token.Token)) == BracketType.OPENED)
                {
                    postfix.Push(token);
                }
                else
                {
                    Element topToken = postfix.Pop();
                    while (BracketTypeDefiner.Define(
                               GetElementFromToken(token.Token)) != BracketType.OPENED)
                    {
                        postfixList.Add(topToken);
                        if (postfix.Count != 0)
                        {
                            topToken = postfix.Pop();   
                        }
                    }
                }
            }
            else
            {
                while (postfix.Count != 0
                       && CompareOperationSignsPriorities(postfix.Peek(),
                           token, operatorsWithPriority))
                {
                    postfixList.Add(postfix.Pop());
                }
                postfix.Push(token);
            }
        }

        while (postfix.Count != 0)
        {
            postfixList.Add(postfix.Pop());
        }
        
        return postfixList;
    }

    private Dictionary<string, int> GetOperatorPriorities()
    {
        Dictionary<string, int> operatorsWithPriority 
            = new Dictionary<string, int>();
        
        operatorsWithPriority.Add("*", 3);
        operatorsWithPriority.Add("/", 3);
        operatorsWithPriority.Add("i2f", 2);
        operatorsWithPriority.Add("+", 1);
        operatorsWithPriority.Add("-", 1);
        operatorsWithPriority.Add("(", 0);

        return operatorsWithPriority;
    }

    private string GetElementFromToken(string token)
    {
        return token.Substring(1, token.Length - 2);
    }

    private bool CompareOperationSignsPriorities(Element? firstToken,
                                                Element? secondToken,
                                                Dictionary<string, int> priorities)
    {
        return priorities[GetElementFromToken(firstToken.Token)]
               >= priorities[GetElementFromToken(secondToken.Token)];
    }

    private List<Element> DeleteBrackets(List<Element> source)
    {
        int bracketsNumber = 0;
        foreach (Element element in source)
        {
            if (element.Type == ElementType.BRACKET)
            {
                bracketsNumber++;
            }
        }

        if (bracketsNumber == 2
            && source[0].Type == ElementType.BRACKET
            && source[^1].Type == ElementType.BRACKET)
        {
            source.RemoveAt(source.Count - 1);
            source.RemoveAt(0);
        }

        return source;
    }
}