using lab2.analyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.former.symbol;

namespace lab2.former.token
{
    internal class VariableTokenFormer : ITokenFormer
    {
        private readonly ILexicalAnalyzer _analyzer;
        private readonly string _definition;
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
            _variableNumber++;
            return $"<id,{_variableNumber}> - {_definition} {element}";
        }
    }
}
