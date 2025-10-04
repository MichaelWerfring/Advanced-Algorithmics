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
    
    public bool Search(T item)
    {
        if (Root == null)
            return false;

        return Search(Root, item);
    }

    public void Insert(T item)
    {
        Root = Insert(Root, item);
    }
    
    public void Delete(T item)
    {
        if (Root != null) 
            Root = Delete(Root, item);
    }

    private bool Search(Node<T> node, T item)
    {
        if (item.CompareTo(node.Data) == 0)
        {
            return true;
        }
        
        if (item.CompareTo(node.Data) < 0)
        {
            if (node.Left == null)
                return false;
            
            return Search(node.Left, item);
        }
        else
        {
            if (node.Right == null)
                return false;

            return Search(node.Right, item);
        }
    }
    
    private Node<T> Insert(Node<T>? node, T item)
    {
        if (node == null)
            return new Node<T>(item);

        //TODO: Implement > and == etc. in Node class
        switch (item.CompareTo(node.Data))
        {
            case 0:
                // return as is so items cannot be inserted twice
                return node;
            case < 0:
                node.Left = Insert(node.Left, item);
                break;
            default:
                node.Right = Insert(node.Right, item);
                break;
        }
        
        // Update height of current node
        node.Height = node.CalculateHeight();

        return BalanceTree(node);
    }

    private Node<T>? Delete(Node<T> node, T item)
    {
        if (item.CompareTo(node.Data) < 0)// Key is smaller than current
        {
            // Item is not found, do not change the tree
            if (node.Left == null)
                return node;
            
            node.Left = Delete(node.Left, item);
        } 
        else if (item.CompareTo(node.Data) > 0) // Key is larger than current
        {
            // Item is not found, do not change the tree
            if (node.Right == null)
                return node;
            
            node.Right = Delete(node.Right, item);
        }
        else
        {
            // No Children: Just delete
            if (node.Left == null && node.Right == null)
            {
                return null;
            }
            
            // One Child: Node takes place of parent
            if (node.Left != null && node.Right == null)
            {
                // TODO: Do I have to null out node? 
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
                node.Data = leftMax.Data; 
                node.Left = Delete(node.Left, leftMax.Data); // Remove duplicates
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