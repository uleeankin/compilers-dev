
using lab2.former.token;
using lab2.utils;

namespace lab2
{
    
    public class Runner
    {
        public static void Main(string[] args)
        {
            Checker.CheckArguments(args);
            new Utility(args[0], args[1], args[2]).Run();
        }
    }
}