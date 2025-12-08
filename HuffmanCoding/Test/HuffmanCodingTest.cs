using System.Net.Mime;
using Lib;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Test;

public class HuffmanCodingTest
{
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
    public void EncodeThrowsArgumentExceptionWhenTextIsEmpty()
    {
        Tree encoding = new Tree();
        Assert.Catch<ArgumentException>(() => HuffmanCoding.Encode("", encoding));
    }
    
    [Test]
    public void EncodeThrowsArgumentExceptionWhenTextNull()
    {
        Tree encoding = new Tree();
        Assert.Catch<ArgumentException>(() => HuffmanCoding.Encode(null!, encoding));
    }
    
    [Test]
    public void EncodeThrowsArgumentNullExceptionWhenEncodingIsNull()
    {
        Assert.Catch<ArgumentNullException>(() => HuffmanCoding.Encode("ABCDEFG", null!));
    }

    [Test]
    public void EncodeEncodesTextCorrectly()
    {
        string text = "ABRACADABRA";
        var tree = HuffmanCoding.BuildTree(text);
        
        string result = HuffmanCoding.Encode(text, tree);
        
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
        
        Assert.Throws<InvalidOperationException>(() => HuffmanCoding.Encode(text, tree));
    }
    
    [Test]
    public void EncodeThrowsWhenTextContainsNewCharactersNotSeenInBuildTree()
    {
        var tree = HuffmanCoding.BuildTree("ABC");
        Assert.Throws<InvalidOperationException>(() => HuffmanCoding.Encode("ABD", tree));
    }
    
    [Test]
    public void DecodeThrowsArgumentExceptionWhenStringIsEmpty()
    {
        Tree encoding = new Tree { Character = 'A' };

        Assert.Catch(() => HuffmanCoding.Decode(string.Empty, encoding));
    }

    [Test]
    public void DecodeThrowsArgumentNullExceptionWhenStringIsNull()
    {
        Tree encoding = new Tree { Character = 'A' };

        Assert.Catch(() => HuffmanCoding.Decode(null!, encoding));
    }

    [Test]
    public void DecodeThrowsArgumentNullExceptionWhenEncodingIsNull()
    {
        Assert.Catch<ArgumentNullException>(() => HuffmanCoding.Decode("0101", null!));
    }

    [Test]
    public void DecodeThrowsArgumentExceptionWhenStringDoesNotContainValidBinaryCharacters()
    {
        Tree tree = new Tree();
        tree.Left = new Tree { Character = 'A' };
        tree.Right = new Tree { Character = 'B' };

        string input = "01011A"; 
        
        Assert.Catch<ArgumentException>(() => HuffmanCoding.Decode(input, tree));
    }
    
    [Test]
    public void DecodeThrowsInvalidOperationExceptionWhenStringDoesNotMatchTree()
    {
        Tree tree = new Tree();
        tree.Left = new Tree { Character = 'A' };
        tree.Left.Right = new Tree { Character = 'B' };

        string input = "01011"; 
        
        Assert.Catch<InvalidOperationException>(() => HuffmanCoding.Decode(input, tree));
    }

    [Test]
    public void DecodeCorrectlyDecodesString()
    {
        Tree tree = new Tree();
        tree.Left = new Tree { Character = 'A' };
        tree.Right = new Tree { Character = 'B' };

        string input = "01011"; 
        string expected = "ABABB";
        
        string result = HuffmanCoding.Decode(input, tree);
        
        Assert.That(expected, Is.EqualTo(result));
    }
    
    [Test]
    public void EncodingAndThenDecodingReturnsTheOriginalString()
    {
        string text = "ABRACADABRA";
        var tree = HuffmanCoding.BuildTree(text);
        
        string encoded = HuffmanCoding.Encode(text, tree);
        string decoded = HuffmanCoding.Decode(encoded, tree);
        
        Assert.That(text, Is.EqualTo(decoded));
        Assert.That(text, Is.Not.EqualTo(encoded));
    }
}