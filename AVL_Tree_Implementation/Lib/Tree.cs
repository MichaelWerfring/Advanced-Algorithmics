using System.Diagnostics;

namespace Lib;

public class Tree<T> 
    where T : IComparable<T>
{
    public Tree(){ }

    /*
     This ctor is handy for: 
        1) testing and
        2) to create trees from existing trees
    */ 
    public Tree(Node<T> root)
    {
        ArgumentNullException.ThrowIfNull(root);
        Root = root;
    }
        
    // ?. Acces if not null else return null
    // ?? return if not null else return value at right
    public int Height => Root?.Height ?? 0;

    public Node<T>? Root { get; private set; }

    public void Insert(T item)
    {
        Root = Insert(Root, item);
    }
    
    private Node<T> Insert(Node<T>? node, T item)
    {
        if (node == null)
            return new Node<T>(item);

        switch (item.CompareTo(node.Data))
        {
            case 0:
                return node;
            case < 0:
                node.Left = Insert(node.Left, item);
                break;
            default:
                node.Right = Insert(node.Right, item);
                break;
        }

        return node;
    }

    public static Node<T> RotateRight(Node<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);
        
        if (node.Left == null)
            throw new InvalidOperationException("The node cannot be rotated since it has no left child!");
                
        var newRoot = node.Left;
        
        newRoot.Right = node;
        node.Left = null;

        // Node gets moved down by 2 levels
        // newRoot.Right.Height -= 2;
        
        return newRoot;
    }

    public static Node<T> RotateLeft(Node<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);

        if (node.Right == null)
            throw new InvalidOperationException("The node cannot be rotated since it has no right child!");
        
        var newRoot = node.Right;

        newRoot.Left = node;
        node.Right = null;
        
        // Re-calculate heights
        // newRoot.Left.Height = 1 + Math.Max(newRoot.Left.Height, newRoot.Right?.Height ?? 0); 
        // newRoot.Right.Height = 1 + Math.Max(newRoot.Left.Height, newRoot.Right?.Height ?? 0);

        return newRoot;
    }
}