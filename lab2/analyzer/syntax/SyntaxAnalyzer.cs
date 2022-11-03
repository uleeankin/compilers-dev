using lab2.utils;

namespace lab2.analyzer.syntax;

public class SyntaxAnalyzer
{
    private readonly ISyntaxAnalyzer _bracketAnalyzer;
    private readonly ISyntaxAnalyzer _operationSignAnalyzer;
    private readonly ISyntaxAnalyzer _variableAnalyzer;
    private readonly ISyntaxAnalyzer _numberAnalyzer;

    public SyntaxAnalyzer()
    {
        _bracketAnalyzer = new BracketSyntaxAnalyzer(); 
        _operationSignAnalyzer = new OperationSignSyntaxAnalyzer();
        _variableAnalyzer = new VariableSyntaxAnalyzer();
        _numberAnalyzer = new NumberSyntaxAnalyzer();
    }
    public void Analyze(List<Element> elements)
    {
        _bracketAnalyzer.Analyze(elements, "", 0);
        for (int i = 0; i < elements.Count; i++)
        {
            Element element = elements[i];
            switch (ElementTypeDefiner.DefineType(element.Token))
            {
                case ElementType.OPERATION_SIGN:
                    _operationSignAnalyzer.Analyze(elements, element.Token, i);
                    break;
                case ElementType.VARIABLE:
                    _variableAnalyzer.Analyze(elements, element.Token, i);
                    break;
                case ElementType.NUMBER:
                    _numberAnalyzer.Analyze(elements, element.Token, i);
                    break;
            }
        }
    }
}