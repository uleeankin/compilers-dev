using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab2.analyzer
{
    internal class VariableLexicalAnalyzer : LexicalAnalyzer
    {
        public void Analyze(string element)
        {
            if (!Regex.IsMatch(element, @"^[a-zA-Z_]."))
            {
                throw new Exception("идентификатор не может начинаться с цифры");
            }
        }
    }
}
