using Lib;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EmptyTreeHasHeightZero()
    {
        var tree = new Tree<int>(0);
        
        Assert.That(tree.GetHeight(), Is.EqualTo(0));
    }

    [Test]
    public void TreeWithLeftChildHasHeightOne()
    {
        var tree = new Tree<int>(0)
        {
            Left = new Tree<int>(1)
        };

        Assert.That(tree.GetHeight(), Is.EqualTo(1));
    }
    
    [Test]
    public void TreeWithRightChildHasHeightOne()
    {
        var tree = new Tree<int>(0)
        {
            Right = new Tree<int>(1)
        };

        Assert.That(tree.GetHeight(), Is.EqualTo(1));
    }

    [Test]
    public void TreeWithRightAndLeftChildHasHeightOne()
    {
        var tree = new Tree<int>(0)
        {
            Right = new Tree<int>(1),
            Left = new Tree<int>(1)
        };

        Assert.That(tree.GetHeight(), Is.EqualTo(1));
    }
    
    [Test]
    public void TreeWithGrandchildHasHeightTwo()
    {
        var tree = new Tree<int>(0);
        tree.Right = new Tree<int>(1);
        tree.Left = new Tree<int>(1);
        tree.Right.Right = new Tree<int>(2);
        
        Assert.That(tree.GetHeight(), Is.EqualTo(2));
    }

    [Test]
    public void EmpytTreeHasBalanceFactorZero()
    {
        var tree = new Tree<int>(0);
        
        Assert.That(tree.GetBalanceFactor(), Is.EqualTo(0));
    }
    
    [Test]
    public void TreeWithLeftChildHasBalanceFactorOne()
    {
        var tree = new Tree<int>(0)
        {
            Left = new Tree<int>(1)
        };

        Assert.That(tree.GetBalanceFactor(), Is.EqualTo(1));
    }
    
    [Test]
    public void TreeWithRightChildHasBalanceFactorMinusOne()
    {
        var tree = new Tree<int>(0)
        {
            Right = new Tree<int>(1)
        };

        Assert.That(tree.GetBalanceFactor(), Is.EqualTo(-1));
    }

    [Test]
    public void TreeWithLeftAndRightChildHasBalanceFactorZero()
    {
        var tree = new Tree<int>(0)
        {
            Right = new Tree<int>(1),
            Left = new Tree<int>(1)
        };

        Assert.That(tree.GetBalanceFactor(), Is.EqualTo(0));
    }

    [Test]
    public void TreeWithTwoLeftChildrenHasBalanceFactorTwo()
    {
        var tree = new Tree<int>(0);
        tree.Left = new Tree<int>(1);
        tree.Left.Right = new Tree<int>(2);
        
        Assert.That(tree.GetBalanceFactor(), Is.EqualTo(2));
    }
    
    [Test]
    public void TreeWithTwoRightChildrenHasBalanceFactorMinusTwo()
    {
        var tree = new Tree<int>(0);
        tree.Right = new Tree<int>(1);
        tree.Right.Left = new Tree<int>(2);
        
        Assert.That(tree.GetBalanceFactor(), Is.EqualTo(-2));
    }
    
    [Test]
    public void TreeWithTwoLeftChildrenAndOneRightChildHasBalanceFactorOne()
    {
        var tree = new Tree<int>(0);
        tree.Left = new Tree<int>(1);
        tree.Left.Right = new Tree<int>(2);
        tree.Right = new Tree<int>(1);
        
        Assert.That(tree.GetBalanceFactor(), Is.EqualTo(1));
    }
    
    [Test]
    public void TreeWithTwoRightChildrenAndOneLeftChildHasBalanceFactorMinusOne()
    {
        var tree = new Tree<int>(0);
        tree.Right = new Tree<int>(1);
        tree.Right.Left = new Tree<int>(2);
        tree.Left = new Tree<int>(1);
        
        Assert.That(tree.GetBalanceFactor(), Is.EqualTo(-1));
    }
}