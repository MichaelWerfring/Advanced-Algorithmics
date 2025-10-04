using Lib;

namespace Test;

public class NodeTests
{
    [Test]
    public void NewNodeHasHeightOne()
    {
        var node = new Node<int>(42);
        
        Assert.That(node.Height, Is.EqualTo(1));
    }

    [Test]
    public void NewNodeHasNullInsteadOfChildren()
    {
        var node = new Node<int>(42);
        
        Assert.That(node.Right, Is.Null);
        Assert.That(node.Left, Is.Null);
    }

    [Test]
    public void NewNodeContainsDataPassed()
    {
        int testNumber = 75;
        var node = new Node<int>(testNumber);
        
        Assert.That(node.Key, Is.EqualTo(testNumber));
    }
}