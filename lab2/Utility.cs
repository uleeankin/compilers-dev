using lab2.analyzer.semantic;
using lab2.analyzer.syntax;
using lab2.former.portable;
using lab2.former.symbol;
using lab2.optimisation;
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
        private readonly Mode _optimisationMode = Mode.NOTHING;

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
            string optimizationMode,
            string inputFileName,
            string outputTokensFileName,
            string outputSymbolsFileName)
        {
            this._inputFileName = inputFileName;
            this._outputTokensFileName = outputTokensFileName;
            this._outputSymbolsFileName = outputSymbolsFileName;
            this._operatingMode = this.ConvertStringToMode(operatingMode);
            this._optimisationMode = this.ConvertStringToMode(operatingMode);
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
                    if (_optimisationMode == Mode.NOTHING)
                    {
                        this.DoFirstGenerationMode();    
                    }
                    else
                    {
                        this.DoFirstGenerationModeWithOptimization();    
                    }
                    break;
                case Mode.GEN2:
                    if (_optimisationMode == Mode.NOTHING)
                    {
                        this.DoSecondGenerationMode();    
                    }
                    else
                    {
                        this.DoSecondGenerationModeWithOptimization();
                    }
                    break;
                case Mode.GEN3:
                        this.DoThirdGenerationMode();
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
            List<PortableCode> portableCode = 
                new PortableCodeFormer().Form(postfixExpression);
            List<Element> portableAdditionVars = 
                TokensParser.GetAdditionVariablesFromPortableCode(portableCode);
            
            FileAccessorUtil.WriteDataToFile(
                new CodeGeneratorSymbolsFormer()
                    .Form(_parsedExpression.Concat(portableAdditionVars).ToList()), 
                _outputSymbolsFileName);
            
            FileAccessorUtil.WriteDataToFile(
                PortableCodeToStringConverter.Convert(portableCode), _outputTokensFileName);
        }

        private void DoSecondGenerationMode()
        {
            _parsedExpression = new TokensFormer().Form(new ArithmeticExpressionParser().Parse(
                FileAccessorUtil
                    .ReadInputDataFromFile(_inputFileName)[0]));
            
            new SyntaxAnalyzer().Analyze(_parsedExpression);
            new SemanticAnalyzer().Analyze(_parsedExpression);

            FileAccessorUtil.WriteDataToFile(
                new CodeGeneratorSymbolsFormer()
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

        private void DoFirstGenerationModeWithOptimization()
        {
            _parsedExpression = new TokensFormer().Form(new ArithmeticExpressionParser().Parse(
                FileAccessorUtil
                    .ReadInputDataFromFile(_inputFileName)[0]));

            new SyntaxTreeFormer().Form(_parsedExpression);
            new SemanticAnalyzer().Analyze(_parsedExpression);
            new SemanticModifier().ModifyExpression(_parsedExpression);
            
            List<Element> postfixExpression = new PostfixExpressionFormer()
                .ConvertInfixToPostfix(new ArithmeticExpressionOptimiser().Optimize( this._parsedExpression));
            List<PortableCode> portableCode = 
                new PortableCodeOptimizer().Optimize(new PortableCodeFormer().Form(postfixExpression));
            List<Element> portableAdditionVars = 
                TokensParser.GetAdditionVariablesFromPortableCode(portableCode);
            
            FileAccessorUtil.WriteDataToFile(
                new CodeGeneratorSymbolsFormer()
                    .Form(_parsedExpression.Concat(portableAdditionVars).ToList()), 
                _outputSymbolsFileName);
            
            FileAccessorUtil.WriteDataToFile(
                PortableCodeToStringConverter.Convert(portableCode), _outputTokensFileName);
        }
        
        private void DoSecondGenerationModeWithOptimization()
        {
            _parsedExpression = new TokensFormer().Form(new ArithmeticExpressionParser().Parse(
                FileAccessorUtil
                    .ReadInputDataFromFile(_inputFileName)[0]));
            
            new SyntaxAnalyzer().Analyze(_parsedExpression);
            new SemanticAnalyzer().Analyze(_parsedExpression);

            new SemanticModifier().ModifyExpression(_parsedExpression);

            List<Element> modifiedExpression = new PostfixExpressionFormer().ConvertInfixToPostfix(
                new ArithmeticExpressionOptimiser().Optimize(this._parsedExpression));
            
            List<string> tokens = new List<string>
            {
                string.Join(" ", TokensListGetter.GetTokens(modifiedExpression))
            };

            FileAccessorUtil.WriteDataToFile(
                new CodeGeneratorSymbolsFormer()
                    .Form(modifiedExpression), 
                _outputSymbolsFileName);
            
            FileAccessorUtil.WriteDataToFile(
                tokens, _outputTokensFileName);
        }

        private void DoThirdGenerationMode()
        {
            _parsedExpression = new TokensFormer().Form(new ArithmeticExpressionParser().Parse(
                FileAccessorUtil
                    .ReadInputDataFromFile(_inputFileName)[0]));

            new SyntaxTreeFormer().Form(_parsedExpression);
            new SemanticAnalyzer().Analyze(_parsedExpression);
            new SemanticModifier().ModifyExpression(_parsedExpression);
            
            List<Element> postfixExpression = new PostfixExpressionFormer()
                .ConvertInfixToPostfix(this._parsedExpression);
            List<PortableCode> portableCode = 
                new PortableCodeFormer().Form(postfixExpression);
            
            FileAccessorUtil.WritePortableCodeToBinaryFile(portableCode, 
                new CodeGeneratorSymbolsFormer()
                                .FormVarsElements(_parsedExpression));
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
                case "OPT":
                    return Mode.OPT;
                case "GEN3":
                    return Mode.GEN3;
                default:
                    throw new ArgumentException("Error mode!");
            }
        }

    }
}
