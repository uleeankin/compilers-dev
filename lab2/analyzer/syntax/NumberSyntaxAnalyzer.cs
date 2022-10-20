using lab2.utils;

namespace lab2.analyzer.syntax;

public class NumberSyntaxAnalyzer : ISyntaxAnalyzer
{
    public void Analyze(List<string> elements, string element, int index)
    {
        if (!this.GetAdjacentElement(elements, index))
        {
            throw new SyntaxException("Рядом с числом", element, 
                ElementPositionDefiner.GetPosition(elements, index), "операция");
        }
    }
    
    private bool GetAdjacentElement(List<string> elements, int index)
    {
        string nextElement;
        string previousElement;
        
        if (index == 0)
        {
            nextElement = elements[1];
            return this.IsOperationSign(nextElement);
        }

        if (index == elements.Count - 1)
        {
            previousElement = elements[index - 1];
            return this.IsOperationSign(previousElement);
        }
        
        nextElement = elements[index + 1];
        previousElement = elements[index - 1];

        return this.IsOperationSign(nextElement) || this.IsOperationSign(previousElement);

    }

    private bool IsOperationSign(string element)
    {
        return ElementTypeDefiner.DefineType(
            element.Substring(1, element.Length - 2)) 
               == ElementType.OPERATION_SIGN;
    }
}