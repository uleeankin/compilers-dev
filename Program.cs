// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace lab1
{
    class Utility
    {
        private readonly Dictionary<String, String> OPERANDS;
        private readonly Dictionary<int, String> NUMBERS;
        private readonly int MIN_VALUE = 1;
        private readonly int MAX_VALUE = 9;
        private static readonly string REGEX = "/.*.txt";

        public Utility()
        {
            OPERANDS = new Dictionary<string, string>()
            {
                { "+", "плюс" },
                { "-", "минус"},
                { "*", "умножить на"},
                { ":", "делить на"}
            };

            NUMBERS = new Dictionary<int, string>()
            {
                { 1, "один" },
                { 2, "два" },
                { 3, "три" },
                { 4, "четыре" },
                { 5, "пять" },
                { 6, "шесть" },
                { 7, "семь" },
                { 8, "восемь" },
                { 9, "девять" },
            };
        }
        
        private void Generate(string fileName, int lineCount, int minOperandNumber, int maxOperandNumber)
        {

            if (lineCount <= 0)
            {
                //throw new ArgumentException("The number of generated lines must be positive");
                CompleteProgramExecution("The number of generated lines must be positive");
            }

            this.WriteDataToFile(this.GetArithmeticExpressionsList(lineCount, minOperandNumber, maxOperandNumber), fileName);
        }

        private List<string> GetArithmeticExpressionsList(int expressionsNumber, int minOperandNumber, int maxOperandNumber)
        {
            List<string> expressions = new List<string>();
            try
            {
                for (int i = 0; i < expressionsNumber; i++)
                {
                    int operandNumber = this.GetRandomNumber(minOperandNumber, maxOperandNumber);
                    string generatedExpression = this.GenerateArithmeticExpression(operandNumber);
                    expressions.Add(generatedExpression);
                }
            } catch (Exception)
            {
                //throw new Exception("Failed expressions generation");
                CompleteProgramExecution("Failed expressions generation");
            }
            
            return expressions;
        }

        private string GenerateArithmeticExpression(int operandNumber)
        {
            int randomNumber = this.GetRandomNumber(this.MIN_VALUE, this.MAX_VALUE);
            StringBuilder arithmeticExpression = new StringBuilder(randomNumber.ToString());

            for (int i = 0; i < operandNumber; i++)
            {
                arithmeticExpression.Append($" {this.OPERANDS.ElementAt(this.GetRandomNumber(0, this.OPERANDS.Count - 1)).Key}");
                arithmeticExpression.Append($" {this.GetRandomNumber(this.MIN_VALUE, this.MAX_VALUE)}");
            }

            return arithmeticExpression.ToString();
        }

        private int GetRandomNumber(int minValue, int maxValue)
        {
            return new Random().Next(minValue, maxValue + 1);
        }

        private void WriteDataToFile(List<string> expressions, string fileName)
        {
            try
            {
                StreamWriter file = new StreamWriter(fileName, false);
                foreach (string expression in expressions)
                {
                    file.WriteLine(expression);
                }
                file.Close();
            } catch (Exception)
            {
                //throw new Exception("Error writing to file!");
                CompleteProgramExecution("Error writing to file!");
            }
            
        }

        private void Translate(string inputFileName, string outputFileName) 
        {
            this.WriteDataToFile(this.TranslateIntoVerbalView(this.ReadInputDataFromFile(inputFileName)), outputFileName);
        }

        private List<string> TranslateIntoVerbalView(List<string> arithmeticExpressions)
        {
            List<string> verbalExpressions = new List<string>();

            foreach(string expression in arithmeticExpressions)
            {
                verbalExpressions.Add(TranslateExpressionToVerbalView(expression).Trim());
            }

            return verbalExpressions;
        }

        private string TranslateExpressionToVerbalView(string arithmeticExpression)
        {
            List<string> verbalElements = new List<string>();
            try
            {
                string[] expressionElements = arithmeticExpression.Split(" ");
                foreach (string element in expressionElements)
                {
                    if (OPERANDS.ContainsKey(element))
                    {
                        verbalElements.Add(OPERANDS[element]);
                    }
                    else if (NUMBERS.ContainsKey(int.Parse(element)))
                    {
                        verbalElements.Add(NUMBERS[int.Parse(element)]);
                    }
                }
            } catch (Exception)
            {
                //throw new Exception("Error translation arithmetic expression into verbal representation");
                CompleteProgramExecution("Error translation arithmetic expression into verbal representation");
            }
            

            return FormVerbalExpression(verbalElements);
        }

        private string FormVerbalExpression(List<string> elements)
        {
            StringBuilder verbalExpression = new StringBuilder();
            foreach(string element in elements)
            {
                verbalExpression.Append($"{element} ");
            }
            return verbalExpression.ToString();
        }

        private List<String?> ReadInputDataFromFile(string inputFileName)
        {
            List<string> inputExpressions = new List<string>();

            try
            {
                StreamReader inputFile = new StreamReader(inputFileName);
                string expression = inputFile.ReadLine();
                while (expression != null)
                {
                    inputExpressions.Add(expression);
                    expression = inputFile.ReadLine();
                };

                inputFile.Close();
            } catch (Exception)
            {
                //throw new FileNotFoundException("File for reading is not found");
                CompleteProgramExecution("File for reading is not found");
            }
            
            return inputExpressions;
        }

        private static void CheckGenerationArguments(string[] args)
        {
            if (args.Length != 5)
            {
                /*throw new Exception("There are exactly 5 arguments when generation expressions:\n" +
                    "generation type\n" + 
                    "output file name\n" + 
                    "lines number\n" + 
                    "minimum operands\n" +
                    "maximum operands number");*/
                CompleteProgramExecution("There are exactly 5 arguments when generation expressions:\n" +
                                        "generation type\n" +
                                        "output file name\n" +
                                        "lines number\n" +
                                        "minimum operands\n" +
                                        "maximum operands number");
            }

            if (!Regex.IsMatch(args[1], REGEX))
            {
                //throw new FileLoadException("Output file must be .txt format");
                CompleteProgramExecution("Output file must be .txt format");
            }

            try
            {
                int.Parse(args[2]);
                int.Parse(args[3]);
                int.Parse(args[4]);
            } catch (Exception)
            {
                //throw new ArgumentException("The values of the lines number, the minimum and maximum operands number must be numerical");
                CompleteProgramExecution("The values of the lines number, the minimum and maximum operands number must be numerical");
            }


        }

        private static void CheckTranslationArguments(string[] args)
        {
            if (args.Length != 3)
            {
                /*throw new Exception("There are exactly 3 arguments when translation expressions:\n" +
                    "translation type\n" +
                    "input file name\n" +
                    "output file name");*/
                CompleteProgramExecution("There are exactly 3 arguments when translation expressions:\n" +
                                            "translation type\n" +
                                            "input file name\n" +
                                            "output file name");
            }

            if (!Regex.IsMatch(args[1], REGEX) || !Regex.IsMatch(args[2], REGEX))
            {
                //throw new FileLoadException("Output and input files must be .txt format");
                CompleteProgramExecution("Output and input files must be .txt format");
            }
        }

        private static void CompleteProgramExecution(string message)
        {
            Console.WriteLine("Exception: " + message);
            Process.GetCurrentProcess().Kill();
        }

        public static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                switch (args[0].ToUpper())
                {
                    case "G":
                        CheckGenerationArguments(args);
                        new Utility().Generate(args[1], int.Parse(args[2]),
                                  int.Parse(args[3]),
                                  int.Parse(args[4]));
                        break;
                    case "T":
                        CheckTranslationArguments(args);
                        new Utility().Translate(args[1], args[2]);
                        break;
                    default:
                        throw new ArgumentException("Illegal argument!");
                }
            } else
            {
                throw new ArgumentNullException("Arguments are not found");
            }
            
        } 
    }    
}