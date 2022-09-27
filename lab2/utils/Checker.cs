using System.Text.RegularExpressions;

namespace lab2.utils
{
    public class Checker
    {
        public static void CheckArguments(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("You must input 3 arguments:\n" +
                                            "1) file with source expression;\n" +
                                            "2) file name for tokens;\n" +
                                            "3) file name for symbols.");
            }
            foreach (string arg in args)
            {
                if (!Regex.IsMatch(arg, @"(.txt)$"))
                {
                    throw new ArgumentException("All arguments must be format files .txt!");
                }
            }
        }
    }
}

