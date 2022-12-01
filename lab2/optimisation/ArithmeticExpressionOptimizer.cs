using lab2.utils;

namespace lab2.optimisation;

public class ArithmeticExpressionOptimiser
{
    public ArithmeticExpressionOptimiser()
    {
    }

    public List<Element> Optimize(List<Element> elements)
    {
        return OptimizeNumbersTypes(OptimizeConstants(OptimizeDefaultOperations(elements)));
    }

    public List<Element> OptimizeConstants(List<Element> expressionElements)
    {
        List<Element> elements = expressionElements;
        Element optimized;
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Type == ElementType.OPERATION_SIGN)
            {
                if (elements[i - 1].Type == ElementType.INTEGER 
                    && elements[i + 1].Type == ElementType.INTEGER)
                {
                    optimized = this.СalculateInteger(elements[i - 1], 
                        elements[i], elements[i + 1]);
                    elements = Delete(i - 1, elements);
                    elements.Insert(i - 1, optimized);
                    i--;
                } else if ((elements[i - 1].Type == ElementType.INTEGER
                            || elements[i - 1].Type == ElementType.FLOAT)
                           && (elements[i + 1].Type == ElementType.INTEGER
                               || elements[i + 1].Type == ElementType.FLOAT))
                {
                    optimized = this.CalculateFloat(elements[i - 1],
                        elements[i], elements[i + 1]);
                    elements = Delete(i - 1, elements);
                    elements.Insert(i - 1, optimized);
                    i--;
                }
            }
        }

        elements = this.DeleteRedundantBrackets(elements);
        
        return elements;
    }

    private List<Element> DeleteRedundantBrackets(List<Element> expression)
    {
        List<Element> elements = expression;

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Type == ElementType.FLOAT || elements[i].Type == ElementType.INTEGER)
            {
                if (elements[i - 1].Type == ElementType.BRACKET
                    && elements[i + 1].Type == ElementType.BRACKET)
                {
                    elements.RemoveAt(i - 1);
                    elements.RemoveAt(i);
                }
            }
        }
        
        return elements;
    }

    private List<Element> Delete(int startIndex, List<Element> elements)
    {
        for (int i = 0; i < 3; i++)
        {
            elements.RemoveAt(startIndex);
        }
        return elements;
    }

    private Element СalculateInteger(Element firstOperand, Element operation, Element secondOperand)
    {
        int first = int.Parse(TokensParser.GetValue(firstOperand.Token));
        int second = int.Parse(TokensParser.GetValue(secondOperand.Token));
        
        return new Element("<" + GetResult(
                TokensParser.GetValue(operation.Token), 
                first, second) + ">", 
            firstOperand.Definition, firstOperand.Position,
            ElementType.INTEGER);
    }
    
    private Element CalculateFloat(Element firstOperand, Element operation, Element secondOperand)
    {
        float first = float.Parse(TokensParser.GetValue(firstOperand.Token));
        float second = float.Parse(TokensParser.GetValue(secondOperand.Token));
        
        return new Element("<" + GetResult(
                                   TokensParser.GetValue(operation.Token), 
                                   first, second) + ">", 
            firstOperand.Definition, firstOperand.Position,
            ElementType.FLOAT);
    }

    private int GetResult(string operation, int firstOperand, int secondOperand)
    {
        switch (operation)
        {
            case "-":
                return firstOperand - secondOperand;
            case "+":
                return firstOperand + secondOperand;
            case "/":
                return firstOperand / secondOperand;
            default:
                return firstOperand * secondOperand;
        }
    }
    
    private float GetResult(string operation, float firstOperand, float secondOperand)
    {
        switch (operation)
        {
            case "-":
                return firstOperand - secondOperand;
            case "+":
                return firstOperand + secondOperand;
            case "/":
                return firstOperand / secondOperand;
            default:
                return firstOperand * secondOperand;
        }
    }
    
    public List<Element> OptimizeNumbersTypes(List<Element> expressionElements)
    {
        List<Element> elements = expressionElements;

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Type == ElementType.INT_TO_FLOAT
                && elements[i + 1].Type == ElementType.INTEGER)
            {
                int value = int.Parse(TokensParser.GetValue(elements[i + 1].Token));
                string newToken = "<" + value + ",0>";
                elements[i + 1].Token = newToken;
                elements[i + 1].Definition = "константа вещественного типа";
                elements[i + 1].Type = ElementType.FLOAT;
                elements.RemoveAt(i);
            }
        }

        return elements;
    }

    public List<Element> OptimizeDefaultOperations(List<Element> expressionElements)
    {
        List<Element> elements = expressionElements;
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Type == ElementType.OPERATION_SIGN)
            {
                if ((elements[i - 1].Type == ElementType.FLOAT_VARIABLE
                     || elements[i - 1].Type == ElementType.INTEGER_VARIABLE)
                    && (elements[i + 1].Type == ElementType.FLOAT
                        || elements[i + 1].Type == ElementType.INTEGER))
                {
                    elements = VarNumberRelation(i, elements[i],
                        elements[i + 1], elements);
                } else if ((elements[i - 1].Type == ElementType.FLOAT
                            || elements[i - 1].Type == ElementType.INTEGER)
                           && (elements[i + 1].Type == ElementType.FLOAT_VARIABLE
                               || elements[i + 1].Type == ElementType.INTEGER_VARIABLE))
                {
                    elements = NumberVarRelation(i, elements[i],
                        elements[i - 1], elements);
                }
            }
        }

        return elements;
    }

    private List<Element> VarNumberRelation(int operationIndex, Element operation, 
        Element num, List<Element> elements)
    {
        float number = float.Parse(TokensParser.GetValue(num.Token));
        string operationValue = TokensParser.GetValue(operation.Token);
        if (number == 0.0)
        {
            if (operationValue == "*")
            {
                elements.RemoveAt(operationIndex - 1);
                elements.RemoveAt(operationIndex - 1);
            }

            if (operationValue == "-" || operationValue == "+")
            {
                elements.RemoveAt(operationIndex);
                elements.RemoveAt(operationIndex);
            }
        } else if (number == 1.0)
        {
            if (operationValue == "*" || operationValue == "/")
            {
                elements.RemoveAt(operationIndex);
                elements.RemoveAt(operationIndex);
            }
        }
        
        return elements;
    }
    
    private List<Element> NumberVarRelation(int operationIndex, Element operation,  
        Element num, List<Element> elements)
    {
        float number = float.Parse(TokensParser.GetValue(num.Token));
        string operationValue = TokensParser.GetValue(operation.Token);
        if (operationValue == "*")
        {
            if (number == 0.0)
            {
                elements.RemoveAt(operationIndex);
                elements.RemoveAt(operationIndex);
            }
            else if (number == 1.0)
            {
                elements.RemoveAt(operationIndex - 1);
                elements.RemoveAt(operationIndex - 1);
            }
        }

        return elements;
    }
    
    
}