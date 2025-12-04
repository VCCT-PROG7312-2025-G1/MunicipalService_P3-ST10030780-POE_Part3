using MunicipalService_P3.Models;

namespace MunicipalService_P3.Services
{
    public interface IDataService
    {
        void AddIssue(Issue issue);
        List<Issue> GetIssues();

        List<Event> GetEvents(string? keyword = null, string? category = null, DateTime? date = null);
        HashSet<string> GetEventCategories();
        List<Event> GetRecommendations(string? keyword, string? category);

        int EnqueueServiceRequest(ServiceRequest request);
        List<ServiceRequest> GetAllServiceRequests();
        ServiceRequest? FindRequest(int id);
        ServiceRequest? GetTopPriority();
        List<int> GetDependencies(int id);
    }
}