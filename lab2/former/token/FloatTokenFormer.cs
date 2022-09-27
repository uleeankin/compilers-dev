using lab2.analyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.former.token
{
    internal class FloatTokenFormer : TokenFormer
    {
        private readonly string DEFINITION;
        private readonly LexicalAnalyzer analyzer;

        public FloatTokenFormer()
        {
            DEFINITION = "константа вещественного типа";
            analyzer = new FloatLexicalAnalyzer();
        }

        public string Form(string element)
        {   
            analyzer.Analyze(element);
            return $"<{element}> - {DEFINITION}";
        }
    }
}
