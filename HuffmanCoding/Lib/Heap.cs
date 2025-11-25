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

    public T Pop()
    {
        if (_data.Count == 0)
            throw new InvalidOperationException(
                "Cannot retrieve item from heap since it contains no elements.");
        
        // remember element at index 0
        T root = _data[0];
        
        // Replace it with last element
        _data[0] = _data[^1];
        _data[^1] = root;
        _data.RemoveAt(_data.Count - 1);

        if (_data.Count > 0) // Only heapify if anything is left
            HeapifyDown();
        
        return root;
    }

    public T Peek()
    {
        if (_data.Count == 0)
            throw new InvalidOperationException(
                "Cannot retrieve item from heap since it contains no elements.");
        
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

    private void HeapifyDown()
    {
        int index = 0;
        
        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int smallest = index;

            if (left < _data.Count && _data[left].IsSmallerThan(_data[smallest]))
            {
                smallest = left;
            }
            if (right < _data.Count && _data[right].IsSmallerThan(_data[smallest]))
            {
                smallest = right;
            }

            if (smallest != index)
            {
                // While anychild < element: swap with anychild
                (_data[index], _data[smallest]) = (_data[smallest], _data[index]); 
                index = smallest;
            }
            else
            {
                break;
            }
        }
    }
}