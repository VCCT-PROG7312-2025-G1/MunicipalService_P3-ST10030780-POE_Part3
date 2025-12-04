// Models/ServiceRequest.cs
namespace MunicipalService_P3.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }                    // unique identifier
        public string Category { get; set; }           // e.g., sanitation, roads
        public string Description { get; set; }
        public string Location { get; set; }
        public int Priority { get; set; }              // lower = more urgent
        public string Status { get; set; }             // e.g., Submitted, In Progress, Resolved
        public int RequestId { get; internal set; }
        public DateTime CreatedAt { get; set; } // Add this property to fix CS1061

        public override string ToString()
        {
            return $"#{Id} [{Category}] - {Description} (Priority {Priority}, Status: {Status})";
        }
    }
}