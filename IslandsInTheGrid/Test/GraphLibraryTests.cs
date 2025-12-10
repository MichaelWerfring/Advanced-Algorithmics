using Lib;

namespace Test
{
  public class GraphLibraryTests
  {

    [Test]
    public void A_new_graph_is_empty()
    {
      Graph<string> uut = new();

      Assert.That(uut.Nodes, Is.Empty);
      Assert.That(uut.NodeCount, Is.EqualTo(0));

      Assert.That(uut.GetNeighbours("A"), Is.Empty);
    }

    [Test]
    public void Adding_Nodes_And_Edges_produces_correct_graph()
    {
      Graph<string> uut = new();
      uut.AddEdge("A", "B", 2);
      uut.AddEdge("A", "C", 3);
      uut.AddEdge("B", "C", 1);
      Assert.That(uut.Nodes, Is.EquivalentTo(new[] { "A", "B", "C" }));
      Assert.That(uut.NodeCount, Is.EqualTo(3));
      Assert.That(uut.GetNeighbours("A"), Is.EquivalentTo(new[] { "B", "C" }));
      Assert.That(uut.GetNeighbours("B"), Is.EquivalentTo(new[] { "C" }));
      Assert.That(uut.GetNeighbours("C"), Is.Empty);
    }

    [Test]
    public void Adding_an_edge_between_existing_nodes_does_not_change_the_graph()
    {
      Graph<string> uut = new();
      uut.AddEdge("A", "B");
      uut.AddEdge("A", "C");
      uut.AddEdge("B", "C");

      // Adding an edge that already exists
      uut.AddEdge("A", "B");

      Assert.That(uut.Nodes, Is.EquivalentTo(new[] { "A", "B", "C" }));
      Assert.That(uut.NodeCount, Is.EqualTo(3));

      Assert.That(uut.GetNeighbours("A"), Is.EquivalentTo(new[] { "B", "C" }));
      Assert.That(uut.GetNeighbours("B"), Is.EquivalentTo(new[] { "C" }));
      Assert.That(uut.GetNeighbours("C"), Is.Empty);
    }

    [Test]
    public void Adding_an_edge_between_one_node_does_not_create_edge_to_itself()
    {
      Graph<string> uut = new();
      uut.AddEdge("A", "A");

      Assert.That(uut.Nodes, Is.EquivalentTo(new[] { "A" }));
      Assert.That(uut.NodeCount, Is.EqualTo(1));
      Assert.That(uut.GetNeighbours("A"), Is.Empty);
    }

    [Test]
    public void A_Graph_produces_valid_json()
    {
      Graph<string> uut = new();
      uut.AddEdge("A", "B", 2);
      uut.AddEdge("A", "C", 3);
      uut.AddEdge("B", "C", 1);

      string json = uut.GraphToJson();
      string expectedJson = "{\"nodes\": [\"A\",\"B\",\"C\"], \"edges\": {\"0\":[{\"Target\":1,\"Weight\":2},{\"Target\":2,\"Weight\":3}],\"1\":[{\"Target\":2,\"Weight\":1}]}}";
      Assert.That(json, Is.EqualTo(expectedJson));
    }

    [Test]
    public void A_json_string_can_be_used_to_create_a_graph()
    {
      string json = "{\"nodes\": [\"A\",\"B\",\"C\"], \"edges\": {\"0\":[{\"Target\":1,\"Weight\":2},{\"Target\":2,\"Weight\":3}],\"1\":[{\"Target\":2,\"Weight\":1}]}}";
      Graph<string> uut = new();
      bool res = uut.JsonToGraph(json);

      Assert.That(res, Is.True);
      Assert.That(uut.Nodes, Is.EquivalentTo(new[] { "A", "B", "C" }));
      Assert.That(uut.NodeCount, Is.EqualTo(3));
      Assert.That(uut.GetNeighbours("A"), Is.EquivalentTo(new[] { "B", "C" }));
      Assert.That(uut.GetNeighbours("B"), Is.EquivalentTo(new[] { "C" }));
      Assert.That(uut.GetNeighbours("C"), Is.Empty);
    }
  }
}