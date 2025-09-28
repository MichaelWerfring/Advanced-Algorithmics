using Lib;

namespace Test;

public class TreeTests
{
    [Test]
    public void EmptyTreeHasHeightZero()
    {
        var tree = new Tree<int>();
        
        Assert.That(tree.Height, Is.EqualTo(0));
    }

    [Test]
    public void TreeWithOneItemHasHeightOne()
    {
        var tree = new Tree<int>(new Node<int>(42));
        
        Assert.That(tree.Height, Is.EqualTo(1));
    }

    [Test]
    public void InsertingIntoEmptyTreeAddsItemAsRoot()
    {
        int newNumber = 2;
        var tree = new Tree<int>();
        
        tree.Insert(newNumber);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(newNumber));
    }

    [Test]
    public void InsertingLargerItemIntoTreePutsItIntoRightSubtree()
    {
        int newNumber = 2;
        var tree = new Tree<int>(new Node<int>(1));
        
        tree.Insert(newNumber);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Right, Is.Not.Null);
        Assert.That(tree.Root.Right.Data, Is.EqualTo(newNumber));
    }
    
    [Test]
    public void InsertingSmallerItemIntoTreePutsItIntoLeftSubtree()
    {
        int newNumber = 1;
        var tree = new Tree<int>(new Node<int>(2));
        
        tree.Insert(newNumber);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Left, Is.Not.Null);
        Assert.That(tree.Root.Left.Data, Is.EqualTo(newNumber));
    }

    [Test]
    public void InsertingSameItemTwiceDoesNotChangeTree()
    {
        int newNumber = 1;
        var tree = new Tree<int>(new Node<int>(1));
        
        tree.Insert(newNumber);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Left, Is.Null);
        Assert.That(tree.Root.Right, Is.Null);
    }

    [Test]
    public void InsertingItemsIntoTreeUpdatesItHeight()
    {
        var newNode = new Node<int>(2);

        var tree = new Tree<int>(newNode);
        tree.Insert(1);
        tree.Insert(3);
        tree.Insert(4);
        
        Assert.That(tree.Height, Is.EqualTo(3));
    }

    [Test]
    public void InsertingTwoItemsLargerThanRootBalancesTheTree()
    {

    }
    
    [Test]
    public void InsertingTwoItemsSmallerThanRootBalancesTheTree()
    {
        
    }
}