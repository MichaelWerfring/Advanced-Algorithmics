using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Lib;

public class Tree<T> 
    where T : IComparable<T>
{
    public Tree(){ }

    /*
     This ctor is handy for: 
        1) testing and
        2) creating trees from existing trees
    */ 
    public Tree(Node<T> root)
    {
        ArgumentNullException.ThrowIfNull(root);
        Root = root;
    }
        
    // ?. Access if not null else return null
    // ?? return if not null else return value at right
    public int Height => Root?.CalculateHeight() ?? 0;

    public Node<T>? Root { get; private set; }
    
    public bool Contains(T item)
    {
        if (Root == null)
            return false;

        return Contains(Root, item);
    }

    public void Insert(T item)
    {
        Root = Insert(Root, item);
    }
    
    public void Remove(T item)
    {
        if (Root != null) 
            Root = Remove(Root, item);
    }

    private bool Contains(Node<T> node, T item)
    {
        if (node.IsEqualTo(item))
        {
            return true;
        }
        
        if (node.IsLargerThan(item))
        {
            if (node.Left == null)
                return false;
            
            return Contains(node.Left, item);
        }
        else
        {
            if (node.Right == null)
                return false;

            return Contains(node.Right, item);
        }
    }
    
    private Node<T> Insert(Node<T>? node, T item)
    {
        if (node == null)
            return new Node<T>(item);

        if (node.IsEqualTo(item))
        {
            return node;
        }

        if (node.IsLargerThan(item))
        {
            node.Left = Insert(node.Left, item);
        }

        if (node.IsSmallerThan(item))
        {
            node.Right = Insert(node.Right, item);
        }
        
        // Update height of current node
        node.Height = node.CalculateHeight();

        return BalanceTree(node);
    }

    private Node<T>? Remove(Node<T> node, T item)
    {
        if (node.IsLargerThan(item))
        {
            // Item is not found, do not change the tree
            if (node.Left == null)
                return node;
            
            node.Left = Remove(node.Left, item);
        } 
        else if (node.IsSmallerThan(item))
        {
            // Item is not found, do not change the tree
            if (node.Right == null)
                return node;
            
            node.Right = Remove(node.Right, item);
        }
        else // Item found, needs to be deleted
        {
            // No Children: Just delete
            if (node.Left == null && node.Right == null)
            {
                return null;
            }
            
            // One Child: Node takes place of parent
            if (node.Left != null && node.Right == null)
            {
                return node.Left;
            }
            if (node.Left == null && node.Right != null)
            {
                return node.Right;
            }
            
            // Two Children: replace either with max of left or min of right
            if (node.Left != null && node.Right != null)
            {
                var leftMax = node.Left.FindMax();
                node.Key = leftMax.Key; 
                node.Left = Remove(node.Left, leftMax.Key); // Remove duplicates
            }
        }

        // Update Height
        node.Height = node.CalculateHeight();
        
        // Check BF & rotate
        return BalanceTree(node);
    }

    private Node<T> BalanceTree(Node<T> node)
    {
        if (node.CalculateBalanceFactor() > 1) // Left Subtree to tall
        {
            if (node.Left.CalculateBalanceFactor() >= 0) // check if grandchild is at left
            {
                //Left-Left case needs Right Rotation
                return node.RotateRight();
            }
            
            //Left-Right case needs Left-Right rotation
            node.Left = node.Left.RotateLeft();
            return node.RotateRight();
        }
        else if (node.CalculateBalanceFactor() < -1) // Right subtree to tall
        {
             if (node.Right.CalculateBalanceFactor() <= 0) // check if grandchild is at right
             {
                 // Right-Right case needs Left Rotation
                 return node.RotateLeft();
             }
             
             // Right-Left case needs Right-Left rotations
             node.Right = node.Right.RotateRight();
             return node.RotateLeft();
        }

        return node;
    }
}