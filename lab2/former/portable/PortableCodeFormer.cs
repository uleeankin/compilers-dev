using System.Text;
using lab2.utils;

namespace lab2.former.portable;

public class PortableCodeFormer
{
    private const string Add = "add";
    private const string Mul = "mul";
    private const string Sub = "sub";
    private const string Div = "div";
    private const string I2F = "i2f";
    private readonly List<OperationSign> _operations = new List<OperationSign>();
    private readonly List<PortableCode> _portableCode = new List<PortableCode>();
    private int _lastVariableIndex;
    private int _additionalVariableIndex = 1;
    

    public PortableCodeFormer()
    {
        _operations.Add(new OperationSign("+",Add, 2));
        _operations.Add(new OperationSign("*",Mul, 2));
        _operations.Add(new OperationSign("-",Sub, 2));
        _operations.Add(new OperationSign("/",Div, 2));
        _operations.Add(new OperationSign("i2f",I2F, 1));
    }

    public List<PortableCode> Form(List<Element> postfixExpression)
    {
        _lastVariableIndex = GetLastVariableIndex(postfixExpression);
        for (int i = 0; i < postfixExpression.Count; i++)
        {
            Element element = postfixExpression[i];
            if (element.Type == ElementType.OPERATION_SIGN)
            {
                _portableCode.Add(GetPortableCode(i, postfixExpression));
            }
        }

        return _portableCode;
    }

    private PortableCode GetPortableCode(int operationPosition,
                                    List<Element> source)
    {
        int operandsNumber = GetOperandNumberByOperationToken(source[operationPosition].Token);
        Element operationToken = new Element(GetOperationNameByOperationToken(source[operandsNumber].Token),
            "", -1, ElementType.OPERATION_SIGN);
        List<Element> operands = new List<Element>();
        for (int i = operationPosition - 1; 
             i <= operationPosition - operandsNumber; i++)
        {
            operands.Add(source[i]);
        }

        if (operands.Count == 2)
        {
            return new PortableCode(operationToken, 
                GetAdditionalVariable(source, operationPosition), 
                operands[0], operands[1]);    
        }
        else
        {

            return new PortableCode(operationToken, 
                GetAdditionalVariable(source, operationPosition), 
                operands[0]);
        }
    }

    private int GetOperandNumberByOperationToken(string token)
    {
        foreach (OperationSign sign in _operations)
        {
            if (token.Substring(1, token.Length - 2) == sign.Sign)
            {
                return sign.OperandsNumber;
            }
        }

        throw new ArgumentException("Unknown operation sign");
    }

    private string GetOperationNameByOperationToken(string token)
    {
        foreach (OperationSign sign in _operations)
        {
            if (token.Substring(1, token.Length - 2) == sign.Sign)
            {
                return sign.Name;
            }
        }

        throw new ArgumentException("Unknown operation sign");
    }
    
    private int GetLastVariableIndex(List<Element> elements)
    {
        int max = 0;
        foreach (Element element in elements)
        {
            if (element.Type == ElementType.FLOAT_VARIABLE
                || element.Type == ElementType.INTEGER_VARIABLE)
            {
                int current = Int32.Parse(element.Token.Split(", ")[2].Substring(0,
                    element.Token.Split(", ")[2].Length - 2));
                max = current >= max ? current : max;
            }
        }
        return max;
    }

    private Element GetAdditionalVariable(List<Element> source, int operationIndex)
    {
        _lastVariableIndex++;
        string token = "<id, " + _lastVariableIndex + ">";
        _additionalVariableIndex++;
        string definition = "T" + _additionalVariableIndex;
        ElementType type = ElementType.VARIABLE;
        if (source[operationIndex - 1].Type == ElementType.FLOAT
            || source[operationIndex - 1].Type == ElementType.FLOAT_VARIABLE
            || source[operationIndex - 1].Type == ElementType.INT_TO_FLOAT)
        {
            type = ElementType.FLOAT_VARIABLE;
        }
        
        if (source[operationIndex - 1].Type == ElementType.INTEGER
            || source[operationIndex - 1].Type == ElementType.INTEGER_VARIABLE)
        {
            type = ElementType.INTEGER_VARIABLE;
        }

        return new Element(token, definition, -1, type);
    }

}