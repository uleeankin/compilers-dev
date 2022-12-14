namespace lab2.utils;

public class TokensParser
{
    public TokensParser()
    {
        
    }

    public static List<string> GetTokens(List<Element> tokensAndDescriptions)
    {
        List<string> result = new List<string>();

        foreach (Element token in tokensAndDescriptions)
        {
            result.Add(token.Token.Substring(0, token.Token.IndexOf('>') + 1));
        }
        
        return result;
    }

    public static string GetValue(string token)
    {
        return token.Substring(1, token.Length - 2);
    }

    public static string GetToken(string tokenWithDescription)
    {
        return tokenWithDescription.Substring(0, tokenWithDescription.IndexOf('>') + 1);
    }
    
    public static string GetDescription(string tokenWithDescription)
    {
        return tokenWithDescription.Substring(tokenWithDescription.IndexOf('-') + 2);
    }

    public static List<Element> GetAdditionVariablesFromPortableCode(List<PortableCode> portableCodes)
    {
        List<Element> result = new List<Element>();

        foreach (PortableCode portableCode in portableCodes)
        {
            result.Add(portableCode.Result);
        }

        List<Element> vars = new List<Element>();

        foreach (Element element in result)
        {
            if (!GetTokens(vars).Contains(element.Token))
            {
                vars.Add(element);
            }
        }

        return vars;
    }
}