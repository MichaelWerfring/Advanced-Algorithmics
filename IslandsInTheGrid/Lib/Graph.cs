using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lib
{
  internal struct Edge
  {
    public int Target { get; set; } = -1; // index of the target node, default -1 means not set
    public int Weight { get; set; } = 0; // weight of the edge

    [JsonIgnore]
    public bool IsValid { get; private set; } = false;

    public Edge(int target, int weight)
    {
      Target = target;
      Weight = weight;
      IsValid = true;
    }
  }

  /// <summary>
  /// This is the Graph class. Nodes are of type T. The key for the node is provided with the ToString() method.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class Graph<T>
  {
    /// <summary>
    /// each node has unique index
    /// inside the graph th eindex is used.
    /// </summary>
    List<T> nodes = new();

    public IEnumerable<T> Nodes => nodes;

    /// <summary>
    /// get the number of nodes in the graph
    /// </summary>
    public int NodeCount => nodes.Count;

    /// <summary>
    /// get the number of edges in the graph
    /// </summary>  
    public int EdgeCount
    {
      get
      {
        int count = 0;
        foreach (var edges in adjacencyList.Values)
        {
          count += edges.Count;
        }
        return count;
      }
    }

    /// <summary>
    /// get all edges in the graph as tuples (from, to, weight)
    /// </summary>
    public IEnumerable<(T from, T to, int weight)> GetAllEdges()
    {
      foreach (var fromIndex in adjacencyList.Keys)
      {
        foreach (var edge in adjacencyList[fromIndex])
        {
          yield return (nodes[fromIndex], nodes[edge.Target], edge.Weight);
        }
      }
    }

    Dictionary<int, List<Edge>> adjacencyList = new();

    public Graph()
    {
    }

    /// <summary>
    /// get the neighbours for a given node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public IEnumerable<T> GetNeighbours(T node)
    {
      if (!nodes.Contains(node))
      {
        yield break;
      }

      int index = nodes.IndexOf(node);
      if (adjacencyList.TryGetValue(index, out var neighbors))
      {
        foreach (var neighbor in neighbors)
        {
          yield return nodes[neighbor.Target];
        }
      }
    }

    /// <summary>
    /// get the neighbours for a given node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public IEnumerable<(T, int)> GetNeighboursWithWeight(T node)
    {
      if (!nodes.Contains(node))
      {
        yield break;
      }

      int index = nodes.IndexOf(node);
      if (adjacencyList.TryGetValue(index, out var neighbors))
      {
        foreach (var neighbor in neighbors)
        {
          yield return (nodes[neighbor.Target], neighbor.Weight);
        }
      }
    }

    /// <summary>
    /// add a new edge to the graph. the edge is directed from 'from' to 'to'.
    /// the edge can have an optional weight.
    /// from and to nodes are added to the graph if they do not exist.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="weight"></param>
    public void AddEdge(T from, T to, int weight=1)
    {
      if (!nodes.Contains(from))
      {
        nodes.Add(from);
      }
      if (!nodes.Contains(to))
      {
        nodes.Add(to);
      }

      int fromIndex = nodes.IndexOf(from);
      int toIndex = nodes.IndexOf(to);

      if (fromIndex == toIndex)
        return; // no self-loops allowed

      if (!adjacencyList.ContainsKey(fromIndex))
      {
        adjacencyList[fromIndex] = new List<Edge>();
      }

      // check if the edge already exists
      // if so, only update the weight
      var existingEdge = adjacencyList[fromIndex].Find(e => e.Target == toIndex);
      if ( existingEdge.IsValid && existingEdge.Target == toIndex )
      {
        existingEdge.Weight = weight;
      }
      else
      {
        // if the edge does not exist, add it
        adjacencyList[fromIndex].Add(new Edge(toIndex, weight));
      }
    }

    /// <summary>
    /// simple method to add an undirected edge.
    /// creates two directed edges, one from 'from' to 'to' and one from 'to' to 'from'.
    /// </summary>
    /// <param name="from">one node</param>
    /// <param name="to">the other node</param>
    /// <param name="weight">an optional weight of the edge</param>
    public void AddUndirectedEdge(T from, T to, int weight=1)
    {
      AddEdge(from, to, weight);
      AddEdge(to, from, weight);
    }

    public string GraphToJson()
    {
      string nodes = JsonSerializer.Serialize(this.nodes);
      string edges = JsonSerializer.Serialize(adjacencyList);

      return $"{{\"nodes\": {nodes}, \"edges\": {edges}}}";
    }

    public bool JsonToGraph(string json)
    {
      var jsonObject = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
      if (jsonObject == null)
      {
        return false;
      }

      nodes.Clear();
      adjacencyList.Clear();

      if (jsonObject != null && jsonObject.TryGetValue("nodes", out var nodesElement))
      {
        foreach (var node in nodesElement.EnumerateArray())
        {
          T ?nodeValue = JsonSerializer.Deserialize<T>(node.GetRawText());
          if (nodeValue != null)
          {
            nodes.Add(nodeValue);
          }
        }
      }

      if (jsonObject != null && jsonObject.TryGetValue("edges", out var edgesElement))
      {
        foreach (var edge in edgesElement.EnumerateObject())
        {
          int fromIndex = int.Parse(edge.Name);

          foreach (var neighbor in edge.Value.EnumerateArray())
          {
            Edge edgeData = JsonSerializer.Deserialize<Edge>(neighbor.GetRawText());
            AddEdge(nodes[fromIndex], nodes[edgeData.Target], edgeData.Weight);
          }
        }
      }
      return true;
    }

    public string GraphToDot()
    {
      StringBuilder sb = new();
      sb.AppendLine("digraph G {");
      foreach (var fromIndex in adjacencyList.Keys)
      {
        foreach (var edge in adjacencyList[fromIndex])
        {
          sb.AppendLine($"  \"{nodes[fromIndex]}\" -> \"{nodes[edge.Target]}\" [label={edge.Weight}];");
        }
      }
      sb.AppendLine("}");
      return sb.ToString();
    }

  }
}
