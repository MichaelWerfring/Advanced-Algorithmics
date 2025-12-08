using System.Collections;

namespace GraphLibrary;

public class GridToGraphConverter
{
    public static Graph<GridNode> Convert(char[,] grid, Predicate<char> isValidNode)
    {
        ArgumentNullException.ThrowIfNull(grid);
        ArgumentNullException.ThrowIfNull(isValidNode);


        var graph = new Graph<GridNode>();
        
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (!isValidNode(grid[i, j]))
                    continue;

                var neighbors = GetLandNeighbors(grid, i, j, isValidNode);
                
                if (neighbors.Count == 0)
                    graph.AddUndirectedEdge(new GridNode(i, j), new GridNode(i, j));
                
                foreach (var neighbor in neighbors)
                { 
                    graph.AddUndirectedEdge(new GridNode(i, j), neighbor);
                }
            }
        }

        return graph;
    }

    private static List<GridNode> GetLandNeighbors(char[,] grid, int i, int j, Predicate<char> isValidNode)
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