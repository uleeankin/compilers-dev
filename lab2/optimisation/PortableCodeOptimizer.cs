using lab2.utils;

namespace lab2.optimisation;

public class PortableCodeOptimizer
{
    public PortableCodeOptimizer()
    {
    }

    public List<PortableCode> Optimize(List<PortableCode> portableCode)
    {

        Element firstVar = portableCode[0].Result;
        Element secondVar = portableCode[1].Result;

        for (int i = 2; i < portableCode.Count; i++)
        {
            if (i % 2 == 0)
            {
                portableCode[i].Result = firstVar;
                portableCode[i].SecondOperand = secondVar;
            }
            else
            {
                portableCode[i].Result = secondVar;
                portableCode[i].SecondOperand = firstVar;
            }
        }
        
        return portableCode;
    }
}