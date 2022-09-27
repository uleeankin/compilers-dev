using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.former.token
{
    internal class BracketTokenFormer : TokenFormer
    {
        private Dictionary<string, string> BRACKETS;

        internal BracketTokenFormer()
        {
            BRACKETS = new Dictionary<string, string>()
            {
                { "(", "открывающая скобка" },
                { ")", "закрывающая скобка" }
            };
        }

        public string Form(string element)
        {
            return $"<{element}> - {BRACKETS[element]}";
        }
    }
}
