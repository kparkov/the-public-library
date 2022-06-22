using Bogus;

namespace NoobSoft.PublicLibrary.Database.Data;

public enum Gender
{
    Unknown,
    Male,
    Female
}



public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public Gender Gender { get; set; }
}

public class Author : Person
{
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

public class BookLoaner
{
}


public class DataFaker
{
    public IEnumerable<ISBN> ISBNs => new Faker<ISBN>()
        .CustomInstantiator(f => new ISBN(f.Random.Replace("###-##-####-###-#")))
        .GenerateForever();

    public IEnumerable<Person> People => new Faker<Person>()
        .RuleFor(x => x.Id, f => Guid.NewGuid())
        .RuleFor(x => x.Name, f => f.Person.FullName)
        .RuleFor(x => x.Birthday, f => f.Date.Past(150, DateTime.Now.AddYears(-10)))
        .RuleFor(x => x.Gender, f => f.PickRandom(Gender.Male, Gender.Female))
        .GenerateForever();

    public IEnumerable<Book> Books(Person author) => 
        new Faker<Book>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.ISBN, f => ISBNs.First().ToString())
            .RuleFor(x => x.Author, f => author.Id)
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(1, 5).Replace(".", ""))
            .RuleFor(x => x.Published, f => f.Date.Between(author.Birthday, DateTime.Now).AddDays(-1))
            .RuleFor(x => x.Summary, f => f.Lorem.Paragraph())
            .GenerateForever();

    public IEnumerable<(Person Author, Book[] Books)> Authorships()
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