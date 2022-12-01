
using lab2.analyzer.syntax;
using lab2.utils;

namespace lab2
{
    
    public class Runner
    {
        public static void Main(string[] args)
        {
            Checker.CheckArguments(args);
            switch (args[0].ToUpper())
            {
                case "LEX":
                    new Utility(args[0], args[1], 
                        args[2], args[3]).Run();
                    break;
                case "SYN":
                    new Utility(args[0], args[1], args[2]).Run();
                    break;
                case "SEM":
                    new Utility(args[0], args[1], args[2]).Run();
                    break;
                case "GEN1":
                    if (args[1].ToUpper() == "OPT")
                    {
                        new Utility(args[0], args[1], 
                            args[2], args[3], args[4]).Run();
                    }
                    else
                    {
                        new Utility(args[0], args[1], 
                            args[2], args[3]).Run();    
                    }
                    break;
                case "GEN2":
                    if (args[1].ToUpper() == "OPT")
                    {
                        new Utility(args[0], args[1], 
                            args[2], args[3], args[4]).Run();
                    }
                    else
                    {
                        new Utility(args[0], args[1], 
                            args[2], args[3]).Run();    
                    }
                    break;
            }
        }
    }
}