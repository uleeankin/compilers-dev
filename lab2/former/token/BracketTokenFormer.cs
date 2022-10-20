using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.utils
{
    internal class BracketTokenFormer : ITokenFormer
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

        public string Form(string element, int position)
        {
            throw new NotImplementedException();
        }
    }
}
