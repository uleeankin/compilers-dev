// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace lab1
{
    class Utility
    {
        private readonly Dictionary<String, String> OPERANDS;
        private readonly Dictionary<int, String> NUMBERS;
        private readonly int MIN_VALUE = 1;
        private readonly int MAX_VALUE = 9;

        public Utility()
        {
            OPERANDS = new Dictionary<string, string>()
            {
                { "+", "плюс" },
                { "-", "минус"},
                { "*", "умножить"},
                { ":", "делить"}
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
            try
            {
                StreamWriter file = new StreamWriter(fileName, false);
                for (int i = 0; i < lineCount; i++)
                {
                    int operandNumber = this.GetRandomNumber(minOperandNumber, maxOperandNumber);
                    string generatedString = this.GenerateArithmeticExpression(operandNumber);
                    file.WriteLine(generatedString);
                }
                file.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("ewjrfherl");
            }
        }

        private String GenerateArithmeticExpression(int operandNumber)
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

        private void Translate(string inputFileName, string outputFileName) 
        {

        }
        
        static void Main(string[] args)
        {
            switch (args[0].ToUpper())
            {
                case "G":
                    new Utility().Generate(args[1], int.Parse(args[2]),
                              int.Parse(args[3]),
                              int.Parse(args[4]));
                    break;
                case "T":
                    new Utility().Translate(args[1], args[2]);
                    break;
                default:
                    new ArgumentException("Illegal argument!");
                    break;

            }
        } 
    }    
}