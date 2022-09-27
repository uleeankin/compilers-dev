using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab2.analyzer
{
    internal class FloatLexicalAnalyzer : LexicalAnalyzer
    {
        public void Analyze(string element)
        {
            if (!Regex.IsMatch(element, @"^[0-9]+[.]?[0-9]+$"))
            {
                throw new Exception("неправильно задана константа (сделать нормальное исключение)");
            }
        }
    }
}
