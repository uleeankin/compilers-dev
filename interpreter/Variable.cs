using lab2.utils;

namespace interpreter
{
    public class Variables
    {
        public string VariableName { get; set; }
        
        public Double Value { get; set; }
        public Element Variable { get; set; }

        public Variables(Element variable)
        {
            Variable = variable;
            VariableName = variable.Definition.Split(" ")[3];
        }

    }
}

