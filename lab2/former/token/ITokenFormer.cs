namespace lab2.former.token;

public interface ITokenFormer
{
    public string Form(string element);

    public string Form(string element, int position);
}