namespace lab2.utils;

public class TokensListGetter
{
    public static List<string> GetTokensWithDescription(List<Element> elements)
    {
        List<string> result = new List<string>();
        foreach (Element element in elements)
        {
            result.Add(element.Token + " - " + element.Definition);
        }

        return result;
    }
}