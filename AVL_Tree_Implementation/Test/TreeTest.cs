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
    public void RotatingNodeToTheRightPutsLeftChildIntoRootAndBalancesTree()
    {
        var root = new Node<int>(3);
        root.Left = new Node<int>(2);
        root.Left.Left = new Node<int>(1);

        var rotatedSubtree = Tree<int>.RotateRight(root);
        
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
        Assert.Catch<ArgumentNullException>(() => Tree<int>.RotateRight(null!));
    }

    [Test]
    public void RotatingNodeToRightWithoutLeftChildToRightThrowsInvalidOperationException()
    {
        var root = new Node<int>(1)
        {
            Left = null
        };

        Assert.Catch<InvalidOperationException>(() => Tree<int>.RotateRight(root));
    }
    
    [Test]
    public void RotatingNodeToTheLeftPutsRightChildIntoRootAndBalancesTree()
    {
        var root = new Node<int>(1);
        root.Right = new Node<int>(2);
        root.Right.Right = new Node<int>(3);

        var rotatedSubtree = Tree<int>.RotateLeft(root);
        
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
        Assert.Catch<ArgumentNullException>(() => Tree<int>.RotateLeft(null!));
    }

    [Test]
    public void RotatingNodeToLeftWithoutRightChildToRightThrowsInvalidOperationException()
    {
        var root = new Node<int>(1)
        {
            Right = null
        };

        Assert.Catch<InvalidOperationException>(() => Tree<int>.RotateLeft(root));
    }

    [Test]
    public void RotatingNodeToTheLeftAndThenToTheRightBalancesOutTree()
    {
        var root = new Node<int>(3);
        root.Left = new Node<int>(1);
        root.Left.Right = new Node<int>(2);
        
        root.Left = Tree<int>.RotateLeft(root.Left); 
        var rotatedSubtree = Tree<int>.RotateRight(root);
        
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
    public void RotatingNodeToTheRightAndThenToTheLeftBalancesOutTree()
    {
        var root = new Node<int>(1);
        root.Right = new Node<int>(3);
        root.Right.Left = new Node<int>(2);
        
        root.Right = Tree<int>.RotateRight(root.Right); 
        var rotatedSubtree = Tree<int>.RotateLeft(root);
        
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
    public void RotatingNodeToTheRightUpdatesHeightsOfTheNode()
    {
        var root = new Node<int>(3);
        root.Height = 3;
        
        root.Left = new Node<int>(2);
        root.Left.Height = 2;
        
        root.Left.Left = new Node<int>(1);
        root.Left.Left.Height = 1;

        var rotatedSubtree = Tree<int>.RotateRight(root);
        
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

        var rotatedSubtree = Tree<int>.RotateLeft(root);
        
        Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
    }

    [Test]
    public void RotatingNodeToTheLeftAndThenToTheRightUpdatesHeightsOfTheNodes()
    {
        var root = new Node<int>(3);
        root.Height = 3;
        
        root.Left = new Node<int>(1);
        root.Left.Height = 2;
        
        root.Left.Right = new Node<int>(2);
        root.Left.Right.Height = 1;
        
        root.Left = Tree<int>.RotateLeft(root.Left); 
        var rotatedSubtree = Tree<int>.RotateRight(root);
        
        Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
    }
    
    [Test]
    public void RotatingNodeToTheRightAndThenToTheLeftUpdatesHeightsOfTheNodes()
    {
        var root = new Node<int>(1);
        root.Right = new Node<int>(3);
        root.Right.Left = new Node<int>(2);

        root.Right = Tree<int>.RotateRight(root.Right);
        var rotatedSubtree = Tree<int>.RotateLeft(root);
        
        Assert.That(rotatedSubtree.Height, Is.EqualTo(2));
        Assert.That(rotatedSubtree.Left, Is.Not.Null);
        Assert.That(rotatedSubtree.Left.Height, Is.EqualTo(1));
        Assert.That(rotatedSubtree.Right, Is.Not.Null);
        Assert.That(rotatedSubtree.Right.Height, Is.EqualTo(1));
    }
}