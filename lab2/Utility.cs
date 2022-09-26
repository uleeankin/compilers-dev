using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Utility
    {
        private readonly string INPUT_FILE_NAME;
        private readonly string OUTPUT_TOKENS_FILE_NAME;
        private readonly string OUTPUT_SYMBOLS_FILE_NAME;
        private readonly string INPUT_ARYTHMETIC_EXPRESSION;

        public Utility(string inputFileName,
                        string outputTokensFileName,
                        string outputSymbolsFileName)
        {
            this.INPUT_FILE_NAME = inputFileName;
            this.OUTPUT_TOKENS_FILE_NAME = outputTokensFileName;
            this.OUTPUT_SYMBOLS_FILE_NAME = outputSymbolsFileName;
        }


    }
}
