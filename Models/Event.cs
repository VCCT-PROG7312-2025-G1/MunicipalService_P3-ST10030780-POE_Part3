// Models/Event.cs
namespace MunicipalService_P3.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Category { get; set; } = "";
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = "";
    }
}