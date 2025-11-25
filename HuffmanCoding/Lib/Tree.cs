namespace Lib;

public class Tree: IComparable<Tree>
{
    public int Weight { get; set; }

    public char? Character { get; set; }

    public Tree? Left { get; set; }
    
    public Tree? Right { get; set; }
    
    public int CompareTo(Tree other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return this.Weight.CompareTo(other.Weight);
    }
}