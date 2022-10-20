using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.utils
{
    public class FileAccessorUtil
    {

        public static void WriteDataToFile(List<string> expressions, string fileName)
        {
            try
            {
                StreamWriter file = new StreamWriter(fileName, false);
                foreach (string expression in expressions)
                {
                    file.WriteLine(expression);
                }
                file.Close();
            }
            catch (Exception)
            {
                throw new Exception("Error writing to file!");
            }

        }

        public static List<string> ReadInputDataFromFile(string inputFileName)
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
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("File for reading is not found");
            }

            if (inputExpressions.Count == 0)
            {
                throw new DataException($"File is empty! File {inputFileName} must contain expression");
            }

            return inputExpressions;
        }
    }

}
