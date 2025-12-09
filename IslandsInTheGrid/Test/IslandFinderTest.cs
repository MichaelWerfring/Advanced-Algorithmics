using GraphLibrary;

namespace Test;

public class IslandFinderTest
{
    [Test]
    public void FindIslandsOnEmptyGraphReturnsEmptyList()
    {
        var islandFinder = new IslandFinder<GridNode>();
        var graph = new Graph<GridNode>();

        var islands = islandFinder.FindIslands(graph);
        
        Assert.That(islands, Is.Empty);
    }

    [Test]
    public void FindIslandsOnGraphWithSingleItemReturnsEmptyList()
    {
        var islandFinder = new IslandFinder<GridNode>();
        var graph = new Graph<GridNode>();
        var node = new GridNode(1, 2);
        graph.AddUndirectedEdge(node, node);
        
        var islands = islandFinder.FindIslands(graph);
        
        Assert.That(islands, Has.Count.EqualTo(1));
        Assert.That(islands, Does.Contain(1));
    }

    [Test]
    public void IslandsInLargeGraphAreDetectedCorrectly()
    {
        var islandFinder = new IslandFinder<GridNode>();
        var graph = new Graph<GridNode>();

        // Grid from slides
        var n01 = new GridNode(0, 1);
        var n11 = new GridNode(1, 1);
        var n10 = new GridNode(1, 0);
        var n21 = new GridNode(2, 1);

        graph.AddUndirectedEdge(n01, n11); 
        graph.AddUndirectedEdge(n10, n11); 
        graph.AddUndirectedEdge(n21, n11); 

        var n04 = new GridNode(0, 4);
        var n14 = new GridNode(1, 4);

        graph.AddUndirectedEdge(n04, n14);

        var n33 = new GridNode(3, 3);
        var n34 = new GridNode(3, 4);
        var n43 = new GridNode(4, 3);
        var n44 = new GridNode(4, 4);

        graph.AddUndirectedEdge(n33, n43);
        graph.AddUndirectedEdge(n33, n34);
        graph.AddUndirectedEdge(n44, n43);
        graph.AddUndirectedEdge(n44, n34);

        var n41 = new GridNode(4, 1);
        graph.AddUndirectedEdge(n41, n41);
        
        var islands = islandFinder.FindIslands(graph);
        
        Assert.That(islands, Has.Count.EqualTo(4));
        Assert.That(islands, Has.Exactly(1).EqualTo(1));
        Assert.That(islands, Has.Exactly(1).EqualTo(2));
        Assert.That(islands, Has.Exactly(2).EqualTo(4));
    }
}