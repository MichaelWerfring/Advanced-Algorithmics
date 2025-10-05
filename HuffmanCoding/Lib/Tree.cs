namespace Lib;

public class Tree
{
    public static Dictionary<char, int> CountCharacters(string text)
    {
        //TODO: Exception Handling
        var characterMap = new Dictionary<char, int>();

        for (int i = 0; i < text.Length; i++)
        {
            char current = text[i];

            // Try Add either adds it or returns false if existant
            if (!characterMap.TryAdd(current, 1))
            {
                characterMap[current]++;
            }
        }

        return characterMap;
    }

    public static Node BuildTree(string text)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        
        //TODO: Maybe put queue build and counting in one step for performance?
        var charMap = CountCharacters(text);

        // Build Queue
        var queue = new PriorityQueue<Node, int>(charMap.Count);
        foreach (var pair in charMap)
        {
            var node = new Node()
            {
                Character = pair.Key,
                Weight = pair.Value
            };
            
            queue.Enqueue(node, pair.Value);
        }

        // TODO: Handle case where Queue count < 2
        // Build Tree from Queue
        while (queue.Count > 1)
        {
            var first = queue.Dequeue();
            var second = queue.Dequeue();

            var newNode = new Node()
            {
                Weight = first.Weight + second.Weight,
                Left = first,
                Right = second
            };
            
            queue.Enqueue(newNode, newNode.Weight);
        }

        return queue.Dequeue();
    }

    public static byte[] EncodeString(string text, Node encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        ArgumentNullException.ThrowIfNull(encoding);
        
        return [];
    }
}