namespace lab2.utils;

public class OperationSign
{
    public string Sign { get; set; }
    public string Name { get; set; }
    public int OperandsNumber { get; set; }

    public OperationSign(string sign, string name, int operandsNumber)
    {
        Name = name;
        Sign = sign;
        OperandsNumber = operandsNumber;
    }
}