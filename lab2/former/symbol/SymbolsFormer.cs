using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.utils;

namespace lab2.symbol
{
    public class SymbolsFormer
    {
        public List<string> Form(List<Element> elements)
        {
            List<string> variables = new List<string>();
            foreach (Element element in elements)
            {
                if (element.Type == ElementType.FLOAT_VARIABLE)
                {
                    variables.Add(element.Token + " [вещественный]");
                }
                if (element.Type == ElementType.INTEGER_VARIABLE)
                {
                    variables.Add(element.Token + " [целый]");
                }
            }

            return this.FormSymbols(variables);
        }

        private List<string> FormSymbols(List<string> variables)
        {
            List<string> symbols = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                symbols.Add($"{i + 1} - {variables[i]}");
            }

            return symbols;
        }
    }
}
