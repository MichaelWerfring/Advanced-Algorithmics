using Lib;

namespace Test;

public class HeapTest
{
    [Test]
    public void NewHeapDoesNotContainAnyElements()
    {
        var heap = new Heap<int>();
        List<int> expected = [];
        
        Assert.That(heap.ToList(), Is.EqualTo(expected));
    }

    [Test]
    public void NewHeapHasCountZero()
    {
        var heap = new Heap<int>();
        
        Assert.That(heap.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void InsertingOneItemAddsItemToHeap()
    {
        var heap = new Heap<int>();
        List<int> expected = [1]; 
        
        heap.Insert(1);
        
        Assert.That(heap.ToList(), Is.EqualTo(expected));
    }

    [Test]
    public void InsertingMultipleItemsIntoHeapOrdersTheItemsCorrectly()
    {
        var heap = new Heap<int>();
        List<int> expected = [3, 17, 7, 26, 92, 38, 8, 87, 57];
        
        foreach (var item in expected)
        {
            heap.Insert(item);
        }

        var result = heap.ToList();
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(IsHeapPropertyFulfilled<int>(result));
    }

    [Test]
    public void InsertingDuplicateIntoHeapDoesInsertTheDuplicateCorrectly()
    {
        var heap = new Heap<int>();
        List<int> inserts = [3, 17, 7, 26, 38, 8, 87, 57, 92];
        List<int> expected = [3, 17, 7, 26, 17, 8, 87, 57, 92, 38];

        foreach (var item in inserts)
        {
            heap.Insert(item);
        }
        
        heap.Insert(17);
        
        var result = heap.ToList();
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(IsHeapPropertyFulfilled<int>(result));
    }

    [Test]
    public void PopThrowsInvalidOperationExceptionOnEmptyHeap()
    {
        var heap = new Heap<int>();
        Assert.Catch<InvalidOperationException>(() => heap.Pop());
    }
    
    [Test]
    public void PopRemovesItemFromHeapWithSingleElement()
    {
        var heap = new Heap<int>();
        heap.Insert(17);

        int item = heap.Pop();
        
        Assert.That(item, Is.EqualTo(17));
        Assert.That(heap.Count, Is.EqualTo(0));
    }

    [Test]
    public void PopRemovesMinimumItemFromHeapWithMultipleElements()
    {
        var heap = new Heap<int>();
        heap.Insert(17);
        heap.Insert(7);
        heap.Insert(27);

        int initialCount = heap.Count;
        int item = heap.Pop();
        
        Assert.That(item, Is.EqualTo(7));
        Assert.That(heap.Count, Is.EqualTo(initialCount - 1));
        Assert.That(IsHeapPropertyFulfilled<int>(heap.ToList()));
    }
    
    [Test]
    public void PopRemovesMultipleItemsFromHeapInCorrectOrder()
    {
        var heap = new Heap<int>();
        heap.Insert(47);
        heap.Insert(17);
        heap.Insert(37);
        heap.Insert(27);
        heap.Insert(7);

        int count = heap.Count;
        var results = new List<int>();
        for (int i = 0; i < count; i++)
        {
            results.Add(heap.Pop());
        }
        
        Assert.That(results, Is.EqualTo(new List<int>([7,17,27,37,47])));
    }
    
    [Test]
    public void PopRemovesDuplicatesFromHeap()
    {
        var heap = new Heap<int>();
        heap.Insert(17);
        heap.Insert(17);
        heap.Insert(7);

        int count = heap.Count;
        var results = new List<int>();
        for (int i = 0; i < count; i++)
        {
            results.Add(heap.Pop());
        }
        
        Assert.That(results, Is.EqualTo(new List<int>([7,17,17])));
    }
    
    [Test]
    public void PeekThrowsInvalidOperationExceptionOnEmptyHeap()
    {
        var heap = new Heap<int>();
        Assert.Catch<InvalidOperationException>(() => heap.Peek());
    }
    
    [Test]
    public void PeekReturnsFirstItemOfHeapButDoesNotChangeIt()
    {
        var heap = new Heap<int>();
        List<int> expected = [3, 17, 7, 26, 92, 38, 8, 87, 57];
        
        foreach (var item in expected)
        {
            heap.Insert(item);
        }
        
        var first = heap.Peek();
        
        var result = heap.ToList();
        Assert.That(first, Is.EqualTo(3));
        Assert.That(result, Is.EqualTo(expected));
        Assert.That(IsHeapPropertyFulfilled<int>(result));
    }
    
    private bool IsHeapPropertyFulfilled<T>(List<T> list) where T : IComparable<T>
    {
        for (int i = 0; i < list.Count; i++)
        {
            T current = list[i];
            int parentIndex = (i - 1) / 2;
            T parent = list[parentIndex];
            
            if (parent.IsLargerThan(current))
                return false;
        }

        return true;
    }
}