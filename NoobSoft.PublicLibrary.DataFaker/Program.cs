// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using NoobSoft.PublicLibrary.DataFaker;

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

void WriteToFile<T>(string filename, IEnumerable<T> list)
{
    using var w = new StreamWriter(filename);
    using var csv = new CsvWriter(w, CultureInfo.InvariantCulture);
    Console.WriteLine(Path.GetFullPath(filename));
    csv.WriteRecords(list);
}