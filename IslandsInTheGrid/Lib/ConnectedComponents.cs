using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
  public class ConnectedComponents<T>
  {
    int[]? visited;

    // we count components starting from 1
    int currentComponent = 0;

    // use a stack and not iteration to avoid stack overflow
    List<T> stack = new List<T>();

    public IEnumerable<int> NodeComponentNumbers => visited ?? throw new InvalidOperationException("Count method must be called before accessing NodeComponentNumbers.");

    public int Count(Graph<T> graph)
    {
      if (graph.NodeCount == 0)
      {
        return 0; // no components in an empty graph
      }

      visited = new Int32[graph.NodeCount];
      // make sure to reset the visited array - we use number 0 as "not visited"
      Array.Fill(visited, 0);

      // get the next unvisited node
      for (int i = 0; i < graph.NodeCount; i++)
      {
        if (visited[i] == 0) // not visited
        {
          // start a new component
          currentComponent++;
          stack.Add(graph.Nodes.ElementAt(i));
          DFS(graph);
        }
      }

      return currentComponent;
    }

    public Dictionary<Graph<T>, int> FindIslands(Graph<T> graph)
    {
      var result = new Dictionary<Graph<T>, int>();
      return result;
    }

    void DFS(Graph<T> graph)
    {
      while (stack.Count > 0)
      {
        T node = stack.Last();

        int nodeIndex = graph.Nodes.ToList().IndexOf(node);
        if (visited![nodeIndex] != 0)
        {
          stack.RemoveAt(stack.Count - 1); // backtrack
          continue;
        }

        visited[nodeIndex] = currentComponent;

        foreach (var neighbor in graph.GetNeighbours(graph.Nodes.ElementAt(nodeIndex)))
        {
          int neighborIndex = graph.Nodes.ToList().IndexOf(neighbor);
          if (visited[neighborIndex] == 0)
          {
            stack.Add(neighbor);
          }
        }
      }
    }

    /// <summary>
    /// get the largest component size
    /// </summary>
    public int LargestComponentSize
    {
      get
      {
        if (visited == null)
        {
          throw new InvalidOperationException("Count method must be called before accessing LargestComponentSize.");
        }

        if (visited.Length == 0)
        {
          return 0; // no components in an empty graph
        }

        return visited.GroupBy(x => x).Max(g => g.Count());
      }
    }
  }
}
