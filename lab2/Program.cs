
using lab2.former.token;
using lab2.utils;

namespace lab2
{

    public class Runner
    {
        public static void Main(string[] args)
        {
            List<string> res = new TokensFormer().FormTokens(new ArithmeticExpressionParser()
                                            .Parse("var1 + (9.5 - 5 * (var2 - 0.6)) / var3"));
            foreach (string token in res)
            {
                Console.WriteLine(token);
            }
        }
    }
}