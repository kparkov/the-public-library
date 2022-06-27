namespace NoobSoft.PublicLibrary.Database.Model;

public readonly struct ISBN
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