using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverterLibrary.Utils
{
    public class Graph<T>
    {
        public Dictionary<T, HashSet<T>> NeighborNodesList { get; }
        public Graph(IEnumerable<T> nodes, IEnumerable<Tuple<T, T>> edges)
        {
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

        public Func<T, IEnumerable<T>> GetShortestPathFunction(T startNode)
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

            Func<T, IEnumerable<T>> shortestPath = (endNode) => {
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
