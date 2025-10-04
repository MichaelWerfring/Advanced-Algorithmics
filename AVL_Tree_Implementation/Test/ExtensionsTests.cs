using Lib;

namespace Test;

public class ExtensionsTests
{
    [Test]
    public void CalculateHeightReturnsOneOnSingleNode()
    {
        Node<int> newNode = new Node<int>(1);
        
        Assert.That(newNode.CalculateHeight(), Is.EqualTo(1));
        
    }

    [Test]
    public void CalculateHeightReturnsTwoWhenNodeHasChildrenOneLevelLower()
    {
        var newNode = new Node<int>(2);
        newNode.Left = new Node<int>(1);
        newNode.Right = new Node<int>(3);
        
        Assert.That(newNode.CalculateHeight(), Is.EqualTo(2));
    }

    [Test]
    public void CalculateBalanceFactorReturnsZeroWhenNodeNoChildren()
    {
        var node = new Node<int>(2);

        int balanceFactor = node.CalculateBalanceFactor();
        
        Assert.That(balanceFactor, Is.EqualTo(0));
    }
    
    [Test]
    public void CalculateBalanceFactorReturnsZeroWhenNodeHasEqualNumberOfRightAndLeftChildren()
    {
        var node = new Node<int>(2);
        node.Left = new Node<int>(1);
        node.Right = new Node<int>(3);
        
        int balanceFactor = node.CalculateBalanceFactor();
        
        Assert.That(balanceFactor, Is.EqualTo(0));
    }
    
    [Test]
    public void CalculateBalanceFactorReturnsOneWhenNodeHasMoreLeftChildrenThanRight()
    {
        var node = new Node<int>(2);
        node.Left = new Node<int>(1);
        
        int balanceFactor = node.CalculateBalanceFactor();
        
        Assert.That(balanceFactor, Is.EqualTo(1));
    }
    
    [Test]
    public void CalculateBalanceFactorReturnsMinusOneWhenNodeHasMoreRightChildrenThanLeft()
    {
        var node = new Node<int>(2);
        node.Right = new Node<int>(3);
        
        int balanceFactor = node.CalculateBalanceFactor();
        
        Assert.That(balanceFactor, Is.EqualTo(-1));
    }
    
    [Test]
    public void CalculateBalanceFactorReturnsTwoWhenNodeHasTwoMoreLeftChildrenThanRight()
    {
        var node = new Node<int>(2);
        node.Left = new Node<int>(1);
        node.Left.Left = new Node<int>(1);
        
        int balanceFactor = node.CalculateBalanceFactor();
        
        Assert.That(balanceFactor, Is.EqualTo(1));
    }
    
    [Test]
    public void CalculateBalanceFactorReturnsMinusTwoWhenNodeHasTwoMoreRightChildrenThanLeft()
    {
        var node = new Node<int>(2);
        node.Right = new Node<int>(3);
        node.Right.Right = new Node<int>(3);
        
        int balanceFactor = node.CalculateBalanceFactor();
        
        Assert.That(balanceFactor, Is.EqualTo(-1));
    }
    
    [Test]
    public void RotatingNodeToTheLefPutsLeftChildIntoRootAndBalancesTree()
    {
        var root = new Node<int>(1);
        root.Right = new Node<int>(2);
        root.Right.Right = new Node<int>(3);

        var rotatedSubtree = root.RotateLeft();
        
        Assert.That(rotatedSubtree, Is.Not.Null);
        Assert.That(rotatedSubtree.Data, Is.EqualTo(2));
        
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Data, Is.EqualTo(1));
        
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Data, Is.EqualTo(3));
        
        // Check that no references are pointing upwards in the tree
        Assert.That(rotatedSubtree.Right.Right, Is.Null);
        Assert.That(rotatedSubtree.Right.Left, Is.Null);
        
        Assert.That(rotatedSubtree.Left.Right, Is.Null);
        Assert.That(rotatedSubtree.Left.Left, Is.Null);
    }

    [Test]
    public void RotatingNullToRightThrowsArgumentNullException()
    {
        // Suppress nullable warning with ! since it is exactly what needs to be tested
        Node<int> dummyNode = null!;
        
        Assert.Catch<ArgumentNullException>(() => dummyNode.RotateRight());
    }
    
    [Test]
    public void RotatingNodeToTheRightPutsRightChildIntoRootAndBalancesTree()
    {
        var root = new Node<int>(3);
        root.Left = new Node<int>(2);
        root.Left.Left = new Node<int>(1);

        var rotatedSubtree = root.RotateRight();
        
        Assert.That(rotatedSubtree, Is.Not.Null);
        Assert.That(rotatedSubtree.Data, Is.EqualTo(2));
        
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Data, Is.EqualTo(1));
        
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Data, Is.EqualTo(3));
        
        // Check that no references are pointing upwards in the tree
        Assert.That(rotatedSubtree.Right.Right, Is.Null);
        Assert.That(rotatedSubtree.Right.Left, Is.Null);
        
        Assert.That(rotatedSubtree.Left.Right, Is.Null);
        Assert.That(rotatedSubtree.Left.Left, Is.Null);
    }
    
    [Test]
    public void RotatingNullToLeftThrowsArgumentNullException()
    {
        // Suppress nullable warning with ! since it is exactly what needs to be tested
        Node<int> dummyNode = null!;
        
        Assert.Catch<ArgumentNullException>(() => dummyNode.RotateLeft());
    }

    [Test]
     public void RotatingNodeToTheRightUpdatesHeightsOfTheNode()
     {
         var root = new Node<int>(3);
         root.Height = 3;
         
         root.Left = new Node<int>(2);
         root.Left.Height = 2;
         
         root.Left.Left = new Node<int>(1);
         root.Left.Left.Height = 1;
    
         var rotatedSubtree = root.RotateRight();
         
         Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
         Assert.That(rotatedSubtree.Left, Is.Not.Null);
         Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
         Assert.That(rotatedSubtree.Right, Is.Not.Null);
         Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
     }
    
    [Test]
    public void RotatingNodeToTheLeftUpdatesHeightsOfTheNode()
    {
        var root = new Node<int>(1);
        root.Height = 3;
        
        root.Right = new Node<int>(2);
        root.Right.Height = 2;
        
        root.Right.Right = new Node<int>(3);
        root.Right.Right.Height = 1;

        var rotatedSubtree = root.RotateLeft();
        
        Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
    }

    [Test]
    public void FindMaxReturnsMaximumChildOfNode()
    {
        var node = new Node<int>(0);
        node.Right = new Node<int>(1);
        node.Right.Right = new Node<int>(2);

        var max = node.FindMax();
        
        Assert.That(max, Is.Not.Null);
        Assert.That(max.Data, Is.EqualTo(2));
    }   
    
}