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
        
        if (node.Left == null)
            throw new InvalidOperationException("The node cannot be rotated since it has no left child!");
                
        var newRoot = node.Left;
        newRoot.Right = node;
        
        // Remove upwards pointing reference
        node.Left = null;

        // Update heights
        newRoot.Right.Height = newRoot.Right.CalculateHeight();
        newRoot.Height = newRoot.CalculateHeight();
        
        return newRoot;
    }
    
    public static Node<T> RotateRight<T>(this Node<T> node)
        where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(node);

        if (node.Right == null)
            throw new InvalidOperationException("The node cannot be rotated since it has no right child!");
        
        var newRoot = node.Right;
        newRoot.Left = node;
        
        // Remove upwards pointing reference
        node.Right = null;
        
        // Update heights
        newRoot.Left.Height = newRoot.Left.CalculateHeight();
        newRoot.Height = newRoot.CalculateHeight();
        
        return newRoot;
    }
}