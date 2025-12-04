using System;
using System.Collections.Generic;
using System.Linq;
using MunicipalService_P3.Models;   // ✅ Adjusted to your project namespace

namespace MunicipalService_P3.DataStructures
{
    public class Graph
    {
        // adjacency list: requestId -> list of (neighborId, weight)
        private readonly Dictionary<int, List<(int Neighbor, int Weight)>> adj
            = new Dictionary<int, List<(int, int)>>();

        // Add a request node
        public void AddVertex(int requestId)
        {
            if (!adj.ContainsKey(requestId))
                adj[requestId] = new List<(int, int)>();
        }

        // Add a dependency edge between two requests
        public void AddEdge(int fromRequestId, int toRequestId, int weight = 1)
        {
            AddVertex(fromRequestId);
            AddVertex(toRequestId);

            adj[fromRequestId].Add((toRequestId, weight));
            adj[toRequestId].Add((fromRequestId, weight)); // undirected for MST
        }

        // All request IDs in the graph
        public IEnumerable<int> Vertices => adj.Keys;

        // Neighbors of a given request
        public IEnumerable<(int Neighbor, int Weight)> Neighbors(int requestId) =>
            adj.ContainsKey(requestId) ? adj[requestId] : Enumerable.Empty<(int, int)>();

        // Breadth-first search (e.g., find all connected requests)
        public List<int> BFS(int startRequestId)
        {
            var visited = new HashSet<int>();
            var q = new Queue<int>();
            var order = new List<int>();

            visited.Add(startRequestId);
            q.Enqueue(startRequestId);

            while (q.Count > 0)
            {
                var u = q.Dequeue();
                order.Add(u);

                foreach (var (n, _) in Neighbors(u))
                {
                    if (!visited.Contains(n))
                    {
                        visited.Add(n);
                        q.Enqueue(n);
                    }
                }
            }
            return order;
        }

        // Depth-first search (e.g., explore dependencies deeply)
        public List<int> DFS(int startRequestId)
        {
            var visited = new HashSet<int>();
            var stack = new Stack<int>();
            var order = new List<int>();

            stack.Push(startRequestId);

            while (stack.Count > 0)
            {
                var u = stack.Pop();
                if (visited.Contains(u)) continue;

                visited.Add(u);
                order.Add(u);

                foreach (var (n, _) in Neighbors(u))
                {
                    if (!visited.Contains(n))
                        stack.Push(n);
                }
            }
            return order;
        }

        // Prim's Minimum Spanning Tree (e.g., find minimal dependency network)
        public List<(int U, int V, int W)> PrimMST()
        {
            var res = new List<(int, int, int)>();
            if (!adj.Any()) return res;

            var start = adj.Keys.First();
            var inMst = new HashSet<int> { start };
            var pq = new PriorityQueue<(int U, int V, int W), int>();

            foreach (var (n, w) in adj[start])
                pq.Enqueue((start, n, w), w);

            while (pq.Count > 0 && inMst.Count < adj.Count)
            {
                var e = pq.Dequeue();
                if (inMst.Contains(e.V)) continue;

                inMst.Add(e.V);
                res.Add((e.U, e.V, e.W));

                foreach (var (nn, ww) in Neighbors(e.V))
                {
                    if (!inMst.Contains(nn))
                        pq.Enqueue((e.V, nn, ww), ww);
                }
            }
            return res;
        }

        // Return all edges (for debugging or visualization)
        public List<(int U, int V, int W)> EdgeList()
        {
            var list = new List<(int, int, int)>();
            var seen = new HashSet<(int, int)>();

            foreach (var u in adj.Keys)
            {
                foreach (var (v, w) in adj[u])
                {
                    var key = u < v ? (u, v) : (v, u);
                    if (seen.Contains(key)) continue;

                    seen.Add(key);
                    list.Add((u, v, w));
                }
            }
            return list;
        }
    }
}