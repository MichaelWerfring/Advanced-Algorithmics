using System.Diagnostics;

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
    
    private Node<T> Insert(Node<T>? node, T key)
    {
        if (node == null)
            return new Node<T>(key);

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
        
        // update height of current node
        node.Height = node.CalculateHeight();
        
        // get balance factor for current node
        int balanceFactor = node.CalculateBalanceFactor();
        
        // Determine Rotation Direction:
        if (balanceFactor > 1 && key.CompareTo(node.Left.Data) < 0)
        {
            // LL
            return node.RotateRight();
        }
        else if (balanceFactor < -1 && key.CompareTo(node.Right.Data) > 0)
        {
            // RR
            return node.RotateLeft();
        }
        else if (balanceFactor > 1 && key.CompareTo(node.Left.Data) > 0)
        {
            // LR
            node.Left = node.Left.RotateLeft(); 
            return node.RotateRight();
        }
        else if (balanceFactor < -1 && key.CompareTo(node.Right.Data) < 0)
        {
            // RL
            node.Right = node.Right.RotateRight(); 
            return node.RotateLeft();
        }
        
        return node;
    }
}