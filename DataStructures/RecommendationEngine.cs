// DataStructures/RecommendationEngine.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalService_P3.DataStructures
{
    public class RecommendationEngine
    {
        // Track keyword frequencies from searches
        private readonly Dictionary<string, int> keywordCounts = new(StringComparer.OrdinalIgnoreCase);

        // Events indexed by ID; metadata stored for matching
        public class EventItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Category { get; set; } // roads, sanitation, utilities
            public DateTime Date { get; set; }
            public List<string> Tags { get; set; } = new();
        }

        private readonly Dictionary<int, EventItem> events = new();

        public void AddEvent(EventItem item) => events[item.Id] = item;

        public void RecordSearch(string query)
        {
            foreach (var token in Tokenize(query))
            {
                if (keywordCounts.ContainsKey(token)) keywordCounts[token]++;
                else keywordCounts[token] = 1;
            }
        }

        public List<EventItem> Recommend(int topN = 3)
        {
            if (!keywordCounts.Any()) return events.Values.Take(topN).OrderBy(e => e.Date).ToList();

            // score events by keyword overlap (title, category, tags)
            var scores = new List<(EventItem Item, int Score)>();
            foreach (var e in events.Values)
            {
                int score = 0;
                foreach (var kv in keywordCounts)
                {
                    var k = kv.Key; var w = kv.Value;
                    if ((e.Title?.IndexOf(k, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0) score += 2 * w;
                    if ((e.Category?.IndexOf(k, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0) score += 2 * w;
                    if (e.Tags.Any(t => t.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0)) score += w;
                }
                scores.Add((e, score));
            }
            return scores.OrderByDescending(s => s.Score).ThenBy(s => s.Item.Date).Take(topN).Select(s => s.Item).ToList();
        }

        private IEnumerable<string> Tokenize(string q)
        {
            return (q ?? "").Split(new[] { ' ', ',', '.', ';', ':', '-', '_' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim().ToLowerInvariant());
        }
    }
}