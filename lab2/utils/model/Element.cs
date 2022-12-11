
namespace lab2.utils
{
    [Serializable]
    public class Element
    {
        private string token;
        private string definition;
        private int position;
        private ElementType type;

        public Element(string token, string definition, 
            int position, ElementType type)
        {
            this.token = token;
            this.definition = definition;
            this.position = position;
            this.type = type;
        }

        public string Definition
        {
            get => definition;
            set => definition = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Token
        {
            get => token;
            set => token = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Position
        {
            get => position;
            set => position = value;
        }

        public ElementType Type
        {
            get => type;
            set => type = value;
        }
    }    
}