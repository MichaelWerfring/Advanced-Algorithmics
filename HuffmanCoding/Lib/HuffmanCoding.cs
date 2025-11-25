using System.Text;

namespace Lib;

//TODO: Hide GetCharacterMap, CountCharacters and BuildTree
//TODO: Test with files
//TODO: Test Decode with shitty tree
public static class HuffmanCoding
{
    /// <summary>
    /// Encodes the given string based on the given tree.
    /// </summary>
    /// <param name="text">The text to be encoded.</param>
    /// <param name="tree">The tree representing the encoing.</param>
    /// <returns>The string encoded with 0s and 1s.</returns>
    /// <exception cref="ArgumentException">Is thrown if the text is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Is thrown if the tree is null.</exception>
    /// <exception cref="InvalidOperationException">Is thrown if the string
    /// contains characters that are not in the tree</exception>
    public static string Encode(string text, Tree tree)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        ArgumentNullException.ThrowIfNull(tree);

        var charMap = new Dictionary<char, string>();
        GetCharacterMap(tree, charMap);

        // more performant than += ...
        var sb = new StringBuilder();
        
        foreach (var t in text)
        {
            if (!charMap.ContainsKey(t))
                throw new InvalidOperationException($"The given key {t} was not in the tree.");
            
            sb.Append(charMap[t]);
        }

        return sb.ToString();
    }
    
    /// <summary>
    /// Recursively traverses the Tree to build a mapping of characters to their binary codes.
    /// </summary>
    /// <param name="tree">The current node in the Tree.</param>
    /// <param name="codings">The dictionary to store the character to code mappings.</param>
    /// <param name="current">The current binary code as a string.</param>
    /// <exception cref="ArgumentNullException">Is thrown if the tree or codings are null.</exception>
    public static void GetCharacterMap(Tree tree, Dictionary<char, string> codings, string current = "")
    {
        ArgumentNullException.ThrowIfNull(tree);
        ArgumentNullException.ThrowIfNull(codings);
        
        if (tree.Left == null && tree.Right == null && tree.Character != null) {
            //It is ok convert since it is checked before if char has value
            codings.Add(Convert.ToChar(tree.Character), current);
            return;
        }

        if (tree.Left != null) 
            GetCharacterMap(tree.Left, codings, current + "0");
        
        if (tree.Right != null) 
            GetCharacterMap(tree.Right, codings, current + "1");
    }
    
    /// <summary>
    /// Builds the Tree from the given text by counting character frequencies and combining nodes.
    /// </summary>
    /// <param name="text">The text to build the tree for.</param>
    /// <returns>The root node of the constructed Huffman tree.</returns>
    /// <exception cref="ArgumentException">Is thrown if the text is null or empty,
    /// or contains less than 2 distinct characters.</exception>
    public static Tree BuildTree(string text)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        
        var charMap = CountCharacters(text);

        // Build Queue
        var heap = new Heap<Tree>();
        foreach (var pair in charMap)
        {
            var node = new Tree()
            {
                Character = pair.Key,
                Weight = pair.Value
            };
            
            heap.Insert(node);
        }
        
        if (heap.Count < 2)
        {
            throw new ArgumentException("The given text must contain at least 2 distinct characters.");
        }
        
        // Build Tree from Queue
        while (heap.Count > 1)
        {
            var first = heap.Pop();
            var second = heap.Pop();

            var newNode = new Tree()
            {
                Weight = first.Weight + second.Weight,
                Left = first,
                Right = second
            };
            
            heap.Insert(newNode);
        }

        return heap.Pop();
    }
    
    /// <summary>
    /// Counts the frequency of each character in the given string.
    /// </summary>
    /// <param name="text">The text to count characters from.</param>
    /// <returns>A dictionary mapping each character to its frequency count.</returns>
    /// <exception cref="ArgumentNullException">Is thrown if the text is null.</exception>
    public static Dictionary<char, int> CountCharacters(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        
        var characterMap = new Dictionary<char, int>();

        for (int i = 0; i < text.Length; i++)
        {
            char current = text[i];

            // TryAdd either adds it or returns false if existent
            if (!characterMap.TryAdd(current, 1))
            {
                characterMap[current]++;
            }
        }

        return characterMap;
    }

    public static string Decode(string binary, Tree encoding)
    {
        ArgumentNullException.ThrowIfNull(binary);
        ArgumentNullException.ThrowIfNull(encoding);
        ArgumentException.ThrowIfNullOrEmpty(binary);

        var sb = new StringBuilder();
        var current = encoding; // start with root
        foreach (var item in binary)
        {
            // 0 is Left and 1 is Right
            if (item == '0')
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
            
            if (current.Left == null || current.Right == null)
            {
                sb.Append(current.Character);
                current = encoding; //set back to root
            }
        }
        
        return sb.ToString();
    }
}