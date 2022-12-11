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
                    Console.WriteLine($"Input {element.Variable.Token} value with {element.Variable.Type} type");
                    var input = Console.ReadLine();
                    
                    
                }
            });
        }
    }
}

