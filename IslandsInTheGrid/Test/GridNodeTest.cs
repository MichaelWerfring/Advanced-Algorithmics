using Lib;

namespace Test;

public class GridNodeTest
{
    [Test]
    public void ConstructorSetsXAndY()
    {
        var node = new GridNode(10, 20);
        Assert.That(node.X, Is.EqualTo(10));
        Assert.That(node.Y, Is.EqualTo(20));
    }

    [Test]
    public void ToStringReturnsFormattedCoordinates()
    {
        var node = new GridNode(5, 5);
        Assert.That(node.ToString(), Is.EqualTo("(5, 5)"));
    }

    [Test]
    public void EqualsReturnsTrueForSameXAndYValues()
    {
        var node1 = new GridNode(1, 1);
        var node2 = new GridNode(1, 1);
        Assert.That(node1.Equals(node2), Is.True);
    }
    
    [Test]
    public void EqualsReturnsFalseForDifferentXAndYValues()
    {
        var node1 = new GridNode(1, 1);
        var node2 = new GridNode(1, 2);
        Assert.That(node1.Equals(node2), Is.False);
    }

    [Test]
    public void EqualsReturnsFalseIfComparedToNull()
    {
        var node1 = new GridNode(1, 1);
        Assert.That(node1.Equals(null!), Is.False);
    }

    [Test]
    public void EqualsReturnsTrueIfTheSameObjectIsCompared()
    {
        var node1 = new GridNode(1, 1);
        Assert.That(node1.Equals(node1), Is.True);
    }
}