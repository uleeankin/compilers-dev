using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class ArithmeticExpressionParser
    {
        public string[] Parse(string sourseExpression)
        {
            return this.AddBrackets(sourseExpression.Split(" ")).ToArray<string>();
        }

        private List<string> AddBrackets(string[] parsedExpression)
        {
            List<string> result = new List<string>();

            foreach (string elem in parsedExpression)
            {
                if (elem.Contains('(') || elem.Contains(')')) {
                    foreach (string sep in this.SeparateBrackets(elem))
                    {
                        result.Add(sep);
                    }
                } else
                {
                    result.Add(elem);
                }
            }

            return result;
        }

        private List<string> SeparateBrackets(string stringWithBrackets)
        {
            List<string> result = new List<string>();
            int i = 0;

            while (i < stringWithBrackets.Length)
            {
                if (stringWithBrackets[i] == '(' 
                    || stringWithBrackets[i] == ')')
                {
                    result.Add(stringWithBrackets[i].ToString());
                } else
                {
                    result.Add(this.SeparateOperatorsAndOperands(stringWithBrackets, ref i));
                    
                }
                i++;
            }

            return result;
        }

        private string SeparateOperatorsAndOperands(string stringWithBrackets, ref int index)
        {
            StringBuilder notBracket = new StringBuilder("");
            while (stringWithBrackets[index] != '('
                    && stringWithBrackets[index] != ')')
            {
                notBracket.Append(stringWithBrackets[index]);
                index++;
                if (index == stringWithBrackets.Length)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
            index--;
            return notBracket.ToString();
        }
    }
}
