namespace Lib;

public class Tree<T>(T data)
{
    public Tree<T>? Left { get; set; } = null;
    
    public Tree<T>? Right { get; set; } = null;

    public T Data { get; set; } = data;

    public int GetHeight()
    {
        if (Left == null && Right != null)
            return Right.GetHeight() + 1;

        if (Right == null && Left != null)
            return Left.GetHeight() + 1;
        
        // no children -> last element
        if(Left == null && Right == null)
            return 0;
        
        return Math.Max(Left.GetHeight(), Right.GetHeight()) + 1;
    }

    public int GetBalanceFactor()
    {
        int heightLeft = 0;
        int heightRight = 0;
        
        if (Left != null)
            heightLeft  = Left.GetHeight() + 1;
        
        if (Right != null)
            heightRight  = Right.GetHeight() + 1;


        return heightLeft - heightRight;
    }
}