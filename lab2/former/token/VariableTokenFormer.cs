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
        private List<string> _uniqueVariables = new List<string>();
        private int _variableNumber = 0;

        public VariableTokenFormer()
        {
            _analyzer = new VariableLexicalAnalyzer();
            _definition = "идентификатор с именем";
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
            return $"<id,{_variableNumber}> - {_definition} {element}";
        }
        
        public bool IsUniqueVariable(string element)
        {
            return !(_uniqueVariables.Contains(element));
        }
    }
}
