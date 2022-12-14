using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.utils
{
    internal class OperationSignTokenFormer : ITokenFormer
    {
        private Dictionary<string, string> OPERATION_SIGNS;

        internal OperationSignTokenFormer()
        {
            OPERATION_SIGNS = new Dictionary<string, string>()
            {
                { "+", "операция сложения" },
                { "-", "операция вычитания" },
                { "*", "операция умножения" },
                { "/", "операция деления" }
            };
        }

        public string Form(string element)
        {
            return $"<{element}> - {OPERATION_SIGNS[element]}";
        }

        public string Form(string element, int position)
        {
            throw new NotImplementedException();
        }
    }
}
