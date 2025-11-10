using Lib;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int seed = 42;
        var random = new Random(seed);
        int[] insertNumbers = new int[500000];
        int[] searchNumbers = new int[10000];

        // same behavior as array now
        var tempList = new List<int>(insertNumbers.Length);

        // it might take a few seconds to generate the numbers
        int generatedItems = 0;
        Console.WriteLine("Generating numbers...\n");
        while (generatedItems < insertNumbers.Length)
        {
            int randomNumber = random.Next();
            if (!tempList.Contains(randomNumber))
            {
                tempList.Add(randomNumber);
                generatedItems++;
            }
        }

        insertNumbers = tempList.ToArray();

        for (int i = 0; i < searchNumbers.Length; i++)
        {
            // Allows values to occur multiple times
            int randomIndex = random.Next(0, searchNumbers.Length);
            searchNumbers[i] = insertNumbers[randomIndex];
        }
        
        // =================== INSERT ===================
        var tree = new Tree<int>();
        var treeInsertTiming = TimeAction(() =>
        {
            foreach (var num in insertNumbers)
            {
                tree.Insert(num);
            }
        });

        Console.WriteLine($"It took {treeInsertTiming.Milliseconds}ms to Insert {insertNumbers.Length} items into the tree!");
        
        var list = new LinkedList<int>();
        var listInsertTiming = TimeAction(() =>
        {
            foreach (var num in insertNumbers)
            {
                list.AddLast(num);
            }
        });
        
        Console.WriteLine($"It took {listInsertTiming.Milliseconds}ms to Insert {insertNumbers.Length} items into the linked list!");

        var arrayList = new List<int>();
        var arrayListInsertTiming = TimeAction(() =>
        {
            foreach (var num in insertNumbers)
            {
                arrayList.Add(num);
            }
        });

        Console.WriteLine($"It took {arrayListInsertTiming.Milliseconds}ms to Insert {insertNumbers.Length} items into the regular list!\n");
        
        // =================== DELETE ===================
        
        var treeSearchTiming = TimeAction(() =>
        {
            foreach (var num in searchNumbers)
            {
                tree.Contains(num);
            }
        });

        Console.WriteLine($"It took {treeSearchTiming.Milliseconds}ms to search {searchNumbers.Length} items in the tree!");

        var listSearchTiming = TimeAction(() =>
        {
            foreach (var num in searchNumbers)
            {
                list.Contains(num);
            }
        });

        Console.WriteLine($"It took {listSearchTiming.Milliseconds}ms to search {searchNumbers.Length} items in the linked list!");
        
        var arrayListSearchTiming = TimeAction(() =>
        {
            foreach (var num in searchNumbers)
            {
                arrayList.Contains(num);
            }
        });

        Console.WriteLine($"It took {arrayListSearchTiming.Milliseconds}ms to search {searchNumbers.Length} items in the regular list!\n");

        // =================== REMOVE ===================
        
        var treeRemoveTiming = TimeAction(() =>
        {
            foreach (var num in searchNumbers)
            {
                tree.Remove(num);
            }
        });

        Console.WriteLine($"It took {treeRemoveTiming.Milliseconds}ms to delete {searchNumbers.Length} items from the tree!");

        var listRemoveTiming = TimeAction(() =>
        {
            foreach (var num in searchNumbers)
            {
                list.Remove(num);
            }
        });

        Console.WriteLine($"It took {listRemoveTiming.Milliseconds}ms to delete {searchNumbers.Length} items from the linked list!");
        
        var arrayListRemoveTiming = TimeAction(() =>
        {
            foreach (var num in searchNumbers)
            {
                arrayList.Remove(num);
            }
        });

        Console.WriteLine($"It took {arrayListRemoveTiming.Milliseconds}ms to delete {searchNumbers.Length} items from the regular list!");
    }

    static TimeSpan TimeAction(Action action)
    {
        var before = DateTime.Now;
        action();
        return DateTime.Now - before;
    }
}
