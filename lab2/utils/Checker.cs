using System.Text.RegularExpressions;

namespace lab2.utils
{
    public class Checker
    {
        public static void CheckArguments(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Arguments not found");
            }

            switch (args[0].ToUpper())
            {
                case "LEX":
                    CheckArgumentsInLexMode(args);
                    break;
                case "SYN":
                    CheckArgumentsInSynOrSemOrGenMode(args);
                    break;
                case "SEM":
                    CheckArgumentsInSynOrSemOrGenMode(args);
                    break;
                case "GEN1":
                    CheckArgumentsInSynOrSemOrGenMode(args);
                    break;
                case "GEN2":
                    CheckArgumentsInSynOrSemOrGenMode(args);
                    break;
                default:
                    throw new ArgumentException("Undefined mode!");
            }
        }

        private static void CheckArgumentsInLexMode(string[] args)
        {
            if (args.Length != 4)
            {
                throw new ArgumentException("You must input 4 arguments:\n" +
                                                        "1) mode" + 
                                                        "2) file with source expression;\n" +
                                                        "3) file name for tokens;\n" +
                                                        "4) file name for symbols.");
            }
            for (int i = 1; i < args.Length; i++)
            {
                if (!Regex.IsMatch(args[i], @"(.txt)$"))
                {
                    throw new ArgumentException("All arguments must be format files .txt!");
                }
            }    
        }

        private static void CheckArgumentsInSynOrSemOrGenMode(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("You must input 4 arguments:\n" +
                                            "1) mode" + 
                                            "2) file with source expression;\n" +
                                            "3) file name for tree.");
            }
            for (int i = 1; i < args.Length; i++)
            {
                if (!Regex.IsMatch(args[i], @"(.txt)$"))
                {
                    throw new ArgumentException("All arguments must be format files .txt!");
                }
            }    
        }
    }
}

