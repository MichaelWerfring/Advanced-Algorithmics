using System.Net.Mime;
using Lib;

namespace Test;

public class HuffmanCodingTest
{
    [Test]
    public void CountCharactersReturnsEmptyDictionaryOnEmptyText()
    {
        var text = string.Empty;
        
        var charMap = HuffmanCoding.CountCharacters(text);
        
        Assert.That(charMap, Is.Empty);
    }

    [Test]
    public void CountCharactersThrowsArgumentExceptionIfTextIsNull()
    {
        Assert.Catch(() => HuffmanCoding.CountCharacters(null!));
    }
    
    [Test]
    public void CountCharactersReturnsCorrectNumberOfCharactersInText()
    {
        var text = "AABACCDDCABBACCCAABB";
        
        var charMap = HuffmanCoding.CountCharacters(text);

        Assert.That(charMap['A'], Is.EqualTo(7));
        Assert.That(charMap['B'], Is.EqualTo(5));
        Assert.That(charMap['C'], Is.EqualTo(6));
        Assert.That(charMap['D'], Is.EqualTo(2));
    }
    
    [Test]
    public void CountCharactersAlsoCountsLowercaseLetters()
    {
        var text = "bbbaaddabadddddaabbbaac";
        
        var charMap = HuffmanCoding.CountCharacters(text);

        Assert.That(charMap['a'], Is.EqualTo(8));
        Assert.That(charMap['b'], Is.EqualTo(7));
        Assert.That(charMap['c'], Is.EqualTo(1));
        Assert.That(charMap['d'], Is.EqualTo(7));
    }
    
    [Test]
    public void CountCharactersAlsoCountsSpecialCharacters()
    {
        var text = "#AA##$$BACCDDCA$$BBACCCAABB%";
        
        var charMap = HuffmanCoding.CountCharacters(text);

        Assert.That(charMap['A'], Is.EqualTo(7));
        Assert.That(charMap['B'], Is.EqualTo(5));
        Assert.That(charMap['C'], Is.EqualTo(6));
        Assert.That(charMap['D'], Is.EqualTo(2));
        Assert.That(charMap['#'], Is.EqualTo(3));
        Assert.That(charMap['$'], Is.EqualTo(4));
        Assert.That(charMap['%'], Is.EqualTo(1));
    }

    [Test]
    public void CountCharactersThrowsArgumentNullExceptionWhenTextIsNull()
    {
        Assert.Catch<ArgumentNullException>(() => HuffmanCoding.CountCharacters(null!));
    }
    
    [TestCase("")]
    [TestCase("A")]
    [TestCase("AA")]
    [TestCase("AAAAAA")]
    public void BuildTreeWithStringsThatContainNoOrOnlyOneCharacterThrowsArgumentException(string input)
    {
        Assert.Catch<ArgumentException>(() => HuffmanCoding.BuildTree(input));
    }

    [Test]
    public void BuildTreeOnTextReturnsValidTreeAndDoesNotChangeWeightsOfCharacters()
    {
        var text = "AABACCDDCABBACCCAABB";

        var result = HuffmanCoding.BuildTree(text);

        Assert.That(result.Weight, Is.EqualTo(20));
        
        Assert.That(result.Left, Is.Not.Null);
        Assert.That(result.Left.Character, Is.EqualTo('A'));
        Assert.That(result.Left.Weight, Is.EqualTo(7));
        
        Assert.That(result.Right, Is.Not.Null);
        Assert.That(result.Right.Left, Is.Not.Null);
        Assert.That(result.Right.Left.Character, Is.EqualTo('C'));
        Assert.That(result.Right.Left.Weight, Is.EqualTo(6));
        
        Assert.That(result.Right.Right, Is.Not.Null);
        Assert.That(result.Right.Right.Left, Is.Not.Null);
        Assert.That(result.Right.Right.Right, Is.Not.Null);
        
        Assert.That(result.Right.Right.Left.Character, Is.EqualTo('D'));
        Assert.That(result.Right.Right.Left.Weight, Is.EqualTo(2));
        
        Assert.That(result.Right.Right.Right.Character, Is.EqualTo('B'));
        Assert.That(result.Right.Right.Right.Weight, Is.EqualTo(5));
    }
    
    [Test]
    public void GetCharacterMapThrowsArgumentNullExceptionWhenTreeIsNull()
    {
        var codings = new Dictionary<char, string>();
        Assert.Catch(() => HuffmanCoding.GetCharacterMap(null!, codings));
    }
    
    [Test]
    public void GetCharacterMapThrowsArgumentNullExceptionWhenCodingsAreNull()
    {
        Tree tree = new Tree();
        Assert.Catch(() => HuffmanCoding.GetCharacterMap(tree, null!));
    }

    [Test]
    public void GetCharacterMapReturnsCorrectMap()
    {
        string text = "AABACCDDCABBACCCAABB";
        var tree = HuffmanCoding.BuildTree(text);
        Dictionary<char, string> encodings = new Dictionary<char, string>();

        HuffmanCoding.GetCharacterMap(tree, encodings);
        
        Assert.That(encodings.Count, Is.EqualTo(4));
        Assert.That(encodings['A'], Is.EqualTo("0"));
        Assert.That(encodings['C'], Is.EqualTo("10"));
        Assert.That(encodings['B'], Is.EqualTo("111"));
        Assert.That(encodings['D'], Is.EqualTo("110"));
    }
    
    [Test]
    public void EncodeThrowsArgumentExceptionWhenTextIsEmpty()
    {
        Tree encoding = new Tree();
        Assert.Catch<ArgumentException>(() => HuffmanCoding.EncodeString("", encoding));
    }
    
    [Test]
    public void EncodeThrowsArgumentExceptionWhenTextNull()
    {
        Tree encoding = new Tree();
        Assert.Catch<ArgumentException>(() => HuffmanCoding.EncodeString(null!, encoding));
    }
    
    [Test]
    public void EncodeThrowsArgumentNullExceptionWhenEncodingIsNull()
    {
        Assert.Catch<ArgumentNullException>(() => HuffmanCoding.EncodeString("ABCDEFG", null!));
    }

    [Test]
    public void EncodeEncodesTextCorrectly()
    {
        string text = "ABRACADABRA";
        var tree = HuffmanCoding.BuildTree(text);
        
        string result = HuffmanCoding.EncodeString(text, tree);
        
        Assert.That(result, Is.EqualTo("01101001110011110110100"));
    }

    [Test]
    public void EncodeThrowsInvalidOperationExceptionIfTextContainsItemWhichIsNotInTree()
    {
        var text = "ABBBABCBAB"; 
        var tree = new Tree
        {
            Weight = 2,
            // Easiest coding possible 0 and 1
            Left = new Tree { Character = 'A', Weight = 1 }, // 0
            Right = new Tree { Character = 'B', Weight = 1 } // 1
        };
        
        Assert.Throws<InvalidOperationException>(() => HuffmanCoding.EncodeString(text, tree));
    }
}