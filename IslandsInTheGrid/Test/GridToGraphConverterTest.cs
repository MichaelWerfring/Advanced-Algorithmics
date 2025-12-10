using Lib;

namespace Test;

public class GridToGraphConverterTest
{
    [Test]
    public void ConvertThrowsArgumentNullExceptionIfGridIsNull()
    {
        Assert.Catch<ArgumentNullException>(() => GridToGraphConverter.Convert<char>(null!, (c) => true));
    }
    
    [Test]
    public void ConvertThrowsArgumentNullExceptionIfIsIsValidNodeIsNull()
    {
        char[,] grid = { { 'W', 'W', 'L' }, { 'W', 'L', 'L' } };
        Assert.Catch<ArgumentNullException>(() => GridToGraphConverter.Convert<char>(grid,null!));
    }

    [Test] 
    public void ConvertCreatesEmptyGraphIfNoNodeMatchesThePredicate()
    {
        char[,] grid = { { 'W', 'W', 'L' }, { 'W', 'L', 'L' } };
        
        var graph =  GridToGraphConverter.Convert<char>(grid,(c) => false);
        
        Assert.That(graph.EdgeCount, Is.EqualTo(0));
        Assert.That(graph.NodeCount, Is.EqualTo(0));
    }

    [Test]
    public void ConvertCreatesEmptyGraphIfGridIsEmpty()
    {
        char[,] grid = new char[0,0];
        
        var graph =  GridToGraphConverter.Convert<char>(grid,(c) => c == 'L');
        
        Assert.That(graph.EdgeCount, Is.EqualTo(0));
        Assert.That(graph.NodeCount, Is.EqualTo(0));
    }
    
    [Test]
    public void ConvertCreatesEmptyGraphIfGridContainsSingleIsolatedNode()
    {
        char[,] grid = { { 'L' } };
        
        var graph =  GridToGraphConverter.Convert<char>(grid,(c) => c == 'L');
        
        Assert.That(graph.EdgeCount, Is.EqualTo(0)); // graph does not allow self edges
        Assert.That(graph.NodeCount, Is.EqualTo(1));
        var nodes = graph.Nodes.ToArray();
        Assert.That(nodes, Does.Contain(new GridNode(0, 0)));
    }

    [Test]
    public void ConvertCanHandleGridWithValidNodesAtTheEdgesAndCornersOfTheGrid()
    {
        char[,] grid =
        {
            { 'L', 'W', 'L', 'W', 'L'}, 
            { 'W', 'W', 'W', 'W', 'W'},
            { 'L', 'W', 'W', 'W', 'L'},
            { 'W', 'W', 'W', 'W', 'W'},
            { 'L', 'W', 'L', 'W', 'L'},
        };
        
        var graph = GridToGraphConverter.Convert(grid, (c) => c == 'L');
        
        Assert.That(graph.NodeCount, Is.EqualTo(8));

        var nodes = graph.Nodes.ToArray();
        Assert.That(nodes, Does.Contain(new GridNode(0, 0)));
        Assert.That(nodes, Does.Contain(new GridNode(0, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(4, 0)));
        Assert.That(nodes, Does.Contain(new GridNode(4, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(0, 2)));
        Assert.That(nodes, Does.Contain(new GridNode(2, 0)));
        Assert.That(nodes, Does.Contain(new GridNode(2, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(4, 2)));
        
        Assert.That(graph.EdgeCount, Is.EqualTo(0));
    }
    
    [Test]
    public void ConvertCreatesGraphWithCorrectNodesAttachedToTheirNeighborsForSquaredGrid()
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
        
        // Check That the single node in (4, 1) has no in/outgoing edges,
        // due to graph implementation it will not even show the self-loop
        Assert.That(edges.Any(e => e.from.Equals(new GridNode(4, 1)) 
                                   || e.to.Equals(new GridNode(4, 1))), Is.False);
    }
    
    [Test]
    public void ConvertCreatesGraphWithCorrectNodesAttachedToTheirNeighborsFor5X4Grid()
    {
        char[,] grid =
        {
            { 'W', 'L', 'W', 'W', 'L' }, 
            { 'L', 'L', 'W', 'W', 'L' },
            { 'W', 'L', 'W', 'W', 'W' },
            { 'W', 'W', 'W', 'L', 'L' },
        };
        
        var graph =  GridToGraphConverter.Convert(grid,(c) => c == 'L');
        
        Assert.That(graph.NodeCount, Is.EqualTo(8));

        var nodes = graph.Nodes.ToArray();
        Assert.That(nodes, Does.Contain(new GridNode(0, 1)));
        Assert.That(nodes, Does.Contain(new GridNode(1, 1)));
        Assert.That(nodes, Does.Contain(new GridNode(1, 0)));
        Assert.That(nodes, Does.Contain(new GridNode(2, 1)));
        Assert.That(nodes, Does.Contain(new GridNode(0, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(1, 4)));
        Assert.That(nodes, Does.Contain(new GridNode(3, 3)));
        Assert.That(nodes, Does.Contain(new GridNode(3, 4)));

        Assert.That(graph.EdgeCount, Is.EqualTo(10));
        
        var edges = graph.GetAllEdges().ToArray();
        Assert.That(edges, Does.Contain((new GridNode(0, 1), new GridNode(1, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 1), new GridNode(0, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 0), new GridNode(1, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 1), new GridNode(1, 0), 1)));
        Assert.That(edges, Does.Contain((new GridNode(2, 1), new GridNode(1, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 1), new GridNode(2, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(0, 4), new GridNode(1, 4), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 4), new GridNode(0, 4), 1)));


        Assert.That(edges, Does.Contain((new GridNode(3, 3), new GridNode(3, 4), 1)));
        Assert.That(edges, Does.Contain((new GridNode(3, 4), new GridNode(3, 3), 1)));
    }
    
    [Test]
    public void ConvertCreatesGraphWithCorrectNodesAttachedToTheirNeighborsForFor4X5Grid()
    {
        char[,] grid =
        {
            { 'L', 'L', 'W', 'W'},
            { 'W', 'L', 'W', 'W'},
            { 'W', 'W', 'W', 'L'},
            { 'W', 'L', 'W', 'L'},
            { 'W', 'W', 'W', 'W'}
        };
        
        var graph =  GridToGraphConverter.Convert(grid,(c) => c == 'L');
        
        Assert.That(graph.NodeCount, Is.EqualTo(6));

        var nodes = graph.Nodes.ToArray();
        Assert.That(nodes, Does.Contain(new GridNode(0, 0)));
        Assert.That(nodes, Does.Contain(new GridNode(0, 1))); 
        Assert.That(nodes, Does.Contain(new GridNode(1, 1)));
        Assert.That(nodes, Does.Contain(new GridNode(2, 3)));
        Assert.That(nodes, Does.Contain(new GridNode(3, 3)));
        Assert.That(nodes, Does.Contain(new GridNode(3, 1)));

        Assert.That(graph.EdgeCount, Is.EqualTo(6));
        
        var edges = graph.GetAllEdges().ToArray();
        Assert.That(edges, Does.Contain((new GridNode(0, 0), new GridNode(0, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(0, 1), new GridNode(0, 0), 1)));
        Assert.That(edges, Does.Contain((new GridNode(0, 1), new GridNode(1, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(1, 1), new GridNode(0, 1), 1)));
        Assert.That(edges, Does.Contain((new GridNode(2, 3), new GridNode(3, 3), 1)));
        Assert.That(edges, Does.Contain((new GridNode(3, 3), new GridNode(2, 3), 1)));

        // Single node cannot have any edges, due to graph implementation it will not even show the self-loop
        Assert.That(edges.Any(e => e.from.Equals(new GridNode(3, 1)) 
                                   || e.to.Equals(new GridNode(4, 1))), Is.False);
    }
}