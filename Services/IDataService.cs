// Services/IDataService.cs
using MunicipalService_P3.Models;
namespace MunicipalService_P3.Services
{
    public interface IDataService
    {
        // Issues
        void AddIssue(Issue issue);
        List<Issue> GetIssues();

        // Events
        List<Event> GetEvents(string? keyword = null, string? category = null, DateTime? date = null);
        HashSet<string> GetEventCategories();
        List<Event> GetRecommendations(string? keyword, string? category);

        // Service requests
        int EnqueueServiceRequest(ServiceRequest req);
        List<ServiceRequest> GetAllServiceRequests();
        ServiceRequest? FindRequest(int requestId);
        ServiceRequest? GetTopPriority();
        List<int> GetDependencies(int requestId);
    }
}