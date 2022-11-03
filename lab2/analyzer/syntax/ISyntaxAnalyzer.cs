using lab2.utils;

namespace lab2.analyzer.syntax;

public interface ISyntaxAnalyzer
{
    public abstract void Analyze(List<Element> elements, string element, int index);
}