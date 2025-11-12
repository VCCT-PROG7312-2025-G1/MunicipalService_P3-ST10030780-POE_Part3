// Services/InMemoryDataService.cs
using MunicipalService_P3.Models;

namespace MunicipalService_P3.Services
{
    public class InMemoryDataService : IDataService
    {
        private readonly List<Issue> _issues = new();
        private readonly SortedDictionary<DateTime, List<Event>> _eventsByDate = new();
        private readonly Dictionary<int, Event> _eventIndex = new();
        private readonly HashSet<string> _eventCategories = new(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<(string? keyword, string? category)> _searchStack = new();
        private readonly Queue<ServiceRequest> _requestQueue = new();
        private readonly Dictionary<int, ServiceRequest> _requests = new();
        private readonly PriorityQueue<ServiceRequest, int> _priorityQueue = new();
        private readonly Dictionary<int, List<int>> _dependencyGraph = new();

        private int _issueSeq = 1;
        private int _eventSeq = 1;
        private int _requestSeq = 1000;

        public InMemoryDataService()
        {
            SeedEvents();
            SeedServiceRequests();
        }

        // Issues
        public void AddIssue(Issue issue)
        {
            issue.Id = _issueSeq++;
            issue.Timestamp = DateTime.UtcNow;
            _issues.Add(issue);
        }
        public List<Issue> GetIssues() => _issues.OrderByDescending(i => i.Timestamp).ToList();

        // Events
        public List<Event> GetEvents(string? keyword = null, string? category = null, DateTime? date = null)
        {
            _searchStack.Push((keyword, category));
            IEnumerable<Event> all = _eventsByDate.Values.SelectMany(v => v);
            if (!string.IsNullOrWhiteSpace(keyword))
                all = all.Where(e => e.Title.Contains(keyword!, StringComparison.OrdinalIgnoreCase)
                                   || e.Description.Contains(keyword!, StringComparison.OrdinalIgnoreCase)
                                   || e.Location.Contains(keyword!, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(category))
                all = all.Where(e => string.Equals(e.Category, category, StringComparison.OrdinalIgnoreCase));
            if (date.HasValue)
                all = all.Where(e => e.EventDate.Date == date.Value.Date);
            return all.OrderBy(e => e.EventDate).ToList();
        }
        public HashSet<string> GetEventCategories() => _eventCategories;

        public List<Event> GetRecommendations(string? keyword, string? category)
        {
            var recent = _searchStack.Take(5).ToList();
            var preferredCategory = category ?? recent.FirstOrDefault(s => !string.IsNullOrWhiteSpace(s.category)).category;
            IEnumerable<Event> all = _eventsByDate.Values.SelectMany(v => v);
            if (!string.IsNullOrWhiteSpace(preferredCategory))
                all = all.Where(e => string.Equals(e.Category, preferredCategory, StringComparison.OrdinalIgnoreCase));
            return all.OrderBy(e => e.EventDate).Take(5).ToList();
        }

        // Service requests
        public int EnqueueServiceRequest(ServiceRequest req)
        {
            req.RequestId = _requestSeq++;
            req.CreatedAt = DateTime.UtcNow;
            _requestQueue.Enqueue(req);
            _requests[req.RequestId] = req;
            _priorityQueue.Enqueue(req, -req.Priority);
            if (!_dependencyGraph.ContainsKey(req.RequestId)) _dependencyGraph[req.RequestId] = new();
            return req.RequestId;
        }
        public List<ServiceRequest> GetAllServiceRequests() =>
            _requests.Values.OrderByDescending(r => r.Priority).ThenBy(r => r.CreatedAt).ToList();

        public ServiceRequest? FindRequest(int requestId) =>
            _requests.TryGetValue(requestId, out var r) ? r : null;

        public ServiceRequest? GetTopPriority() =>
            _priorityQueue.Count > 0 ? _priorityQueue.Peek() : null;

        public List<int> GetDependencies(int requestId) =>
            _dependencyGraph.TryGetValue(requestId, out var deps) ? deps : new List<int>();

        private void SeedEvents()
        {
            var today = DateTime.Today;
            var seed = new List<Event>
            {
                new() { Id = _eventSeq++, Title = "Community Cleanup", Description = "Help keep our town clean.", Category = "Environment", EventDate = today.AddDays(10), Location = "Vyeboom" },
                new() { Id = _eventSeq++, Title = "Church Bazaar", Description = "Support the church fundraiser.", Category = "Market", EventDate = today.AddDays(5), Location = "Vyeboom Church" },
                new() { Id = _eventSeq++, Title = "Park Run", Description = "First ever park run!", Category = "Sport", EventDate = today.AddDays(15), Location = "Vyeboom Park" },
                new() { Id = _eventSeq++, Title = "Water Update", Description = "Maintenance schedule.", Category = "Utilities", EventDate = today.AddDays(2), Location = "Town Hall" }
            };
            foreach (var e in seed)
            {
                if (!_eventsByDate.ContainsKey(e.EventDate.Date)) _eventsByDate[e.EventDate.Date] = new();
                _eventsByDate[e.EventDate.Date].Add(e);
                _eventIndex[e.Id] = e;
                _eventCategories.Add(e.Category);
            }
        }

        private void SeedServiceRequests()
        {
            var r1 = new ServiceRequest { Description = "Burst pipe near Main Rd", Status = "In Progress", Priority = 5 };
            var r2 = new ServiceRequest { Description = "Streetlight out on 3rd Ave", Status = "Pending", Priority = 2 };
            var r3 = new ServiceRequest { Description = "Pothole repair on Oak St", Status = "Scheduled", Priority = 3 };

            EnqueueServiceRequest(r1);
            EnqueueServiceRequest(r2);
            EnqueueServiceRequest(r3);

            // Graph edge: r3 depends on r1
            _dependencyGraph[r3.RequestId].Add(r1.RequestId);
        }
    }
}