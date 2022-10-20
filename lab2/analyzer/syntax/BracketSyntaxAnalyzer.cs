using lab2.utils;

namespace lab2.analyzer.syntax;

public class BracketSyntaxAnalyzer : ISyntaxAnalyzer
{
    private Stack<int> _openedBrackets = new Stack<int>();
    private Stack<int> _closedBrackets = new Stack<int>();
    public void Analyze(List<string> elements, string element, int index)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            string elem = elements[i].Substring(1, elements[i].Length - 2);
            if (ElementTypeDefiner.DefineType(elem) == ElementType.BRACKET)
            {
                if (BracketTypeDefiner.Define(elem) == BracketType.OPENED)
                {
                    _openedBrackets.Push(i);
                }
                
                if (BracketTypeDefiner.Define(elem) == BracketType.CLOSED)
                {
                    if (_openedBrackets.Count != 0)
                    {
                        _openedBrackets.Pop();    
                    }
                    else
                    {
                        _closedBrackets.Push(i);
                    }
                }
            }
        }
        this.GetSyntaxException(elements);
        
    }

    private void GetSyntaxException(List<string> elements)
    {
        if (this._openedBrackets.Count != 0)
        {
            List<int> openedBrackets = new List<int>(_openedBrackets.ToArray());
            throw new SyntaxException("У открывающей скобки", elements[openedBrackets[0]],
                ElementPositionDefiner.GetPosition(elements, openedBrackets[0]), "закрывающая скобка");
        }
        
        if (this._closedBrackets.Count != 0)
        {
            List<int> closedBrackets = new List<int>(_closedBrackets.ToArray());
            throw new SyntaxException("У закрывающей скобки", elements[closedBrackets[0]],
                ElementPositionDefiner.GetPosition(elements, closedBrackets[0]), "открывающая скобка");
        }
    }

    
    
}