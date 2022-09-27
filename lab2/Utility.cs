using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using lab2.former.symbol;
using lab2.former.token;
using lab2.utils;

namespace lab2
{
    public class Utility
    {
        private readonly string _inputFileName;
        private readonly string _outputTokensFileName;
        private readonly string _outputSymbolsFileName;
        private string[] _parsedExpression;

        public Utility(string inputFileName,
                        string outputTokensFileName,
                        string outputSymbolsFileName)
        {
            this._inputFileName = inputFileName;
            this._outputTokensFileName = outputTokensFileName;
            this._outputSymbolsFileName = outputSymbolsFileName;
        }

        public void Run()
        {
            _parsedExpression = new ArithmeticExpressionParser().Parse(
                                                        FileAccessorUtil
                                                            .ReadInputDataFromFile(_inputFileName)[0]);
            FileAccessorUtil.WriteDataToFile(new TokensFormer()
                                                        .Form(this._parsedExpression), 
                                                _outputTokensFileName);
            FileAccessorUtil.WriteDataToFile(new SymbolsFormer()
                                                        .Form(this._parsedExpression), 
                                                _outputSymbolsFileName);
        }


    }
}
