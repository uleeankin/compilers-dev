using lab2.utils;

namespace lab2.analyzer.syntax;

public class VariableSyntaxAnalyzer : ISyntaxAnalyzer
{
    public void Analyze(List<Element> elements, string element, int index)
    {
        if (!this.GetAdjacentElement(elements, index))
        {
            throw new SyntaxException("Рядом с идентификатором", element, 
                elements[index].Position, "операция");
        }
    }
    
    private bool GetAdjacentElement(List<Element> elements, int index)
    {
        string nextElement;
        string previousElement;
        
        if (index == 0)
        {
            nextElement = elements[1].Token;
            return this.IsOperationSign(nextElement);
        }

        if (index == elements.Count - 1)
        {
            previousElement = elements[index - 1].Token;
            return this.IsOperationSign(previousElement);
        }
        
        nextElement = elements[index + 1].Token;
        previousElement = elements[index - 1].Token;

        return this.IsOperationSign(nextElement) || this.IsOperationSign(previousElement);

    }

    private bool IsOperationSign(string element)
    {
        return ElementTypeDefiner.DefineType(
            element.Substring(1, element.Length - 2)) 
               == ElementType.OPERATION_SIGN;
    }
}