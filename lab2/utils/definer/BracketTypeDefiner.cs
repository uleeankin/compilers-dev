namespace lab2.utils;

public class BracketTypeDefiner
{
    public static BracketType Define(string bracket)
    {
        switch (bracket)
        {
            case "(":
                return BracketType.OPENED;
            case ")":
                return BracketType.CLOSED;
            default:
                return BracketType.OPENED;
        }
    }
}