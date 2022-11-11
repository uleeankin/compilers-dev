using lab2.utils;

namespace lab2.former.portable;

public class PortableCodeToStringConverter
{
    public static List<string> Convert(List<PortableCode> lines)
    {
        List<string> result = new List<string>();
        foreach (PortableCode line in lines)
        {
            string resultLine = line.Operation.Token + " "
                                               + line.Result.Token + " "
                                               + line.FirstOperand.Token;
            if (line.SecondOperand != null)
            {
                resultLine = resultLine + " " + line.SecondOperand.Token;
            }
            result.Add(resultLine);
        }

        return result;
    }
}