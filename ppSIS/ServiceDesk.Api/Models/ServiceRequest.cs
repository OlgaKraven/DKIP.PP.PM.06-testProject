namespace ServiceDesk.Api.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Priority { get; set; } = "Средний";
        public string Status { get; set; } = "Новая";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }

        public int? EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }
    }
}
