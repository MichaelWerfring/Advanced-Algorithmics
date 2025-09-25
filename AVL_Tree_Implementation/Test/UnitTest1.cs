using Lib;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var c1 = new Class1();

        bool value = c1.ReturnFalse();
        
        Assert.That(value, Is.False);
    }
}