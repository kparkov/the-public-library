namespace NoobSoft.PublicLibrary.Database.Data;

public class ISBN
{
    private readonly string _value;
    
    public ISBN(string isbn)
    {
        _value = isbn;
    }

    public override string ToString()
    {
        return _value;
    }
}