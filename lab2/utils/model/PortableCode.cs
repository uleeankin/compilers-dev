namespace lab2.utils;

[Serializable]
public class PortableCode
{
    public Element Operation { get; }

    public Element Result { get; set; }

    public Element FirstOperand { get; }

    public Element SecondOperand { get; set; }

    public PortableCode(Element operation, Element result, Element firstOperand, Element secondOperand = null)
    {
        Operation = operation;
        Result = result;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
    }
}