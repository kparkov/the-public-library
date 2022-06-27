using Bogus;

namespace NoobSoft.PublicLibrary.DataFaker;

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

public class Book
{
    public Guid Id { get; set; }
    public string ISBN { get; set; }
    public string Title { get; set; }
    public Guid Author { get; set; }
    public DateTime Published { get; set; }
    public string Summary { get; set; }
}


public class DataFaker
{
    private Random random = new Random(0);
    
    public string GenerateISBN13()
    {
        IEnumerable<int> r = new List<int>() { 9, 7, 8 };
        
        r = r.Concat(Enumerable.Range(0, 9).Select(x => random.Next(0, 10))).ToArray();
        
        int checksum = r.Select((item, index) => item * (index % 2 == 0 ? 1 : 3)).Sum();
        int last = 10 - (checksum % 10);
        r = r.Append(last);
        var d = r.Select(x => x.ToString()).ToArray();
        
        return d[0] + d[1] +d[2] + "-" + d[3] + d[4] + "-" + d[5] + d[6] + d[7] + d[8] + "-" + d[9] + d[10] + d[11] + "-" + d[12];
    }
    
    public IEnumerable<ISBN> ISBNs => new Faker<ISBN>()
        .CustomInstantiator(f => new ISBN(GenerateISBN13()))
        .GenerateForever();

    public IEnumerable<FakePerson> People => new Faker<FakePerson>()
        .RuleFor(x => x.Id, f => Guid.NewGuid())
        .RuleFor(x => x.Name, f => f.Person.FullName)
        .RuleFor(x => x.Birthday, f => f.Date.Past(150, DateTime.Now.AddYears(-10)))
        .GenerateForever();

    public IEnumerable<Book> Books(FakePerson author) => 
        new Faker<Book>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.ISBN, f => ISBNs.First().ToString())
            .RuleFor(x => x.Author, f => author.Id)
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(1, 5).Replace(".", ""))
            .RuleFor(x => x.Published, f => f.Date.Between(author.Birthday, DateTime.Now).AddDays(-1))
            .RuleFor(x => x.Summary, f => f.Lorem.Paragraph())
            .GenerateForever();

    public IEnumerable<(FakePerson Author, Book[] Books)> Authorships()
    {
        while (true)
        {
            var author = People.First();
            var numBooks = new Random().Next(1, 20); 
            var books = Books(author).Take(numBooks).ToArray();
            yield return (author, books);
        }
    }
}