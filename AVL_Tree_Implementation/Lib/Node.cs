namespace Lib;

public class Node<T> (T key)
    where T : IComparable<T>
{
    public Node<T>? Left { get; set; }
    
    public Node<T>? Right { get; set; }

    public int Height { get; set; } = 1;

    public T Key { get; set; } = key;
}