using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.former.symbol
{
    public class SymbolFormer
    {
        private Dictionary<int, string> variableIdPair;

        public SymbolFormer()
        {
            variableIdPair = new Dictionary<int, string>();
        }

        public void AddVariable(int id, string variable)
        {
            variableIdPair.Add(id, variable);
        }
    }
}
