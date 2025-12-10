namespace Lib;

public static class GridToGraphConverter
{
    
    /// <summary>
    /// Converts the given grid to a graph where all the neighboring grid cells are neighbors in the graph.
    /// </summary>
    /// <param name="grid">The grid to be converted.</param>
    /// <param name="isValidNode">A predicated to decide whether a cell should be taken into the graph.</param>
    /// <returns>A graph representing the given grid only with applicable nodes.</returns>
    /// <exception cref="ArgumentNullException">If either the grid or the predicate is null.</exception>
    public static Graph<GridNode> Convert<T>(T[,] grid, Predicate<T> isValidNode)
    {
        if (grid == null) 
            throw new ArgumentNullException(nameof(grid));

        if (isValidNode == null) 
            throw new ArgumentNullException(nameof(isValidNode));

        var graph = new Graph<GridNode>();
        
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (!isValidNode(grid[i, j]))
                    continue;

                var neighbors = GetValidNeighbors(grid, i, j, isValidNode);
                
                if (neighbors.Count == 0) // Self edges are not allowed but node is still created
                    graph.AddUndirectedEdge(new GridNode(i, j), new GridNode(i, j)); 
                
                foreach (var neighbor in neighbors)
                { 
                    graph.AddUndirectedEdge(new GridNode(i, j), neighbor);
                }
            }
        }

        return graph;
    }

    private static List<GridNode> GetValidNeighbors<T>(T[,] grid, int i, int j, Predicate<T> isValidNode)
    {
        var neighbors = new List<GridNode>();

        if (i + 1 < grid.GetLength(0) && isValidNode(grid[i+1, j]))
            neighbors.Add(new GridNode(i+1, j));
        if (i - 1 >= 0 && isValidNode(grid[i-1, j]))
            neighbors.Add(new GridNode(i-1, j));
        if (j + 1 < grid.GetLength(1) && isValidNode(grid[i, j + 1]))
            neighbors.Add(new GridNode(i, j+1));
        if (j - 1 >= 0 && isValidNode(grid[i, j-1]))
            neighbors.Add(new GridNode(i, j-1));

        return neighbors;
    }
}