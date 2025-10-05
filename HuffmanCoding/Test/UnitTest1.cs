using Lib;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        var c1 = new Class1();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}