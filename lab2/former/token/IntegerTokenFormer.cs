using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.utils
{
    internal class IntegerTokenFormer : ITokenFormer
    {
        private readonly string DEFINITION;

        public IntegerTokenFormer()
        {
            DEFINITION = "константа целого типа";
        }

        public string Form(string element)
        {
            return $"<{element}> - {DEFINITION}";
        }

        public string Form(string element, int position)
        {
            throw new NotImplementedException();
        }
    }
}
