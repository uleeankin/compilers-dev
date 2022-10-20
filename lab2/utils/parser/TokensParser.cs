namespace lab2.utils;

public class TokensParser
{
    public TokensParser()
    {
        
    }

    public static List<string> GetTokens(List<string> tokensAndDescriptions)
    {
        List<string> result = new List<string>();

        foreach (string token in tokensAndDescriptions)
        {
            result.Add(token.Substring(0, token.IndexOf('>') + 1));
        }
        
        return result;
    }
}