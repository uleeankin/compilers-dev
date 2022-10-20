using lab2.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab2.utils
{
    public class TokensFormer
    {

        private readonly ITokenFormer _bracketTokenFormer;
        private readonly ITokenFormer _operationSignsTokenFormer;
        private readonly ITokenFormer _integerTokenFormer;
        private readonly ITokenFormer _floatTokenFormer;
        private readonly VariableTokenFormer _variableTokenFormer;
        private int _currentPosition = 0;

        public TokensFormer()
        {
            _bracketTokenFormer = new BracketTokenFormer();
            _operationSignsTokenFormer = new OperationSignTokenFormer();
            _integerTokenFormer = new IntegerTokenFormer();
            _floatTokenFormer = new FloatTokenFormer();
            _variableTokenFormer = new VariableTokenFormer();
        }

        public List<string> Form(string[] elements)
        {
            List<string> result = new List<string>();

            foreach (string element in elements)
            {
                switch(ElementTypeDefiner.Define(element))
                {
                    case ElementType.BRACKET:
                        result.Add(_bracketTokenFormer.Form(element));
                        break;
                    case ElementType.OPERATION_SIGN:
                        result.Add(_operationSignsTokenFormer.Form(element));
                        _currentPosition += 2;
                        break;
                    case ElementType.INTEGER:
                        result.Add(_integerTokenFormer.Form(element));
                        break;
                    case ElementType.FLOAT:
                        result.Add(_floatTokenFormer.Form(element, _currentPosition));
                        break;
                    case ElementType.VARIABLE:
                        if (_variableTokenFormer.IsUniqueVariable(element))
                        {
                            result.Add(_variableTokenFormer.Form(element, _currentPosition));    
                        }
                        break;
                    default:
                        throw new LexicalException($"Неизвестный элемент {element}", _currentPosition);
                }

                _currentPosition += element.Length;
            }

            return result;
        }

    }
}
