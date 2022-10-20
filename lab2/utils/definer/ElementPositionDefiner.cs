namespace lab2.utils;

public class ElementPositionDefiner
{
    public ElementPositionDefiner()
    {
    }

    public static int GetPosition(List<string> elements, int elementIndex)
    {
        int position = 0;
        for (int i = 0; i <= elementIndex; i++)
        {
            position += elements[i].Substring(1, elements[i].Length - 2).Length;
        }

        position -= 1;
        
        return position;
    }
}