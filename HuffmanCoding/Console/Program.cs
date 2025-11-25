// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Lib;

if (args.Length == 0)
{
    DisplayHelp();
    return;
}

string operation = args[0];

if (operation == "help")
{
    DisplayHelp();
}
else if (operation == "encode")
{
    if (args.Length != 2)
    {
        Console.WriteLine("Please specify a valid file path.");
        DisplayHelp();
        return;
    }
    
    try
    {
        EncodeFile(args[1]);
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine(e);
    }
}
else if (operation == "decode")
{
    if (args.Length != 3) // Tree needs to be specified as well
    {
        Console.WriteLine("Please specify a valid file path.");
        DisplayHelp();
        return;
    }

    try
    {
        DecodeFile(args[1], args[2]);
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine(e);
    }
}

void DisplayHelp()
{
    Console.WriteLine("Huffman Coding Tool");
    Console.WriteLine("-------------------");
    Console.WriteLine("Usage:");
    Console.WriteLine("  [program] <command> <arguments>");
    Console.WriteLine();
        
    Console.WriteLine("Commands:");
    Console.WriteLine("  encode <filePath>");
    Console.WriteLine("      Compresses a text file.");
    Console.WriteLine("      Generates two files: .bin (content) and .json (tree).");
    Console.WriteLine();
        
    Console.WriteLine("  decode <binPath> <treePath>");
    Console.WriteLine("      Decompresses a binary file using the specified JSON tree.");
    Console.WriteLine("      Generates a .txt file.");
    Console.WriteLine();
        
    Console.WriteLine("  help");
    Console.WriteLine("      Shows this help message.");
    Console.WriteLine();
        
    Console.WriteLine("Examples:");
    Console.WriteLine("  HuffmanCoding.exe encode myBook.txt");
    Console.WriteLine("  HuffmanCoding.exe myBook.bin myBook.json");
}

    void EncodeFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new InvalidOperationException($"The file: {filePath} does not exist!");
    
        string directoryName = Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException();
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string text;
        
        try
        {
            text = File.ReadAllText(filePath);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error reading file: " + e.Message);
        }
        
        if(text == string.Empty)
            throw new InvalidOperationException("The given file must not be empty!");
        
        var tree = HuffmanCoding.BuildTree(text);
        string encoded = HuffmanCoding.Encode(text, tree);
    
        string binPath = Path.Combine(directoryName, fileName + ".bin");
    
        string treePath = Path.Combine(directoryName, fileName + ".json");
        string treeJson = JsonSerializer.Serialize(tree, 
            // WriteIntended does pretty printing 
            new JsonSerializerOptions { WriteIndented = true });
    
        try
        {
            File.WriteAllText(binPath, encoded);
            File.WriteAllText(treePath, treeJson);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error writing file: " + e.Message);
        }
    }

    void DecodeFile(string binPath, string treePath)
    {
        if (!File.Exists(binPath))
            throw new InvalidOperationException($"The file: {binPath} does not exist!");
    
        if (!File.Exists(treePath))
            throw new InvalidOperationException($"The file: {treePath} does not exist!");
        
        string encodedContent; 
        string treeJson; 

        try
        {
            encodedContent = File.ReadAllText(binPath);
            treeJson = File.ReadAllText(treePath);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error reading file: " + e.Message);
        }
        
        Tree? tree;

        try
        {
            tree = JsonSerializer.Deserialize<Tree>(treeJson);
        }
        catch (JsonException e)
        {
            throw new InvalidOperationException("Error reading tree data: " + e.Message);
        }
        
        if (tree == null)
            throw new InvalidOperationException("The tree file contained no valid JSON data.");

        string decodedText = HuffmanCoding.Decode(encodedContent, tree);

        string directory = Path.GetDirectoryName(binPath) 
                           ?? throw new InvalidOperationException("Could not determine directory.");
        
        string fileNameWithoutBin = Path.GetFileNameWithoutExtension(binPath);
        string outputPath = Path.Combine(directory, fileNameWithoutBin + ".txt");

        try
        {
            File.WriteAllText(outputPath, decodedText);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error writing file: " + e.Message);
        }
    }