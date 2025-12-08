namespace GraphLibrary;

public class GridNode(int x, int y) : IEquatable<GridNode>
{
    public int X { get; private set; } = x;

    public int Y { get; private set; } = y;

    public bool Equals(GridNode? other)
    {
        if (other is null) 
            return false;
        if (ReferenceEquals(this, other)) // ReferenceEquals compares the real References 
            return true; 

        return X == other.X && Y == other.Y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}