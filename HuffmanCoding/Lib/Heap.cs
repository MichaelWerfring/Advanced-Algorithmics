namespace Lib;

public class Heap<T> where T : IComparable<T>
{
    private List<T> _data = [];

    public int Count => this._data.Count;
    
    public void Insert(T newItem)
    {
        _data.Add(newItem);
        HeapifyUp(newItem);
    }

    public void Remove(T item)
    {
        
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("The heap contains no elements.");
        
        return _data[0];
    }
    
    public List<T> ToList()
    {
        return _data.ToList();
    }

    private void HeapifyUp(T newItem)
    {
        int index = Count - 1;
        
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            T parent = _data[parentIndex];

            if (!parent.IsLargerThan(newItem))
                break;
            
            (_data[parentIndex], _data[index]) = (_data[index], _data[parentIndex]); //Pyhton-Style Swap
            
            index = parentIndex; // Move up 
        }
    }
}