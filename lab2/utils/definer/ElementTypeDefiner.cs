using lab2.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab2.utils
{
    internal class ElementTypeDefiner
    {
        public static ElementType Define(string element)
        {
            if (IsBracket(element))
            {
                return ElementType.BRACKET;
            }

            if (IsOperationSign(element))
            {
                return ElementType.OPERATION_SIGN;
            }

            if (IsInteger(element))
            {
                return ElementType.INTEGER;
            }

            if (IsFloat(element))
            {
                return ElementType.FLOAT;
            }

            if (IsVariable(element))
            {
                if (element.Contains("[F]") || element.Contains("[f]"))
                {
                    return ElementType.FLOAT_VARIABLE;
                }
                
                return ElementType.INTEGER_VARIABLE;
            }

            return ElementType.UNKNOWN;

        }
        
        public static ElementType DefineType(string element)
        {
            if (IsBracket(element))
            {
                return ElementType.BRACKET;
            }

            if (IsOperationSign(element))
            {
                return ElementType.OPERATION_SIGN;
            }

            if (IsInteger(element) || IsFloat(element))
            {
                return ElementType.NUMBER;
            }

            if (IsVariableToken(element))
            {
                return ElementType.VARIABLE;
            }

            return ElementType.UNKNOWN;

        }

        private static bool IsBracket(string element)
        {
            string[] brackets = { "(", ")" };
            return brackets.Contains(element);
        }

        private static bool IsOperationSign(string element)
        {
            string[] operationSigns = { "+", "-", "*", "/" };
            return operationSigns.Contains(element);
        }

        private static bool IsInteger(string element)
        {
            return Regex.IsMatch(element, @"^[0-9]+$");
        }

        private static bool IsFloat(string element)
        {
            return Regex.IsMatch(element, @"^[0-9.]+$");
        }

        private static bool IsVariable(string element)
        {
            if (element.Contains("[F]")
                || element.Contains("[f]")
                || element.Contains("[I]")
                || element.Contains("[i]"))
            {
                element = element.Substring(0, element.Length - 3);
            }
            return Regex.IsMatch(element, @"^[a-zA-Z0-9_]+$");
        }
        
        private static bool IsVariableToken(string element)
        {
            return element.Contains("id");
        }
    }
}
