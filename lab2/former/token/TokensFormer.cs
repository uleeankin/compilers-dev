using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab2.former.token
{
    public class TokensFormer
    {

        private readonly TokenFormer bracketTokenFormer;
        private readonly TokenFormer operationSignsTokenFormer;
        private readonly TokenFormer integerTokenFormer;
        private readonly TokenFormer floatTokenFormer;
        private readonly TokenFormer variableTokenFormer;

        public TokensFormer()
        {
            bracketTokenFormer = new BracketTokenFormer();
            operationSignsTokenFormer = new OperationSignTokenFormer();
            integerTokenFormer = new IntegerTokenFormer();
            floatTokenFormer = new FloatTokenFormer();
            variableTokenFormer = new VariableTokenFormer();
        }

        public List<string> FormTokens(string[] elements)
        {
            List<string> result = new List<string>();

            foreach (string element in elements)
            {
                if (IsBracket(element))
                {
                    result.Add(bracketTokenFormer.Form(element));
                    continue;
                }

                if (IsOperationSign(element))
                {
                    result.Add(operationSignsTokenFormer.Form(element));
                    continue;
                }

                if (IsInteger(element))
                {
                    result.Add(integerTokenFormer.Form(element));
                    continue;
                }

                if (IsFloat(element))
                {
                    result.Add(floatTokenFormer.Form(element));
                    continue;
                }

                if (IsVariable(element))
                {
                    result.Add(variableTokenFormer.Form(element));
                    continue;
                }

                throw new Exception("Unknown element (do rigth exception)");
            }

            return result;
        }

        private bool IsBracket(string element)
        {
            string[] brackets = { "(", ")" };
            return brackets.Contains(element);
        }

        private bool IsOperationSign(string element)
        {
            string[] operationSigns = { "+", "-", "*", "/" };
            return operationSigns.Contains(element);
        }

        private bool IsInteger(string element)
        {
            return Regex.IsMatch(element, @"^[0-9]+$");
        }

        private bool IsFloat(string element)
        {
            return Regex.IsMatch(element, @"^[0-9.]+$");
        }

        private bool IsVariable(string element)
        {
            return Regex.IsMatch(element, @"^[a-zA-Z0-9_]+$");
        }

    }
}
