
namespace lab2
{

    public class Runner
    {
        public static void Main(string[] args)
        {
            new ArithmeticExpressionParser().Parse("var1 + (9.5 - 5 * (var2 - 0.6)) / var3");
        }
    }
}