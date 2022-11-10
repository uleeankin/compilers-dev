using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using lab2.analyzer.semantic;
using lab2.analyzer.syntax;
using lab2.former.portable;
using lab2.former.postfix;
using lab2.symbol;
using lab2.syntax_tree;
using lab2.utils;
using lab2.utils.semantic;

namespace lab2
{
    public class Utility
    {
        private readonly string _inputFileName;
        private readonly string _outputTokensFileName;
        private readonly string _outputSymbolsFileName;
        private readonly string _outputTreeFileName;
        private List<Element> _parsedExpression;
        private readonly Mode _operatingMode;

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
                case Mode.SEM:
                    this.DoSemanticMode();
                    break;
                case Mode.GEN1:
                    this.DoFirstGenerationMode();
                    break;
                case Mode.GEN2:
                    this.DoSecondGenerationMode();
                    break;
                default:
                    throw new ArgumentException("Error mode!");
            }
        }

        private void DoLexicalMode()
        {
            _parsedExpression = new TokensFormer().Form(
                new ArithmeticExpressionParser().Parse(
                            FileAccessorUtil
                                .ReadInputDataFromFile(_inputFileName)[0]));
            FileAccessorUtil.WriteDataToFile(
                TokensListGetter.GetTokensWithDescription(this._parsedExpression), 
                _outputTokensFileName);
            FileAccessorUtil.WriteDataToFile(new SymbolsFormer()
                    .Form(this._parsedExpression), 
                _outputSymbolsFileName);
        }

        private void DoSyntaxMode()
        {
            _parsedExpression = new TokensFormer().Form(
                new ArithmeticExpressionParser().Parse(
                            FileAccessorUtil
                                .ReadInputDataFromFile(_inputFileName)[0]));
            List<string> tree = new TreeOutputFormer().FormOutputTree(
                new SyntaxTreeFormer().Form(
                    _parsedExpression),
                "");
            FileAccessorUtil.WriteDataToFile(tree, 
                _outputTreeFileName);
        }

        private void DoSemanticMode()
        {
            _parsedExpression = new TokensFormer().Form(new ArithmeticExpressionParser().Parse(
                FileAccessorUtil
                    .ReadInputDataFromFile(_inputFileName)[0]));
            
            Tree syntaxTree = new SyntaxTreeFormer().Form(_parsedExpression);
            new SemanticAnalyzer().Analyze(syntaxTree);
            
            List<string> tree = new TreeOutputFormer().FormOutputTree(
                new SemanticTreeModifier(syntaxTree).Modify(),"");
            FileAccessorUtil.WriteDataToFile(tree, 
                _outputTreeFileName);
        }

        private void DoFirstGenerationMode()
        {
            _parsedExpression = new TokensFormer().Form(new ArithmeticExpressionParser().Parse(
                FileAccessorUtil
                    .ReadInputDataFromFile(_inputFileName)[0]));

            new SyntaxTreeFormer().Form(_parsedExpression);
            new SemanticAnalyzer().Analyze(_parsedExpression);
            new SemanticModifier().ModifyExpression(_parsedExpression);
            List<Element> postfixExpression = new PostfixExpressionFormer()
                .ConvertInfixToPostfix(this._parsedExpression);
            TokensParser.GetAdditionVariablesFromPortableCode(
                new PortableCodeFormer().Form(postfixExpression));
//TODO: собрать portableCode в строки и вывести в файл; соединить обычные переменные и дополнительные в один список и вывести в файл
        }

        private void DoSecondGenerationMode()
        {
            _parsedExpression = new TokensFormer().Form(new ArithmeticExpressionParser().Parse(
                FileAccessorUtil
                    .ReadInputDataFromFile(_inputFileName)[0]));
            
            new SyntaxAnalyzer().Analyze(_parsedExpression);
            new SemanticAnalyzer().Analyze(_parsedExpression);

            FileAccessorUtil.WriteDataToFile(
                new PostfixGeneratorSymbolsFormer()
                             .Form(this._parsedExpression), 
                         _outputSymbolsFileName);
            new SemanticModifier().ModifyExpression(_parsedExpression);
            List<string> tokens = new List<string>
            {
                string.Join(" ", TokensListGetter.GetTokens(
                    new PostfixExpressionFormer().ConvertInfixToPostfix(
                           this._parsedExpression)))
            };
            FileAccessorUtil.WriteDataToFile(
                tokens, _outputTokensFileName);
        }

        private Mode ConvertStringToMode(string mode)
        {
            switch (mode.ToUpper())
            {
                case "LEX": 
                    return Mode.LEX;
                case "SYN":
                    return Mode.SYN;
                case "SEM":
                    return Mode.SEM;
                case "GEN1":
                    return Mode.GEN1;
                case "GEN2":
                    return Mode.GEN2;
                default:
                    throw new ArgumentException("Error mode!");
            }
        }

    }
}
