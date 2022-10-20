using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using lab2.symbol;
using lab2.syntax_tree;
using lab2.utils;

namespace lab2
{
    public class Utility
    {
        private readonly string _inputFileName;
        private readonly string _outputTokensFileName;
        private readonly string _outputSymbolsFileName;
        private readonly string _outputTreeFileName;
        private string[] _parsedExpression;
        private List<string> _tokens = new List<string>();
        private Mode _operatingMode;

        public Utility(string operatingMode,
                        string inputFileName,
                        string outputTokensFileName,
                        string outputSymbolsFileName)
        {
            this._inputFileName = inputFileName;
            this._outputTokensFileName = outputTokensFileName;
            this._outputSymbolsFileName = outputSymbolsFileName;
            this._operatingMode = this.ConvertStringToMode(operatingMode);
        }
        
        public Utility(string operatingMode,
            string inputFileName,
            string outputTreeFileName)
        {
            this._inputFileName = inputFileName;
            this._outputTreeFileName = outputTreeFileName;
            this._operatingMode = this.ConvertStringToMode(operatingMode);
        }

        public void Run()
        {
            switch (this._operatingMode)
            {
                case Mode.LEX:
                    this.DoLexicalMode();
                    break;
                case Mode.SYN:
                    this.DoSyntaxMode();
                    break;
                default:
                    throw new ArgumentException("Error mode!");
            }
        }

        private void DoLexicalMode()
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

        private void DoSyntaxMode()
        {
            _parsedExpression = new ArithmeticExpressionParser().Parse(
                FileAccessorUtil
                    .ReadInputDataFromFile(_inputFileName)[0]);
            List<string> tree = new TreeOutputFormer().FormOutputTree(
                new SyntaxTreeFormer().Form(
                    TokensParser.GetTokens(
                        new TokensFormer().Form(_parsedExpression))),
                "");
            FileAccessorUtil.WriteDataToFile(tree, 
                _outputTreeFileName);
        }

        private Mode ConvertStringToMode(string mode)
        {
            switch (mode.ToUpper())
            {
                case "LEX": 
                    return Mode.LEX;
                case "SYN":
                    return Mode.SYN;
                default:
                    throw new ArgumentException("Error mode!");
            }
        }

    }
}
