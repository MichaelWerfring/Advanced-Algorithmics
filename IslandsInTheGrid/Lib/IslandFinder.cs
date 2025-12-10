namespace Lib;

public class IslandFinder<T>
{
    // 0 node at index was not visited 
    private int[]? _visited;

    // we count components starting from 1
    private int _currentComponent = 0;

    // use a stack and not iteration to avoid stack overflow
    private readonly List<T> _stack = [];
    
    // is not needed for the assignment but was easy to do
    private readonly Dictionary<int, List<T>> _components = [];
    
    public List<int> FindIslands(Graph<T> graph)
    {
        if (graph == null) 
            throw new ArgumentNullException(nameof(graph));
        
        
        var result = new List<int>();
        _components.Clear();
      
        if (graph.NodeCount == 0)
            return []; // no components in an empty graph

        _visited = new int[graph.NodeCount];
        // make sure to reset the visited array - we use number 0 as "not visited"
        Array.Fill(_visited, 0);

        // get the next unvisited node
        for (var i = 0; i < graph.NodeCount; i++)
        {
            if (_visited[i] == 0) // not visited
            {
                // start a new component
                _currentComponent++;
                _components.Add(_currentComponent, []);
                _stack.Add(graph.Nodes.ElementAt(i));
                
                // Remember size for each Island
                int size = DepthFirstSearch(graph);
                result.Add(size);
            }
        }

        return result;
    }
    
    private int DepthFirstSearch(Graph<T> graph)
    {
        // Keep track of island size while searching
        int currentSize = 0;
        
        while (_stack.Count > 0)
        {
            T node = _stack.Last();
            
            int nodeIndex = graph.Nodes.ToList().IndexOf(node);
            if (_visited![nodeIndex] != 0) // _visited cannot be null since it is never called before initialisation 
            {
                _stack.RemoveAt(_stack.Count - 1); // backtrack, node was already visited
                continue;
            }

            _visited[nodeIndex] = _currentComponent;
            _components[_currentComponent]?.Add(node); // no need to check is always set before calling search

            currentSize++;
            
            foreach (var neighbor in graph.GetNeighbours(node))
            {
                int neighborIndex = graph.Nodes.ToList().IndexOf(neighbor);
                if (_visited[neighborIndex] == 0)
                    _stack.Add(neighbor);
            }
        }

        return currentSize;
    }
}