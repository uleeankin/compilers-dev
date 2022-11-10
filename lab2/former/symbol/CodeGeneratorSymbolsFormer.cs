using lab2.utils;

namespace lab2.former.postfix;

public class PostfixGeneratorSymbolsFormer
{
    public List<string> Form(List<Element> elements)
    {
        List<string> variables = new List<string>();
        foreach (Element element in elements)
        {
            if (element.Type == ElementType.FLOAT_VARIABLE
                || element.Type == ElementType.INTEGER_VARIABLE)
            {
                variables.Add(element.Token + " - " 
                                            + GetVariableNameByDescription(element.Definition)
                                            + ", " + GetTypeDescription(element.Type));
            }
        }

        return variables;
    }

    private string GetTypeDescription(ElementType type)
    {
        switch (type)
        {
            case ElementType.FLOAT_VARIABLE:
                return "float";
            case ElementType.INTEGER_VARIABLE:
                return "integer";
        }

        return "";
    }

    private string GetVariableNameByDescription(string description)
    {
        return description.Split(" ")[3];
    }
}