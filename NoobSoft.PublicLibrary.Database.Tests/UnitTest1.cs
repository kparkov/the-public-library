using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using NoobSoft.PublicLibrary.Database.Data;
using Xunit.Abstractions;

namespace NoobSoft.PublicLibrary.Database.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _out;

    public UnitTest1(ITestOutputHelper @out)
    {
        _out = @out;
    }
    
    [Fact]
    public void Test1()
    {
        var f = new DataFaker();
        var authorships = f
            .Authorships()
            .Take(500)
            .ToList();

        var authors = authorships
            .Select(x => x.Author);
        
        WriteToFile("authors.csv", authorships.Select(x => x.Author).OrderBy(x => x.Name));
        WriteToFile("books.csv", authorships.SelectMany(x => x.Books).OrderBy(x => x.Title));
        WriteToFile("loaners.csv", f.People.Take(1000).OrderBy(x => x.Name));
    }

    private void WriteToFile<T>(string filename, IEnumerable<T> list)
    {
        using var w = new StreamWriter(filename);
        using var csv = new CsvWriter(w, CultureInfo.InvariantCulture);
        csv.WriteRecords(list);
    }
}