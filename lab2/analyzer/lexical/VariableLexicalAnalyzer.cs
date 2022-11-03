using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using lab2.utils;

namespace lab2.analyzer.syntax.lexical
{
    internal class VariableLexicalAnalyzer : ILexicalAnalyzer
    {
        public void Analyze(string element, int position)
        {
            if (element.Contains("[F]")
                || element.Contains("[f]")
                || element.Contains("[I]")
                || element.Contains("[i]"))
            {
                element = element.Substring(0, element.Length - 3);
            }
            if (!Regex.IsMatch(element, @"^[a-zA-Z_]."))
            {
                throw new LexicalException($"Идентификатор {element} не может начинаться с цифры", position);
            }
        }
    }
}
