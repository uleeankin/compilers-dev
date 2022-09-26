using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class FileAccessorUtil
    {

        public void WriteDataToFile(List<string> expressions, string fileName)
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

        public List<String?> ReadInputDataFromFile(string inputFileName)
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
            catch (Exception)
            {
                throw new FileNotFoundException("File for reading is not found");
            }

            return inputExpressions;
        }
    }

}
