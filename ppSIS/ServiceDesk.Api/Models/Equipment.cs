namespace ServiceDesk.Api.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; } = "";
        public string Type { get; set; } = "";
        public string Room { get; set; } = "";
        public string Status { get; set; } = "Работает";
    }
}
