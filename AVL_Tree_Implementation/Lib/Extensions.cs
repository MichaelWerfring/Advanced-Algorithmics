namespace Lib;

public static class Extensions
{
    public static int CalculateHeight<T>(this Node<T> node)
        where T : IComparable<T>
    {
        int leftSubtree = 0;
        int rightSubtree = 0;

        if (node.Left != null)
            leftSubtree = node.Left.Height;

        if (node.Right != null)
            rightSubtree = node.Right.Height;

        return Math.Max(leftSubtree, rightSubtree) + 1;
    }

    public static int CalculateBalanceFactor<T>(this Node<T> node)
        where T : IComparable<T>
    {
        int leftHeight = 0;
        int rightHeight = 0;

        if (node.Left != null)
            leftHeight = node.Left.Height;

        if (node.Right != null)
            rightHeight = node.Right.Height;

        return leftHeight - rightHeight;
    }
    
    public static Node<T> RotateLeft<T>(this Node<T> node)
        where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(node);
                        
        var root = node.Right;
        var temp = root.Left;

        root.Left = node;
        node.Right = temp;
        
        // Update heights
        node.Height = node.CalculateHeight();
        root.Height = root.CalculateHeight();
        
        return root;
    }
    
    public static Node<T> RotateRight<T>(this Node<T> node)
        where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(node);

        var root = node.Left;
        var temp = root.Right;

        root.Right = node;
        node.Left = temp;
        
        // Update heights
        node.Height = node.CalculateHeight();
        root.Height = root.CalculateHeight();
        
        return root;
    }

    public static Node<T> FindMax<T>(this Node<T> node)
        where T : IComparable<T>
    {
        if (node.Right == null)
        {
            return node;
        }

        return node.Right.FindMax();
    }

    public static bool IsLargerThan<T>(this Node<T> node, T key)
        where T : IComparable<T>
    {
        return node.Key.CompareTo(key) > 0;
    }
    
    public static bool IsEqualTo<T>(this Node<T> node, T key)
        where T : IComparable<T>
    {
        return node.Key.CompareTo(key) == 0;
    }
    
    public static bool IsSmallerThan<T>(this Node<T> node, T key)
        where T : IComparable<T>
    {
        return node.Key.CompareTo(key) < 0;
    }
}