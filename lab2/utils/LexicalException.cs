namespace lab2.utils;

public class LexicalException : Exception
{
    public LexicalException(string message, int position) 
        : base($"Лексическая ошибка! {message} на позиции {position}") {}
}