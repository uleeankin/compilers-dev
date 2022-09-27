using lab2.utils;
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
                switch(ElementTypeDefiner.define(element))
                {
                    case ElementType.BRACKET:
                        result.Add(bracketTokenFormer.Form(element));
                        break;
                    case ElementType.OPERATION_SIGN:
                        result.Add(operationSignsTokenFormer.Form(element));
                        break;
                    case ElementType.INTEGER:
                        result.Add(integerTokenFormer.Form(element));
                        break;
                    case ElementType.FLOAT:
                        result.Add(floatTokenFormer.Form(element));
                        break;
                    case ElementType.VARIABLE:
                        result.Add(variableTokenFormer.Form(element));
                        break;
                    default:
                        throw new Exception("неизвестный элемент");
                }
            }

            return result;
        }

    }
}
