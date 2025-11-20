namespace Lib;

public static class Extensions
{
    public static bool IsEqualTo<T>(this T self, T other) where T : IComparable<T>
    {
        return self.CompareTo(other) == 0;
    }
    
    public static bool IsLargerThan<T>(this T self, T other) where T : IComparable<T>
    {
        return self.CompareTo(other) > 0;
    }
    
    public static bool IsSmallerThan<T>(this T self, T other) where T : IComparable<T>
    {
        return self.CompareTo(other) < 0;
    }
}