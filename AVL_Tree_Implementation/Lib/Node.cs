namespace Lib;

public class Node<T> (T data)
    where T : IComparable<T>
{
    public Node<T>? Left { get; set; }
    
    public Node<T>? Right { get; set; }

    public int Height { get; set; } = 1;

    public T Data { get; set; } = data;
}