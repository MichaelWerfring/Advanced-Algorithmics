using Lib;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void CountCharactersReturnsEmptyDictionaryOnEmptyText()
    {
        var text = string.Empty;
        
        var charMap = Tree.CountCharacters(text);
        
        Assert.That(charMap, Is.Empty);
    }

    [Test]
    public void CountCharactersReturnsCorrectNumberOfCharactersInText()
    {
        var text = "AABACCDDCABBACCCAABB";
        
        var charMap = Tree.CountCharacters(text);

        Assert.That(charMap['A'], Is.EqualTo(7));
        Assert.That(charMap['B'], Is.EqualTo(5));
        Assert.That(charMap['C'], Is.EqualTo(6));
        Assert.That(charMap['D'], Is.EqualTo(2));
    }
    
    [Test]
    public void CountCharactersAlsoCountsLowercaseLetters()
    {
        var text = "bbbaaddabadddddaabbbaac";
        
        var charMap = Tree.CountCharacters(text);

        Assert.That(charMap['a'], Is.EqualTo(8));
        Assert.That(charMap['b'], Is.EqualTo(7));
        Assert.That(charMap['c'], Is.EqualTo(1));
        Assert.That(charMap['d'], Is.EqualTo(7));
    }
    
    [Test]
    public void CountCharactersAlsoCountsSpecialCharacters()
    {
        var text = "#AA##$$BACCDDCA$$BBACCCAABB%";
        
        var charMap = Tree.CountCharacters(text);

        Assert.That(charMap['A'], Is.EqualTo(7));
        Assert.That(charMap['B'], Is.EqualTo(5));
        Assert.That(charMap['C'], Is.EqualTo(6));
        Assert.That(charMap['D'], Is.EqualTo(2));
        Assert.That(charMap['#'], Is.EqualTo(3));
        Assert.That(charMap['$'], Is.EqualTo(4));
        Assert.That(charMap['%'], Is.EqualTo(1));
    }

    [TestCase("")]
    [TestCase("A")]
    [TestCase("AB")]
    public void BuildTreeWithStringsSmallerThanTwoThrowsArgumentException(string input)
    {
        Assert.Catch<ArgumentException>(() => Tree.BuildTree(input));
    }

    [Test]
    public void BuildTreeOnTextReturnsValidTreeAndDoesNotChangeWeightsOfCharacters()
    {
        var text = "AABACCDDCABBACCCAABB";

        var result = Tree.BuildTree(text);

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

    public void EncodeThrowsArgumentExceptionWhenTextIsEmpty()
    {
        Node encoding = new Node();
        Assert.Catch<ArgumentException>(() => Tree.EncodeString("", encoding));
    }
    
    public void EncodeThrowsArgumentExceptionWhenTextNull()
    {
        Node encoding = new Node();
        Assert.Catch<ArgumentException>(() => Tree.EncodeString(null!, encoding));
    }
    
    public void EncodeThrowsArgumentNullExceptionWhenEncodingIsNull()
    {
        Assert.Catch<ArgumentNullException>(() => Tree.EncodeString("ABCDEFG", null!));
    }
}