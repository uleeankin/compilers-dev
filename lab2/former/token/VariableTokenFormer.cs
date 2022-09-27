using lab2.analyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.former.token
{
    internal class VariableTokenFormer : TokenFormer
    {
        private readonly LexicalAnalyzer analyzer;
        private readonly string DEFINITION;
        private int variableNumber = 0;

        public VariableTokenFormer()
        {
            analyzer = new VariableLexicalAnalyzer();
            DEFINITION = "идентификатор с именем";
        }
        public string Form(string element)
        {
            analyzer.Analyze(element);
            variableNumber++;
            return $"<id,{variableNumber}> - {DEFINITION} {element}";
        }
    }
}
