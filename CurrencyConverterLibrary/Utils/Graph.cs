using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverterLibrary.Utils
{
    public class Graph<T>
    {
        private Dictionary<T, HashSet<T>> NeighborNodesList { get; }
        public HashSet<T> Nodes { get; }
        public Graph(HashSet<T> nodes, IEnumerable<Tuple<T, T>> edges)
        {
            Nodes = nodes;
            NeighborNodesList = new Dictionary<T, HashSet<T>>();
            foreach (var node in nodes)
                NeighborNodesList.Add(node, new HashSet<T>());

            foreach (var edge in edges)
            {
                NeighborNodesList[edge.Item1].Add(edge.Item2);
                NeighborNodesList[edge.Item2].Add(edge.Item1);
            }
        }
        public static Graph<T> CreateGraph(IEnumerable<Tuple<T, T>> edges)
        {
            HashSet<T> nodes = new HashSet<T>();
            foreach (var edge in edges)
            {
                nodes.Add(edge.Item1);
                nodes.Add(edge.Item2);
            }
            return new Graph<T>(nodes, edges);
        }
        public static Graph<T> CreateGraph(IEnumerable<Tuple<T, T,double>> rates)
        {
            HashSet<T> nodes = new HashSet<T>();
            List<Tuple<T, T>> edges = new List<Tuple<T, T>>();
            foreach (var rate in rates)
            {
                nodes.Add(rate.Item1);
                nodes.Add(rate.Item2);
                edges.Add(Tuple.Create(rate.Item1, rate.Item2));
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
                foreach (var neighbor in NeighborNodesList[currentNode])
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
}
