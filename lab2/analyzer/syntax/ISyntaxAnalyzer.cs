namespace lab2.analyzer.syntax;

public interface ISyntaxAnalyzer
{
    public abstract void Analyze(List<string> elements, string element, int index);
}