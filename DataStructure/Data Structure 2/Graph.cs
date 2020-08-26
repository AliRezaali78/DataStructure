using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Data_Structure_2
{
    public class Graph<T>
    {
        internal class Node
        {
             internal T Label { get; private set; }
             internal Dictionary<T,Node> Nodes { get; private set; } = new Dictionary<T,Node>();

             public Node(T label)
             {
                 Label = label;
             }

             public bool IsConnected(T to)
             {
                 return Nodes.ContainsKey(to);
             }

             public void Connect(Node to)
             {
                 if(IsConnected(to.Label)) return;

                 Nodes.Add(to.Label, to);
             }

             public void DisConnect(T to)
             {
                 if(!IsConnected(to)) return;

                 Nodes.Remove(to);
             }
        }

        private readonly Dictionary<T, Node> _nodes = new Dictionary<T, Node>();

        public void AddNode(T label)
        {
            if (IsNull(label)) throw new ArgumentNullException(nameof(label));

            _nodes.TryAdd(label, new Node(label));
        }

        public void AddEdge(T from, T to)
        {
            if (IsNull(from) || IsNull(to)) throw new ArgumentNullException("from or to is null");

            if (!NodeExists(from) || !NodeExists(to))
                throw new ArgumentException("One of the node does not exists");

            _nodes[from].Connect(_nodes[to]);
        }

        public void RemoveNode(T label)
        {
            if (IsNull(label)) throw new ArgumentNullException(nameof(label));

            if (!NodeExists(label)) return;

            foreach (var node in _nodes)
                node.Value.DisConnect(label);

            _nodes.Remove(label);
        }

        public void RemoveEdge(T from, T to)
        {
            if (IsNull(from) || IsNull(to)) throw new ArgumentNullException("from or to is null");

            if (!NodeExists(from) || !NodeExists(to))
                throw new ArgumentException("One of the node does not exists");
           
            _nodes[from].DisConnect(to);
        }

        public void DepthFirstTraverse(T label)
        {
            if(IsNull(label) || !NodeExists(label)) return;
            DepthFirstTraverse(_nodes[label],new HashSet<Node>());
        }
        private void DepthFirstTraverse(Node current, ISet<Node> visited)
        {
            Console.WriteLine($"{current.Label}");
            visited.Add(current);
            foreach (var (_, node) in current.Nodes)
                if(!visited.Contains(node))
                    DepthFirstTraverse(node,visited);
        }

        public void BreadthFirstTraverse(T label)
        {
            if (IsNull(label) || !NodeExists(label)) return;
            
            var queue = new Queue<Node>();
            queue.Enqueue(_nodes[label]);

            var visited = new HashSet<Node>();

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if(visited.Contains(current))
                    continue;

                Console.WriteLine(current.Label.ToString());
                visited.Add(current);

                foreach (var (_,node) in current.Nodes)
                    if(!visited.Contains(node))
                        queue.Enqueue(node);
            }

        }

        public List<T> TopologicalSort()
        {
            if(_nodes.Count == 0) return new List<T>();

            var stack = new Stack<Node>();
            TopologicalSort(_nodes.First().Value, new HashSet<Node>(), stack);

            return stack.Select(s=>s.Label).ToList();
        }
        private void TopologicalSort(Node current, ISet<Node> visited, Stack<Node> stack)
        {
            visited.Add(current);

            foreach (var (_, node) in current.Nodes)
                if (!visited.Contains(node))
                    TopologicalSort(node, visited,stack);

            stack.Push(current);
        }

        public bool HasCycle()
        {
            if (_nodes.Count == 0) return false;

            var all = _nodes.Values.ToHashSet();
            
            var visited = new HashSet<Node>();
            var visiting = new HashSet<Node>();

            while (all.Count != 0)
            {
                var current = all.First();
                if (HasCycle(current, all, visiting, visited))
                    return true;
            }

            return false;
        }
        private bool HasCycle(in Node node,ISet<Node> all,ISet<Node> visiting, ISet<Node> visited)
        {
            all.Remove(node);
            visiting.Add(node);

            foreach (var (_, neighbor) in node.Nodes)
            {
                if(visited.Contains(neighbor)) continue;
                if (visiting.Contains(neighbor)) return true;

                if (HasCycle(neighbor, all, visiting, visited))
                    return true;
            }

            visiting.Remove(node);
            visited.Add(node);

            return false;
        }

        // Same As above 
        public bool HasCycle2()
        {
            var all = _nodes.Values.ToHashSet();
            return HasCycle2(all.First(),all, new HashSet<Node>(), new HashSet<Node>());
        }
        private bool HasCycle2(Node node, ISet<Node> all, ISet<Node> visiting, ISet<Node> visited)
        {
            if (visiting.Contains(node))
                return true;

            all.Remove(node);
            visiting.Add(node);

            foreach (var (_,neighbor) in node.Nodes)
            {
                if(visited.Contains(neighbor)) continue;
                 
                if (HasCycle2(neighbor, all, visiting, visited))
                    return true;
            }

            visiting.Remove(node);
            visited.Add(node);
            return false;
        }

        public void Print()
        {
            foreach (var node in _nodes)
            {
                Console.Write($"{node.Key} is connected to ");

                if(node.Value.Nodes.Count == 0)
                    Console.Write("no one");

                foreach (var to in node.Value.Nodes)
                    Console.Write($"{to.Value.Label}, ");
                Console.WriteLine();
            }
        }

        private bool NodeExists(T label)
        {
            return _nodes.ContainsKey(label);
        }

        private static bool IsNull(T label)
        {
            switch (label)
            {
                case null:
                case string _ when string.IsNullOrWhiteSpace(label.ToString()):
                    return true;
                default:
                    return false;
            }
        }
    }
   
}
