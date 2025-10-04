using System.Diagnostics;
using Lib;

namespace Test;

public class TreeTests
{
    
    //TODO: Refactor to work with strings and floats as well
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
    public void InsertingItemIntoEmptyTreeAddsItAsRoot()
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

    [TestCase(1,2,3)]
    [TestCase(1,3,2)]
    public void InsertingTwoItemsLargerThanRootResultsInBalancedTree(int first, int second, int third)
    {
        var tree = new Tree<int>(new Node<int>(first));
        
        tree.Insert(second);
        tree.Insert(third);

        var root = tree.Root;
        
        Assert.That(root, Is.Not.Null);
        Assert.That(root.Data, Is.EqualTo(2));
        
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Left.Data, Is.EqualTo(1));
        
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Right.Data, Is.EqualTo(3));
        
        // Check that no references are pointing upwards in the tree
        Assert.That(root.Right.Right, Is.Null);
        Assert.That(root.Right.Left, Is.Null);
        
        Assert.That(root.Left.Right, Is.Null);
        Assert.That(root.Left.Left, Is.Null);
    }
    
    [TestCase(3,2,1)]
    [TestCase(3,1,2)]
    public void InsertingTwoItemsSmallerThanRootResultsInBalancedTree(int first, int second, int third)
    {
        var tree = new Tree<int>(new Node<int>(first));
        
        tree.Insert(second);
        tree.Insert(third);

        var root = tree.Root;
        
        Assert.That(root, Is.Not.Null);
        Assert.That(root.Data, Is.EqualTo(2));
        
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Left.Data, Is.EqualTo(1));
        
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Right.Data, Is.EqualTo(3));
        
        // Check that no references are pointing upwards in the tree
        Assert.That(root.Right.Right, Is.Null);
        Assert.That(root.Right.Left, Is.Null);
        
        Assert.That(root.Left.Right, Is.Null);
        Assert.That(root.Left.Left, Is.Null);
    }
    
    [Test]
    public void InsertingMultipleValuesAscendingResultsInBalancedTree()
    {
        var tree = new Tree<int>(new Node<int>(1));

        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(4);
        tree.Insert(5);
        tree.Insert(6);

        var root = tree.Root;
        Assert.That(root, Is.Not.Null);
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Left.Left, Is.Not.Null);
        Assert.That(root.Left.Right, Is.Not.Null);
        Assert.That(root.Right.Right, Is.Not.Null);
        
        Assert.That(root.Data, Is.EqualTo(4));
        Assert.That(root.Left.Data, Is.EqualTo(2));
        Assert.That(root.Right.Data, Is.EqualTo(5));
        Assert.That(root.Left.Left.Data, Is.EqualTo(1));
        Assert.That(root.Left.Right.Data, Is.EqualTo(3));
        Assert.That(root.Right.Right.Data, Is.EqualTo(6));
    }
    
    [Test]
    public void InsertingMultipleValuesDescendingAlsoResultsInBalancedTree()
    {
        var tree = new Tree<int>(new Node<int>(6));

        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(3);
        tree.Insert(2);
        tree.Insert(1);

        var root = tree.Root;
        Assert.That(root, Is.Not.Null);
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Left.Left, Is.Not.Null);
        Assert.That(root.Right.Right, Is.Not.Null);
        Assert.That(root.Right.Left, Is.Not.Null);

        Assert.That(root.Data, Is.EqualTo(3));
        Assert.That(root.Left.Data, Is.EqualTo(2));
        Assert.That(root.Right.Data, Is.EqualTo(5));
        Assert.That(root.Left.Left.Data, Is.EqualTo(1));
        Assert.That(root.Right.Right.Data, Is.EqualTo(6));
        Assert.That(root.Right.Left.Data, Is.EqualTo(4));
        
        Assert.That(root.Height, Is.EqualTo(3));
        Assert.That(root.Left.Height, Is.EqualTo(2));
        Assert.That(root.Right.Height, Is.EqualTo(2));
        Assert.That(root.Left.Left.Height, Is.EqualTo(1));
        Assert.That(root.Right.Right.Height, Is.EqualTo(1));
        Assert.That(root.Right.Left.Height, Is.EqualTo(1));
    }
    
    [Test]
    public void InsertingMultipleValuesAscendingAdjustsHeightsWithinTree()
    {
        var tree = new Tree<int>(new Node<int>(1));

        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(4);
        tree.Insert(5);
        tree.Insert(6);

        var root = tree.Root;
        Assert.That(root, Is.Not.Null);
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Left.Left, Is.Not.Null);
        Assert.That(root.Left.Right, Is.Not.Null);
        Assert.That(root.Right.Right, Is.Not.Null);
        
        Assert.That(root.Height, Is.EqualTo(3));
        Assert.That(root.Left.Height, Is.EqualTo(2));
        Assert.That(root.Right.Height, Is.EqualTo(2));
        Assert.That(root.Left.Left.Height, Is.EqualTo(1));
        Assert.That(root.Left.Right.Height, Is.EqualTo(1));
        Assert.That(root.Right.Right.Height, Is.EqualTo(1));
    }
    
    [Test]
    public void InsertingMultipleValuesDescendingAdjustsHeightsWithinTree()
    {
        var tree = new Tree<int>(new Node<int>(6));

        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(3);
        tree.Insert(2);
        tree.Insert(1);

        var root = tree.Root;
        Assert.That(root, Is.Not.Null);
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Left.Left, Is.Not.Null);
        Assert.That(root.Right.Right, Is.Not.Null);
        Assert.That(root.Right.Left, Is.Not.Null);
        
        Assert.That(root.Height, Is.EqualTo(3));
        Assert.That(root.Left.Height, Is.EqualTo(2));
        Assert.That(root.Right.Height, Is.EqualTo(2));
        Assert.That(root.Left.Left.Height, Is.EqualTo(1));
        Assert.That(root.Right.Right.Height, Is.EqualTo(1));
        Assert.That(root.Right.Left.Height, Is.EqualTo(1));
    }

    [Test]
    public void DeletingItemFromEmptyTreeDoesNotChangeTree()
    {
        var tree = new Tree<int>();
        
        tree.Delete(0);
        
        Assert.That(tree.Root, Is.Null);
    }

    [Test]
    public void DeletingItemFromTreeWithSingleItemLeavesEmptyTree()
    {
        var tree = new Tree<int>(new Node<int>(0));
        
        tree.Delete(0);
        
        Assert.That(tree.Root, Is.Null);
    }

    [Test]
    public void DeletingItemWhichIsNotInTreeDoesNotChangeTree()
    {
        var tree = new Tree<int>(new Node<int>(0));
        
        tree.Delete(1);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(0));
    }

    [Test]
    public void DeletingRootWithLeftChildReplacesRootWithChild()
    {
        var root = new Node<int>(1);
        root.Left = new Node<int>(0);
        var tree = new Tree<int>(root);
        
        tree.Delete(1);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(0));
    }
    
    [Test]
    public void DeletingRootWithRightChildReplacesRootWithChild()
    {
        var root = new Node<int>(1);
        root.Right = new Node<int>(2);
        var tree = new Tree<int>(root);
        
        tree.Delete(1);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(2));
    }
    
    [Test]
    public void DeletingRootWithTwoChildrenReplacesRootWithLeftMaxChild()
    {
        var root = new Node<int>(1);
        root.Left = new Node<int>(0);
        root.Right = new Node<int>(2);
        
        var tree = new Tree<int>(root);
        
        tree.Delete(1);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(0));
    }
    
    [Test]
    public void DeletingLeafItemDeletesTheItemButDoesNotChangeRestOfTheTree()
    {
        var root = new Node<int>(2);
        root.Left = new Node<int>(1);
        root.Right = new Node<int>(3);
        root.Right.Right = new Node<int>(4);

        var tree = new Tree<int>(root);
        
        tree.Delete(4);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(2));
        Assert.That(tree.Root.Left, Is.Not.Null);
        Assert.That(tree.Root.Left.Data, Is.EqualTo(1));
        Assert.That(tree.Root.Right, Is.Not.Null);
        Assert.That(tree.Root.Right.Data, Is.EqualTo(3));
        Assert.That(tree.Root.Right.Right, Is.Null);
    }

    [Test]
    public void DeletingItemWithLeftChildDeletesTheItemAndReplacesItWithChildWithoutChangingRestOfTheTree()
    {
        var root = new Node<int>(2);
        root.Left = new Node<int>(1);
        root.Right = new Node<int>(4);
        root.Right.Left = new Node<int>(3);

        var tree = new Tree<int>(root);
        
        tree.Delete(4);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(2));
        Assert.That(tree.Root.Left, Is.Not.Null);
        Assert.That(tree.Root.Left.Data, Is.EqualTo(1));
        Assert.That(tree.Root.Right, Is.Not.Null);
        Assert.That(tree.Root.Right.Data, Is.EqualTo(3));
        Assert.That(tree.Root.Right.Right, Is.Null);
    }
    
    [Test]
    public void DeletingItemWithRightChildDeletesTheItemAndReplacesItWithChildWithoutChangingRestOfTheTree()
    {
        var root = new Node<int>(2);
        root.Left = new Node<int>(1);
        root.Right = new Node<int>(3);
        root.Right.Right = new Node<int>(4);

        var tree = new Tree<int>(root);
        
        tree.Delete(3);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(2));
        Assert.That(tree.Root.Left, Is.Not.Null);
        Assert.That(tree.Root.Left.Data, Is.EqualTo(1));
        Assert.That(tree.Root.Right, Is.Not.Null);
        Assert.That(tree.Root.Right.Data, Is.EqualTo(4));
        Assert.That(tree.Root.Right.Right, Is.Null);
    }

    [Test]
    public void DeletingItemWithTwoChildrenReplacesItWithMaxLeftChild()
    {
        var tree = new Tree<int>();
        tree.Insert(0);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(4);
        tree.Insert(5);
        tree.Insert(6);
        
        // element that will be the max left from 2
        tree.Insert(1);
        
        tree.Delete(2);
        
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root.Data, Is.EqualTo(4));
        Assert.That(tree.Root.Left, Is.Not.Null);
        Assert.That(tree.Root.Left.Data, Is.EqualTo(1));
        Assert.That(tree.Root.Right, Is.Not.Null);
        Assert.That(tree.Root.Right.Data, Is.EqualTo(5));
        Assert.That(tree.Root.Left.Left, Is.Not.Null);
        Assert.That(tree.Root.Left.Left.Data, Is.EqualTo(0));
        Assert.That(tree.Root.Left.Right, Is.Not.Null);
        Assert.That(tree.Root.Left.Right.Data, Is.EqualTo(3));
        Assert.That(tree.Root.Right.Right, Is.Not.Null);
        Assert.That(tree.Root.Right.Right.Data, Is.EqualTo(6));
    }
}