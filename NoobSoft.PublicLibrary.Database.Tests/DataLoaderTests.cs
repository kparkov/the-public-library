using Xunit.Abstractions;

namespace NoobSoft.PublicLibrary.Database.Tests;

public class DataLoaderTests
{
    private readonly ITestOutputHelper _out;

    public DataLoaderTests(ITestOutputHelper @out)
    {
        _out = @out;
    }
    
    [Fact]
    public void Test1()
    {
    }
}