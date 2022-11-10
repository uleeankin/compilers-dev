namespace lab2.utils;

public class OperationSign
{

    private string _sign;
    private string _name;
    private int _operandsNumber;
    
    public string Sign { get; }
    public string Name { get; }
    public int OperandsNumber { get; }

    public OperationSign(string sign, string name, int operandsNumber)
    {
        _sign = sign;
        _name = name;
        _operandsNumber = operandsNumber;
    }
}