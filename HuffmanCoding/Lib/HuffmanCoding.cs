using System.Text;

namespace Lib;

//TODO: Test with files
public static class HuffmanCoding
{
    /// <summary>
    /// Encodes the given string based on the given tree.
    /// </summary>
    /// <param name="text">The text to be encoded.</param>
    /// <param name="tree">The tree representing the encoding.</param>
    /// <returns>The string encoded with 0s and 1s.</returns>
    /// <exception cref="ArgumentException">Is thrown if the text is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Is thrown if the encoding is null.</exception>
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
    /// Decodes the binary string based on the given encoding. 
    /// </summary>
    /// <param name="binary">A string containing only the binary encoded text.</param>
    /// <param name="encoding">The tree representing the encoding</param>
    /// <returns>The decoded text.</returns>
    /// <exception cref="ArgumentException">Is thrown if the binary is empty.</exception>
    /// <exception cref="ArgumentException">Is thrown if the binary contains
    /// characters other than 0s and 1s.</exception>
    /// <exception cref="ArgumentNullException">Is thrown if the binary or the encoding is null.</exception>
    public static string Decode(string binary, Tree encoding)
    {
        ArgumentNullException.ThrowIfNull(binary);
        ArgumentNullException.ThrowIfNull(encoding);
        ArgumentException.ThrowIfNullOrEmpty(binary);

        var sb = new StringBuilder();
        var current = encoding; // start with root
        foreach (var item in binary)
        {
            if (item == '0')
                current = current.Left;
            else if(item == '1')
                current = current.Right;
            else
                throw new ArgumentException("The binary string must only contain 0s and 1s");

            if (current == null)
                throw new InvalidOperationException(
                    "The binary string contained characters that cannot be handled with the given encoding");
            
            if (current.Left == null || current.Right == null)
            {
                sb.Append(current.Character);
                current = encoding; //set back to root & restart searching
            }
        }
        
        return sb.ToString();
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
    /// Recursively traverses the Tree to build a mapping of characters to their binary codes.
    /// </summary>
    /// <param name="tree">The current node in the Tree.</param>
    /// <param name="codings">The dictionary to store the character to code mappings.</param>
    /// <param name="current">The current binary code as a string.</param>
    /// <exception cref="ArgumentNullException">Is thrown if the tree or codings are null.</exception>
    private static void GetCharacterMap(Tree tree, Dictionary<char, string> codings, string current = "")
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
    
    private static Dictionary<char, int> CountCharacters(string text)
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
}