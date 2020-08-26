using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Data_Structure_2
{
    public class WeightedGraph
    {
        internal class Node
        {
            public Node(string label)
            {

                Label = label;
            }

            public string Label { get; set; }
            public Dictionary<Node, Edge> Edges { get; private set; } = new Dictionary<Node, Edge>();

            public bool IsConnected(Node to)
            {
                return Edges.ContainsKey(to);
            }

            public void Connect(Node to, int weight)
            {
                if(IsConnected(to)) return;

                Edges.Add(to, new Edge(this, to, weight));
            }

            public void DisConnect(Node to)
            {
                if(!IsConnected(to)) return;

                Edges.Remove(to);
            }

            public override string ToString()
            {
                return $"{Label}";
            }
        }

        internal class Edge
        {
            public Edge(Node from, Node to, int weight)
            {
                From = from;
                To = to;
                Weight = weight;
            }

            public Node From { get; private set; }
            public Node To { get; private set; }
            public int Weight { get; private set; }

            public override string ToString()
            {
                return $"{From}->{To}";
            }
        }

        internal class EntryNode 
        {
            public Node Node { get; private set; }
            public int Priority { get; private set; }

            public EntryNode(Node node, int priority)
            {
                Node = node;
                Priority = priority;
            }
          
        }

        private readonly Dictionary<string, Node> _nodes;
        public WeightedGraph()
        {
            _nodes = new Dictionary<string, Node>();
        }

        public void AddNode(string label)
        {
            if (AnyNullRange(label))
                throw new ArgumentNullException(nameof(label));


            if (_nodes.ContainsKey(label))
            {
                _nodes[label].Label = label;
                return;
            }

            var node = new Node(label);
            _nodes.Add(label, node);
        }

        public void AddEdge(string from, string to, int weight = 0)
        {
            if(AnyNullRange(from,to))
                throw new ArgumentNullException("from or to", "from or to can not be null or empty strings");

            if(!_nodes.ContainsKey(from) || !_nodes.ContainsKey(to))
                throw new ArgumentNullException("from or to", "one of the nodes does not exists");

            _nodes[from].Connect(_nodes[to], weight);
            _nodes[to].Connect(_nodes[from], weight);
        }

        public int GetShortestDistance(string from, string to)
        {
            return GetShortestResult(from, to).Distance;
        }
        public IEnumerable<string> GetShortestPath(string from, string to)
        {
            return GetShortestResult(from, to).Nodes;
        }
        private Path GetShortestResult(string from, string to)
        {
            var distances = _nodes
                .ToDictionary(node => node.Value, node => int.MaxValue);
            distances[_nodes[from]] = 0;

            var visited = new HashSet<Node>();

            var queue = new PriorityHeap<EntryNode>((first, second) => first.Priority < second.Priority);
            queue.Enqueue(new EntryNode(_nodes[from], 0));
            
            var previousNodes = new Dictionary<Node,Node>();

            FindShortest(queue, visited, distances, previousNodes);

            var path = BuildPath(to, previousNodes, distances);

            return path;
        }
        private static void FindShortest(PriorityHeap<EntryNode> queue, HashSet<Node> visited, Dictionary<Node, int> distances, Dictionary<Node, Node> previousNodes)
        {
            while (queue.Size != 0)
            {
                var current = queue.Dequeue().Node;
                visited.Add(current);

                foreach (var (_, edge) in current.Edges)
                {
                    if (visited.Contains(edge.To)) continue;

                    var newDistance = distances[current] + edge.Weight;
                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;

                        if (!previousNodes.ContainsKey(edge.To))
                            previousNodes.Add(edge.To, current);
                        else
                            previousNodes[edge.To] = current;

                        queue.Enqueue(new EntryNode(edge.To, edge.Weight));
                    }
                }
            }
        }
        private Path BuildPath(string to, Dictionary<Node, Node> previousNodes, Dictionary<Node, int> distances)
        {
            var stack = new Stack<Node>();
            stack.Push(_nodes[to]);

            var previous = previousNodes[_nodes[to]];

            while (previous != null)
            {
                stack.Push(previous);
                previous = previousNodes.ContainsKey(previous) ? previousNodes[previous] : null;
            }

            var path = new Path(distances[_nodes[to]]);
            while (stack.Count != 0)
                path.AddNode(stack.Pop().Label);
            return path;
        }

        public bool HasCycle()
        {
            var visited = new HashSet<Node>();

            foreach (var node in _nodes.Values)
                if (!visited.Contains(node) && HasCycle(node, null, visited))
                    return true;

            return false;
        }

        private bool HasCycle(Node current, Node parent, ISet<Node> visited)
        {
            visited.Add(current);

            foreach (var edge in current.Edges.Values)
            {
                if(edge.To == parent) continue;

                if (visited.Contains(edge.To) || HasCycle(edge.To, edge.From, visited)) return true;
            }

            return false;
        }

        public WeightedGraph MinimumSpanning()
        {
            var queue = new PriorityHeap<Edge>((first, second) => first.Weight < second.Weight);
            var visited = new HashSet<Node>();
            var nodesCount = _nodes.Count;

            var current = _nodes.First().Value;
            var spanningTree = new WeightedGraph();
            var edges = 0;

            while (edges != nodesCount - 1)
            {
                spanningTree.AddNode(current.Label);

                visited.Add(current);
                foreach (var edge in current.Edges.Values)
                    if(!visited.Contains(edge.To))
                        queue.Enqueue(edge);

                var nextEdge = queue.Dequeue();
                while (visited.Contains(nextEdge.From) && visited.Contains(nextEdge.To))
                    nextEdge = queue.Dequeue();

                spanningTree.AddNode(nextEdge.To.Label);
                spanningTree.AddEdge(current.Label, nextEdge.To.Label, nextEdge.Weight);
                edges++;
                current = nextEdge.To;
            }
            
            return spanningTree;
        }

        public void Print()
        {
            foreach (var node in _nodes)
            {
                foreach (var (_,edge) in node.Value.Edges)
                {
                    Console.WriteLine(edge);
                }
            }
        }

        private static bool AnyNullRange(params string[] labels)
        {
            return labels.Any(string.IsNullOrWhiteSpace);
        }
    }

    internal class Path
    {
        public int Distance { get; }
        public List<string> Nodes { get; private set; } = new List<string>();

        public Path(int distance)
        {
            Distance = distance;
        }

        public void AddNode(string node)
        {
            Nodes.Add(node);
        }
    }
}
