using Lib;

namespace Test;

public class ExtensionsTest
{
    [TestCase(0,0,true)]
    [TestCase(1,1,true)]
    [TestCase(-1,-1,true)]
    [TestCase(-1,1,false)]
    [TestCase(1,-1,false)]
    [TestCase(-1,0,false)]
    public void IsEqualToComparesNumbersCorrectly(int first, int second, bool expected)
    {
        bool actual = first.IsEqualTo(second);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase(0,0,false)]
    [TestCase(1,1,false)]
    [TestCase(-1,-1,false)]
    [TestCase(-1,1,false)]
    [TestCase(-1,0,false)]
    [TestCase(1,-1,true)]
    [TestCase(2,1,true)]
    public void IsLargerThanComparesNumbersCorrectly(int first, int second, bool expected)
    {
        bool actual = first.IsLargerThan(second);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase(0,0,false)]
    [TestCase(1,1,false)]
    [TestCase(-1,-1,false)]
    [TestCase(-1,1,true)]
    [TestCase(-1,0,true)]
    [TestCase(1,-1,false)]
    [TestCase(1,2,true)]
    public void IsSmallerThanComparesNumbersCorrectly(int first, int second, bool expected)
    {
        bool actual = first.IsSmallerThan(second);
        Assert.That(actual, Is.EqualTo(expected));
    }
}