using Lib;

namespace Test;

public class TreeTest
{
    [Test]
    public void CompareToThrowsArgumentNullExceptionWhenOtherIsNull()
    {
        var self = new Tree();
        self.Weight = 1;

        Assert.Catch<ArgumentNullException>(() => _ = self.CompareTo(null!));
    }
    
    [TestCase(0,0,0)]
    [TestCase(1,1,0)]
    [TestCase(-1,-1,0)]
    [TestCase(-1,1,-1)]
    [TestCase(1,-1,1)]
    [TestCase(-1,0,-1)]
    public void CompareToComparesTheTreesWeightsCorrectly(int first, int second, int expected)
    {
        var self = new Tree();
        self.Weight = first;
        
        var other = new Tree();
        other.Weight = second;

        int result = self.CompareTo(other);
        
        Assert.That(result, Is.EqualTo(expected));
    }
}