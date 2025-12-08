namespace GraphLibrary;

public class GridToGraphConverterTest
{
    [Test]
    public void ConvertThrowsArgumentNullExceptionIfGridIsNull()
    {
        Assert.Catch<ArgumentNullException>(() => GridToGraphConverter.Convert(null!, (c) => true));
    }
    
    [Test]
    public void ConvertThrowsArgumentNullExceptionIfIsIsValidNodeIsNull()
    {
        char[,] grid = { { 'W', 'W', 'L' }, { 'W', 'L', 'L' } };
        Assert.Catch<ArgumentNullException>(() => GridToGraphConverter.Convert(grid,null!));
    }

    [Test] public void ConvertCreatesEmptyGraphIfNoNodeMatchesThePredicate()
    {
        char[,] grid = { { 'W', 'W', 'L' }, { 'W', 'L', 'L' } };
        
        var graph =  GridToGraphConverter.Convert(grid,(c) => false);
        
        Assert.That(graph.EdgeCount, Is.EqualTo(0));
        Assert.That(graph.NodeCount, Is.EqualTo(0));
    }
    
    [Test]
    public void ConvertCreatesGraphWithCorrectNodesAttachedToTheirNeighbors()
    {
        char[,] grid =
        {
            { 'W', 'L', 'W', 'W', 'L', 'W' }, 
            { 'L', 'L', 'W', 'W', 'L', 'W' },
            { 'W', 'L', 'W', 'W', 'W', 'W' },
            { 'W', 'W', 'W', 'L', 'L', 'W' },
            { 'W', 'L', 'W', 'L', 'L', 'W' },
            { 'W', 'W', 'W', 'W', 'W', 'W' }
        };
        
        var graph =  GridToGraphConverter.Convert(grid,(c) => c == 'L');
        
        Assert.That(graph.NodeCount, Is.EqualTo(11));

        var nodes = graph.Nodes.ToArray();
        Assert.That(nodes, Does.Contain(new GridNode(0, 1)));
        Assert.That(nodes, Does.Contain(new GridNode(1, 1)));
        Assert.That(nodes, Does.Contain(new GridNode(1, 0)));
        Assert.That(nodes, Does.Contain(new GridNode(2, 1)));
        Assert.That(nodes, Does.Contain(new GridNode(0, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(1, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(3, 3)));
        Assert.That(nodes, Does.Contain(new GridNode(3, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(4, 3)));
        Assert.That(nodes, Does.Contain(new GridNode(4, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(4, 1)));

        Assert.That(graph.EdgeCount, Is.EqualTo(16));
        
        var edges = graph.GetAllEdges().ToArray();
        Assert.That(edges, Does.Contain((new GridNode(0, 1), new GridNode(1, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 1), new GridNode(0, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 0), new GridNode(1, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 1), new GridNode(1, 0), 1)));
        Assert.That(edges, Does.Contain((new GridNode(2, 1), new GridNode(1, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 1), new GridNode(2, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(0, 4), new GridNode(1, 4), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 4), new GridNode(0, 4), 1)));
        Assert.That(edges, Does.Contain((new GridNode(3, 3), new GridNode(4, 3), 1)));
        Assert.That(edges, Does.Contain((new GridNode(4, 3), new GridNode(3, 3), 1)));
        Assert.That(edges, Does.Contain((new GridNode(3, 3), new GridNode(3, 4), 1)));
        Assert.That(edges, Does.Contain((new GridNode(3, 4), new GridNode(3, 3), 1)));
        Assert.That(edges, Does.Contain((new GridNode(4, 3), new GridNode(4, 4), 1)));
        Assert.That(edges, Does.Contain((new GridNode(4, 4), new GridNode(4, 3), 1)));
        Assert.That(edges, Does.Contain((new GridNode(3, 4), new GridNode(4, 4), 1)));
        Assert.That(edges, Does.Contain((new GridNode(4, 4), new GridNode(3, 4), 1)));
        
        // Check That the single node in (4, 1) has no in/outgoing edges
        Assert.That(edges.Any(e => e.from.Equals(new GridNode(4, 1)) 
                                   || e.to.Equals(new GridNode(4, 1))), Is.False);
    }
}