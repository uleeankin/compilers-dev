namespace lab2.utils.semantic;

public class SemanticModifier
{
    public List<Element> ModifyExpression(List<Element> tokens)
    {

        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].Type == ElementType.OPERATION_SIGN)
            {
                if (CheckNeighbourElementsType(tokens[i - 1])
                    && !CheckNeighbourElementsType(tokens[i + 1]))
                {
                    tokens.Insert(i + 1, new Element("<i2f>", "IntToFloat", i, ElementType.INT_TO_FLOAT));
                    i++;
                }
                else
                {
                    if (!CheckNeighbourElementsType(tokens[i - 1])
                        && CheckNeighbourElementsType(tokens[i + 1]))
                    {
                        tokens.Insert(i - 1, new Element("<i2f>", "IntToFloat", i, ElementType.INT_TO_FLOAT));
                    }
                }
            }
        }

        return tokens;
    }

    private bool CheckNeighbourElementsType(Element element)
    {
        return element.Type == ElementType.FLOAT
               || element.Type == ElementType.FLOAT_VARIABLE;
    }
}