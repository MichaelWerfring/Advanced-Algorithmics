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