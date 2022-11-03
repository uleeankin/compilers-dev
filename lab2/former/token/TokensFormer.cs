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

        public List<Element> Form(string[] elements)
        {
            List<Element> result = new List<Element>();
            string el;

            foreach (string element in elements)
            {
                switch(ElementTypeDefiner.Define(element))
                {
                    case ElementType.BRACKET:
                        el = _bracketTokenFormer.Form(element);
                        result.Add(new Element(TokensParser.GetToken(el), 
                                            TokensParser.GetDescription(el),
                                                    _currentPosition, ElementType.BRACKET));
                        break;
                    case ElementType.OPERATION_SIGN:
                        el = _operationSignsTokenFormer.Form(element);
                        result.Add(new Element(TokensParser.GetToken(el),
                            TokensParser.GetDescription(el),
                            _currentPosition, ElementType.OPERATION_SIGN));
                        _currentPosition += 2;
                        break;
                    case ElementType.INTEGER:
                        el = _integerTokenFormer.Form(element);
                        result.Add(new Element(TokensParser.GetToken(el),
                            TokensParser.GetDescription(el),
                            _currentPosition, ElementType.INTEGER));
                        break;
                    case ElementType.FLOAT:
                        el = _floatTokenFormer.Form(element);
                        result.Add(new Element(TokensParser.GetToken(el),
                            TokensParser.GetDescription(el),
                            _currentPosition, ElementType.FLOAT));
                        break;
                    case ElementType.FLOAT_VARIABLE:
                        if (_variableTokenFormer.IsUniqueVariable(element))
                        {
                            el = _variableTokenFormer.Form(element, _currentPosition);
                            result.Add(new Element(TokensParser.GetToken(el),
                                TokensParser.GetDescription(el),
                                _currentPosition, ElementType.FLOAT_VARIABLE));    
                        }
                        break;
                    case ElementType.INTEGER_VARIABLE:
                        if (_variableTokenFormer.IsUniqueVariable(element))
                        {
                            el = _variableTokenFormer.Form(element, _currentPosition);
                            result.Add(new Element(TokensParser.GetToken(el),
                                TokensParser.GetDescription(el),
                                _currentPosition, ElementType.INTEGER_VARIABLE));    
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
