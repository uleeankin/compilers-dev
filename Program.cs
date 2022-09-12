// See https://aka.ms/new-console-template for more information

namespace lab1
{
    class Utility
    {
        private readonly String[] ITEMS = new String[] { "+", "-", "*", ":" };
        
        private void Generate(string fileName, int lineCount, int minOperandNumber, int maxOperandNumber)
        {
            try
            {
                int operandNumber = new Random().Next(minOperandNumber, maxOperandNumber + 1);
                for (int i = 0; i < lineCount; i++)
                {
                    
                }
            }
            catch (Exception exception)
            {
                
            }
        }

        private void Translate(string inputFileName, string outputFileName) 
        {
            
        }
        
        static void Main(string[] args)
        {
            switch (args[0].ToUpper())
            {
                case "G": new Utility().Generate(args[1], int.Parse(args[2]), 
                                    int.Parse(args[3]), 
                                    int.Parse(args[4]));
                            break;
                case "T": new Utility().Translate(args[1], args[2]);
                            break;
                default: Console.WriteLine("Illegal argument!");
                            break;
                    
            }
        } 
    }    
}