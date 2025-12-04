namespace MunicipalService_P3.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string MediaPath { get; set; } = string.Empty;
    }
}