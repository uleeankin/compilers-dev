using lab2.analyzer.syntax.lexical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.symbol;

namespace lab2.utils
{
    internal class VariableTokenFormer : ITokenFormer, IVariableTokenFormer
    {
        private readonly ILexicalAnalyzer _analyzer;
        private readonly string _definition;
        private readonly string _floatType;
        private readonly string _integerType;
        private List<string> _uniqueVariables = new List<string>();
        private int _variableNumber = 0;

        public VariableTokenFormer()
        {
            _analyzer = new VariableLexicalAnalyzer();
            _definition = "идентификатор с именем";
            _floatType = "вещественного типа";
            _integerType = "целого типа";

        }
        public string Form(string element)
        {
            throw new NotImplementedException();
        }

        public string Form(string element, int position)
        {
            _analyzer.Analyze(element, position);
            _uniqueVariables.Add(element);
            _variableNumber++;
            string type;
            if (element.Contains("[F]") || element.Contains("[f]"))
            {
                type = _floatType;
            }
            else
            {
                type = _integerType;
            }
            return $"<id,{_variableNumber}> - {_definition} {ModifyElement(element)} {type}";
        }
        
        public bool IsUniqueVariable(string element)
        {
            return !(_uniqueVariables.Contains(element));
        }

        private string ModifyElement(string element)
        {
            if (element.Contains("[F]")
                || element.Contains("[f]")
                || element.Contains("[I]")
                || element.Contains("[i]"))
            {
                return element.Substring(0, element.Length - 3);
            }

            return element;
        }
    }
}
