using EventHubTicket.Management.Domain.Common;

namespace EventHubTicket.Management.Domain.Entities
{
    public class Event : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? Organizer { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
