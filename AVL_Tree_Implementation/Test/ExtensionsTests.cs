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
    public void CalculateHeightReturnsTwoWhenNodeHasChildren()
    {
        var newNode = new Node<int>(2);
        newNode.Left = new Node<int>(1);
        newNode.Right = new Node<int>(3);
        
        Assert.That(newNode.CalculateHeight(), Is.EqualTo(2));
    }
    
    
    
    [Test]
    public void RotatingNodeToTheRightPutsLeftChildIntoRootAndBalancesTree()
    {
        var root = new Node<int>(1);
        root.Right = new Node<int>(2);
        root.Right.Right = new Node<int>(3);

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
    public void RotatingNullToRightThrowsArgumentNullException()
    {
        // Suppress nullable warning with ! since it is exactly what needs to be tested
        Node<int> dummyNode = null!;
        
        Assert.Catch<ArgumentNullException>(() => dummyNode.RotateRight());
    }

    [Test]
    public void RotatingNodeToRightWithoutLeftChildToRightThrowsInvalidOperationException()
    {
        var root = new Node<int>(1)
        {
            Left = null
        };

        Assert.Catch<InvalidOperationException>(() => root.RotateRight());
    }
    
    [Test]
    public void RotatingNodeToTheLeftPutsRightChildIntoRootAndBalancesTree()
    {
        var root = new Node<int>(3);
        root.Left = new Node<int>(2);
        root.Left.Left = new Node<int>(1);

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
    public void RotatingNullToLeftThrowsArgumentNullException()
    {
        // Suppress nullable warning with ! since it is exactly what needs to be tested
        Node<int> dummyNode = null!;
        
        Assert.Catch<ArgumentNullException>(() => dummyNode.RotateLeft());
    }

    [Test]
    public void RotatingNodeToLeftWithoutRightChildToRightThrowsInvalidOperationException()
    {
        var root = new Node<int>(1)
        {
            Right = null
        };

        Assert.Catch<InvalidOperationException>(() => root.RotateLeft());
    }

    [Test]
    public void RotatingNodeToTheRightAndThenToTheLeftBalancesOutTree()
    {
        var root = new Node<int>(3);
        root.Left = new Node<int>(1);
        root.Left.Right = new Node<int>(2);
        
        root.Left = root.Left.RotateRight(); 
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
    public void RotatingNodeToTheLeftAndThenToTheRightBalancesOutTree()
    {
        var root = new Node<int>(1);
        root.Right = new Node<int>(3);
        root.Right.Left = new Node<int>(2);
        
        root.Right = root.Right.RotateLeft(); 
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
    public void RotatingNodeToTheLeftUpdatesHeightsOfTheNode()
    {
        var root = new Node<int>(3);
        root.Height = 3;
        
        root.Left = new Node<int>(2);
        root.Left.Height = 2;
        
        root.Left.Left = new Node<int>(1);
        root.Left.Left.Height = 1;

        var rotatedSubtree = root.RotateLeft();
        
        Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
    }
    
    [Test]
    public void RotatingNodeToTheRightUpdatesHeightsOfTheNode()
    {
        var root = new Node<int>(1);
        root.Height = 3;
        
        root.Right = new Node<int>(2);
        root.Right.Height = 2;
        
        root.Right.Right = new Node<int>(3);
        root.Right.Right.Height = 1;

        var rotatedSubtree = root.RotateRight();
        
        Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
    }

    [Test]
    public void RotatingNodeToTheRightAndThenToTheLeftUpdatesHeightsOfTheNodes()
    {
        var root = new Node<int>(3);
        root.Height = 3;
        
        root.Left = new Node<int>(1);
        root.Left.Height = 2;
        
        root.Left.Right = new Node<int>(2);
        root.Left.Right.Height = 1;
        
        root.Left = root.Left.RotateRight(); 
        var rotatedSubtree = root.RotateLeft();
        
        Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
    }
    
    [Test]
    public void RotatingNodeToTheLeftAndThenToTheRightUpdatesHeightsOfTheNodes()
    {
        var root = new Node<int>(1);
        root.Right = new Node<int>(3);
        root.Right.Left = new Node<int>(2);

        root.Right = root.Right.RotateLeft();
        var rotatedSubtree = root.RotateRight();
        
        Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
    }
}