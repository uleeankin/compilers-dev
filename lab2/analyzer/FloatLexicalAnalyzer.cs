using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using lab2.utils;

namespace lab2.analyzer
{
    internal class FloatLexicalAnalyzer : ILexicalAnalyzer
    {
        public void Analyze(string element, int position)
        {
            if (!Regex.IsMatch(element, @"^[0-9]+[.]?[0-9]+$"))
            {
                throw new LexicalException($"Неправильно задана константа {element}", position);
            }
        }
    }
}
