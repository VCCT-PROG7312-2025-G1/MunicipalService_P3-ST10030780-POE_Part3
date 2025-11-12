using System.Collections.Generic;

namespace MunicipalService_P3.Models.DataStructures
{
    public class StatusGraph
    {
        private readonly Dictionary<int, List<int>> _adjacencyList = new();

        public void AddRequest(int requestId)
        {
            if (!_adjacencyList.ContainsKey(requestId))
                _adjacencyList[requestId] = new List<int>();
        }

        public void AddDependency(int fromRequestId, int toRequestId)
        {
            AddRequest(fromRequestId);
            AddRequest(toRequestId);
            _adjacencyList[fromRequestId].Add(toRequestId);
        }

        public List<int> GetDependencies(int requestId)
        {
            return _adjacencyList.TryGetValue(requestId, out var deps) ? deps : new List<int>();
        }

        public List<int> TraverseFrom(int startId)
        {
            var visited = new HashSet<int>();
            var result = new List<int>();
            DFS(startId, visited, result);
            return result;
        }

        private void DFS(int current, HashSet<int> visited, List<int> result)
        {
            if (!visited.Add(current)) return;
            result.Add(current);
            if (_adjacencyList.TryGetValue(current, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                    DFS(neighbor, visited, result);
            }
        }
    }
}