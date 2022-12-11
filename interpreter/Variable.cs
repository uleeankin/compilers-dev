using lab2.utils;

namespace interpreter
{
    public class Variables
    {
        private Element variable;
        
        public double Value { get; set; }
        public Element Variable { get; }

        public Variables(Element variable)
        {
            this.variable = variable;
        }

    }
}

