namespace lab2.utils;

public class PortableCode
{
    public Element Operation { get; }

    public Element Result { get; }

    public Element FirstOperand { get; }

    public Element SecondOperand { get; }

    public PortableCode(Element operation, Element result, Element firstOperand, Element secondOperand = null)
    {
        Operation = operation;
        Result = result;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
    }
}