using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
  internal class ConnectedComponentsTest
  {
    [Test]
    public void Number_Of_Components_In_Empty_Graph_Is_Zero()
    {
      Graph<string> graph = new();
      ConnectedComponents<string> cc = new();
      int components = cc.Count(graph);
      Assert.That(components, Is.EqualTo(0));
    }

    [Test]
    public void Number_Of_Components_In_Single_Node_Graph_Is_One()
    {
      Graph<string> graph = new();
      graph.AddEdge("A", "A");

      ConnectedComponents<string> cc = new();
      int components = cc.Count(graph);
      Assert.That(components, Is.EqualTo(1));
    }

    [Test]
    public void Sample_Graph_from_slides_is_detected_correctly()
    {
      Graph<string> graph = new();
      graph.AddUndirectedEdge("3", "3");
      graph.AddUndirectedEdge("4", "6");
      graph.AddUndirectedEdge("5", "6");
      graph.AddUndirectedEdge("6", "7");
      graph.AddUndirectedEdge("6", "8");
      graph.AddUndirectedEdge("1", "2");

      ConnectedComponents<string> cc = new();
      int components = cc.Count(graph);
      Assert.That(components, Is.EqualTo(3));
    }

    [Test]
    public void If_Count_Is_Not_Called_We_Get_Exception()
    {
      Graph<string> graph = new();
      ConnectedComponents<string> cc = new();
      int x = 0;

      Assert.Throws<InvalidOperationException>(() => x = cc.LargestComponentSize);
    }

    [Test]
    public void Largest_Component_Size_Is_Correct()
    {
      Graph<string> graph = new();
      graph.AddUndirectedEdge("3", "3");
      graph.AddUndirectedEdge("4", "6");
      graph.AddUndirectedEdge("5", "6");
      graph.AddUndirectedEdge("6", "7");
      graph.AddUndirectedEdge("6", "8");
      graph.AddUndirectedEdge("1", "2");

      ConnectedComponents<string> cc = new();
      int components = cc.Count(graph);

      Assert.That(components, Is.EqualTo(3));
      Assert.That(cc.LargestComponentSize, Is.EqualTo(5)); // The largest component has 5 nodes
    }
  }
}
