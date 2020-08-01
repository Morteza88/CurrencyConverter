using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverterLibrary.Utils
{
    public class Graph<T>
    {
        private Dictionary<T, HashSet<T>> neighborNodesList { get; }
        public HashSet<T> Nodes { get; }
        public Graph(HashSet<T> nodes, IEnumerable<Edge<T>> edges)
        {
            Nodes = nodes;
            neighborNodesList = new Dictionary<T, HashSet<T>>();
            foreach (var node in nodes)
                neighborNodesList.Add(node, new HashSet<T>());

            foreach (var edge in edges)
            {
                neighborNodesList[edge.NodeA].Add(edge.NodeB);
                neighborNodesList[edge.NodeB].Add(edge.NodeA);
            }
        }
        public static Graph<T> CreateGraph(IEnumerable<Edge<T>> edges)
        {
            HashSet<T> nodes = new HashSet<T>();
            foreach (var edge in edges)
            {
                nodes.Add(edge.NodeA);
                nodes.Add(edge.NodeB);
            }
            return new Graph<T>(nodes, edges);
        }
        public static Graph<T> CreateGraph(IEnumerable<Tuple<T, T,double>> rates)
        {
            HashSet<T> nodes = new HashSet<T>();
            List<Edge<T>> edges = new List<Edge<T>>();
            foreach (var rate in rates)
            {
                nodes.Add(rate.Item1);
                nodes.Add(rate.Item2);
                edges.Add(new Edge<T>(rate.Item1, rate.Item2));
            }
            return new Graph<T>(nodes, edges);
        }

        public Func<T, List<T>> GetShortestPathFunction(T startNode)
        {
            var previousNodes = new Dictionary<T, T>();
            var queue = new Queue<T>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                foreach (var neighbor in neighborNodesList[currentNode])
                {
                    if (previousNodes.ContainsKey(neighbor))
                        continue;
                    previousNodes[neighbor] = currentNode;
                    queue.Enqueue(neighbor);
                }
            }

            Func<T, List<T>> shortestPath = (endNode) => {
                var path = new List<T> { };
                var current = endNode;
                while (!current.Equals(startNode))
                {
                    if (!previousNodes.ContainsKey(current))
                        return path;
                    path.Add(current);
                    current = previousNodes[current];
                };
                path.Add(startNode);
                path.Reverse();
                return path;
            };

            return shortestPath;
        }
    }

    public struct Edge<T>
    {
        public T NodeA;
        public T NodeB;
        public Edge(T nodeA, T nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }
    }
}
