using lab2.utils;

namespace lab2.analyzer.syntax;

public class OperationSignSyntaxAnalyzer : ISyntaxAnalyzer
{
    public void Analyze(List<string> elements, string element, int index)
    {
        if (!this.GetAdjacentElement(elements, index))
        {
            this.GetOperationSignSyntaxException(elements, element, index);
        }
    }
    
    private bool GetAdjacentElement(List<string> elements, int index)
    {
        string nextElement;
        string previousElement;
        
        if (index == 0)
        {
            nextElement = elements[1];
            return this.IsNumberOrVariable(nextElement);
        }

        if (index == elements.Count - 1)
        {
            previousElement = elements[index - 1];
            return this.IsNumberOrVariable(previousElement);
        }
        
        nextElement = elements[index + 1];
        previousElement = elements[index - 1];

        return (this.IsNumberOrVariable(nextElement) 
               || this.IsNumberOrVariable(previousElement))
            || (this.IsBracket(nextElement) && this.IsBracket(previousElement));

    }

    private bool IsNumberOrVariable(string element)
    {
        ElementType type = ElementTypeDefiner.DefineType(element.Substring(1, element.Length - 2));
        return type == ElementType.VARIABLE || type == ElementType.NUMBER;
    }

    private bool IsBracket(string element)
    {
        ElementType type = ElementTypeDefiner.DefineType(element.Substring(1, element.Length - 2));
        return type == ElementType.BRACKET;
    }

    private void GetOperationSignSyntaxException(List<string> elements, string element, int index)
    {
        throw new SyntaxException("У операции", element,
            ElementPositionDefiner.GetPosition(elements, index), "операнд");
    }
}