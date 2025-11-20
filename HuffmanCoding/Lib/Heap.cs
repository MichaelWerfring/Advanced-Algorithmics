namespace Lib;

public class Heap<T> where T : IComparable<T>
{
    private List<T> _data = [];

    public int Count => this._data.Count;
    
    public void Insert(T newItem)
    {
        _data.Add(newItem);

        if (Count == 1) // First insert
            return;
        
        int index = Count - 1;
        int parentIndex = (index - 1) / 2;
        T parent = _data[parentIndex];
        
        while (parent.IsLargerThan(newItem))
        {
            (_data[parentIndex], _data[index]) = (_data[index], _data[parentIndex]); //Pyhton-Style Swap
            index = parentIndex;
        }
    }

    public void Remove(T item)
    {
        
    }

    public T Peek()
    {
        return _data[0];
    }
}