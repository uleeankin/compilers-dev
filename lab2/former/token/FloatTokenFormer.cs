using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.analyzer.syntax.lexical;

namespace lab2.utils
{
    internal class FloatTokenFormer : ITokenFormer
    {
        private readonly string _definition;
        private readonly ILexicalAnalyzer _analyzer;

        public FloatTokenFormer()
        {
            _definition = "константа вещественного типа";
            _analyzer = new FloatLexicalAnalyzer();
        }

        public string Form(string element)
        { 
            throw new NotImplementedException();
        }

        public string Form(string element, int position)
        {
            _analyzer.Analyze(element, position);
            return $"<{element}> - {_definition}";
        }
    }
}
