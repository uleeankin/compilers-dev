using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using lab2.utils;

namespace interpreter
{
    public static class Program
    {
        private static List<PortableCode> _portableCode;
        private static List<Variables> _variables;

        public static void Main(string[] args)
        {
            GetDataFromFile(args[0]);
            InputVariables();
            Calculate();
        }

        private static void GetDataFromFile(string fileName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream(fileName, FileMode.Open);

            try
            {
                #pragma warning disable CS0618
                _portableCode = (List<PortableCode>)binaryFormatter.Deserialize(fs);
                GenerateVariables((List<Element>)binaryFormatter.Deserialize(fs));
                #pragma warning restore CS0618
            }
            catch (SerializationException e)
            {
                throw new SerializationException("Error deserialization because " + e.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        private static void GenerateVariables(List<Element> variable)
        {
            _variables = new List<Variables>();
            variable.ForEach((var) => _variables.Add(new Variables(var)));
        }

        private static void InputVariables()
        {
            _variables.ForEach(element =>
            {
                while (true)
                {
                    Console.WriteLine($"Input {element.VariableName} value with {element.Variable.Type} type");
                    var input = Console.ReadLine();

                    if ((element.Variable.Type == ElementType.INTEGER_VARIABLE && int.TryParse(input, out var _))
                        || (element.Variable.Type == ElementType.FLOAT_VARIABLE && double.TryParse(input, out var _)))
                    {
                        element.Value = Double.Parse(input);
                        break;
                    }  
                }
            });
        }

        private static void Calculate()
        {

            string expressionResult = "";
            _portableCode.ForEach((code) =>
                {
                    string operation = code.Operation.Token;
                    var result = code.Result;
                    var firstOperand = code.FirstOperand;
                    var secondOperand = code.SecondOperand;

                    try
                    {
                        switch (operation)
                        {
                            case "mul":
                                result.Definition = Mul(firstOperand, secondOperand);
                                break;
                            case "add":
                                result.Definition = Add(firstOperand, secondOperand);
                                break;
                            case "i2f":
                                break;
                            case "sub":
                                result.Definition = Sub(firstOperand, secondOperand);
                                break;
                            case "div":
                                result.Definition = Div(firstOperand, secondOperand);
                                break;
                        }

                        expressionResult = result.Definition;
                    }
                    catch (DivideByZeroException e)
                    {
                        throw new DivideByZeroException("Divide by zero");
                    }
                    
                }
            );
            Console.WriteLine(Math.Round(double.Parse(TokensParser.GetValue(expressionResult)), 2));
        }

        private static string Add(Element firstOperand, Element secondOperand)
        {
            return ExecuteOperation(firstOperand, secondOperand,
                (x, y) => x + y, (x, y) => x + y);
        }

        private static string Mul(Element firstOperand, Element secondOperand)
        {
            return ExecuteOperation(firstOperand, secondOperand,
                (x, y) => x + y, (x, y) => x * y);
        }

        private static string Div(Element firstOperand, Element secondOperand)
        {
            return ExecuteOperation(firstOperand, secondOperand,
                (x, y) => x + y, (x, y) => x / y);
        }

        private static string Sub(Element firstOperand, Element secondOperand)
        {
            return ExecuteOperation(firstOperand, secondOperand,
                (x, y) => x + y, (x, y) => x - y);
        }

        private static string ExecuteOperation(Element firstOperand, Element secondOperand, 
            Func<int, int, int> intFunc, Func<double, double, double> doubleFunc)
        {

            Element first = CastElement(firstOperand);
            Element second = CastElement(secondOperand);


            if (first.Type == ElementType.INTEGER
                && second.Type == ElementType.INTEGER)
            {
                return "<" + intFunc(
                    int.Parse(TokensParser.GetValue(first.Token)),
                    int.Parse(TokensParser.GetValue(second.Token))) + ">";
            }
            
            if (first.Type == ElementType.FLOAT
                && second.Type == ElementType.FLOAT)
            {
                string firstToken = CastDoubleToken(TokensParser.GetValue(first.Token));
                string secondToken = CastDoubleToken(TokensParser.GetValue(second.Token));
                return "<" + doubleFunc(
                    double.Parse(firstToken),
                    double.Parse(secondToken)) + ">";
            }

            
            return "";
        }

        private static string CastDoubleToken(string token)
        {
            if (token.Contains("."))
            {
                token.Replace('.', ',');
            }

            return token;
        }

        private static Element CastElement(Element operand)
        {
            Element castOperand = null;
            
            if (operand.Type == ElementType.INTEGER
                || operand.Type == ElementType.FLOAT)
            {
                castOperand = operand;
            }

            if (operand.Type == ElementType.FLOAT_VARIABLE
                || operand.Type == ElementType.INTEGER_VARIABLE)
            {
                ElementType type;

                if (operand.Type == ElementType.FLOAT_VARIABLE)
                {
                    type = ElementType.FLOAT;
                }
                else
                {
                    type = ElementType.INTEGER;
                }

                if (operand.Position == -1)
                {
                    Element additionalElement = FindById(operand.Token);
                    additionalElement.Type = type;
                    additionalElement.Token = operand.Definition;
                    castOperand = additionalElement;
                }
                else
                {
                    Variables var = _variables.Find((v) => v.Variable.Token == operand.Token);
                    castOperand = new Element("<" + var.Value + ">",
                        var.VariableName, var.Variable.Position, type);
                }
                
            }

            return castOperand;
        }

        private static Element FindById(string token)
        {
            foreach (PortableCode code in _portableCode)
            {
                if (code.Result.Token == token)
                {
                    return code.Result;
                }
            }

            return null;
        }
    }
}

