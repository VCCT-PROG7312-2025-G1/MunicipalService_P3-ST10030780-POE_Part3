namespace MunicipalService_P3.Models
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
    }
}