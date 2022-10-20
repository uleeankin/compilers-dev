namespace lab2.utils;

public class SyntaxException : Exception
{
    public SyntaxException(string elementType, 
                            string elementSign, 
                            int position, 
                            string missingElement) 
        : base($" Синтаксическая ошибка! {elementType} {elementSign} на позиции {position} отсутствует {missingElement}.")
    {
    }
}