using System.Diagnostics;
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

    public void Insert(T item)
    {
        Root = Insert(Root, item);
    }
    
    public void Delete(T item)
    {
        if (Root != null) 
            Root = Delete(Root, item);
    }
    
    private Node<T> Insert(Node<T>? node, T key)
    {
        if (node == null)
            return new Node<T>(key);

        //TODO: Implement > and == etc. in Node class
        switch (key.CompareTo(node.Data))
        {
            case 0:
                // return as is so items cannot be inserted twice
                return node;
            case < 0:
                node.Left = Insert(node.Left, key);
                break;
            default:
                node.Right = Insert(node.Right, key);
                break;
        }
        
        // Update height of current node
        node.Height = node.CalculateHeight();

        return BalanceTree(node, key);
    }

    private Node<T>? Delete(Node<T> node, T key)
    {
        if (key.CompareTo(node.Data) < 0)// Key is smaller than current
        {
            // Item is not found, do not change the tree
            if (node.Left == null)
                return node;
            
            node.Left = Delete(node.Left, key);
        } 
        else if (key.CompareTo(node.Data) > 0) // Key is larger than current
        {
            // Item is not found, do not change the tree
            if (node.Right == null)
                return node;
            
            node.Right = Delete(node.Right, key);
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
                var newNode = node.Left.FindMax();
                newNode.Left = node.Left;
                newNode.Right = node.Right;
                
                node.Left = null;
                node.Right = null;
                
                return newNode;
            }
        }

        // Update Height
        node.Height = node.CalculateHeight();
        
        // Check BF & rotate
        return BalanceTree(node, key);
    }

    private Node<T> BalanceTree(Node<T> node, T key)
    {
        // Get balance factor for current node
        int balanceFactor = node.CalculateBalanceFactor();

        switch (balanceFactor)
        {
            // Determine Rotation Direction:
            case > 1 when key.CompareTo(node.Left.Data) < 0:
                // LL
                return node.RotateRight();
            case < -1 when key.CompareTo(node.Right.Data) > 0:
                // RR
                return node.RotateLeft();
            case > 1 when key.CompareTo(node.Left.Data) > 0:
                // LR
                node.Left = node.Left.RotateLeft(); 
                return node.RotateRight();
            case < -1 when key.CompareTo(node.Right.Data) < 0:
                // RL
                node.Right = node.Right.RotateRight(); 
                return node.RotateLeft();
            default:
                return node;
        }
    }
}