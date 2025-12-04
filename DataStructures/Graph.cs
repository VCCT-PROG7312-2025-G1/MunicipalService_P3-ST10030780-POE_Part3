// DataStructures/Graph.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalService_P3.DataStructures
{
    // Undirected weighted graph: nodes are ServiceRequest IDs or zone IDs
    public class Graph
    {
        private readonly Dictionary<int, List<(int Neighbor, int Weight)>> adj = new();

        public void AddVertex(int v)
        {
            if (!adj.ContainsKey(v)) adj[v] = new List<(int, int)>();
        }

        public void AddEdge(int a, int b, int weight = 1)
        {
            AddVertex(a); AddVertex(b);
            adj[a].Add((b, weight));
            adj[b].Add((a, weight));
        }

        public IEnumerable<int> Vertices => adj.Keys;

        public IEnumerable<(int Neighbor, int Weight)> Neighbors(int v) =>
            adj.ContainsKey(v) ? adj[v] : Enumerable.Empty<(int, int)>();

        public List<int> BFS(int start)
        {
            var visited = new HashSet<int>(); var q = new Queue<int>(); var order = new List<int>();
            visited.Add(start); q.Enqueue(start);
            while (q.Count > 0)
            {
                var u = q.Dequeue(); order.Add(u);
                foreach (var (n, _) in Neighbors(u)) if (!visited.Contains(n)) { visited.Add(n); q.Enqueue(n); }
            }
            return order;
        }

        public List<int> DFS(int start)
        {
            var visited = new HashSet<int>(); var stack = new Stack<int>(); var order = new List<int>();
            stack.Push(start);
            while (stack.Count > 0)
            {
                var u = stack.Pop();
                if (visited.Contains(u)) continue;
                visited.Add(u); order.Add(u);
                foreach (var (n, _) in Neighbors(u)) if (!visited.Contains(n)) stack.Push(n);
            }
            return order;
        }

        public List<(int U, int V, int W)> PrimMST()
        {
            var res = new List<(int, int, int)>();
            if (!adj.Any()) return res;

            var start = adj.Keys.First();
            var inMst = new HashSet<int> { start };
            var pq = new PriorityQueue<(int U, int V, int W), int>();
            foreach (var (n, w) in adj[start]) pq.Enqueue((start, n, w), w);

            while (pq.Count > 0 && inMst.Count < adj.Count)
            {
                var e = pq.Dequeue();
                if (inMst.Contains(e.V)) continue;
                inMst.Add(e.V); res.Add((e.U, e.V, e.W));

                foreach (var (nn, ww) in Neighbors(e.V))
                    if (!inMst.Contains(nn)) pq.Enqueue((e.V, nn, ww), ww);
            }
            return res;
        }

        public List<(int U, int V, int W)> EdgeList()
        {
            var list = new List<(int, int, int)>(); var seen = new HashSet<(int, int)>();
            foreach (var u in adj.Keys)
                foreach (var (v, w) in adj[u])
                {
                    var key = u < v ? (u, v) : (v, u);
                    if (seen.Contains(key)) continue;
                    seen.Add(key); list.Add((u, v, w));
                }
            return list;
        }
    }
}