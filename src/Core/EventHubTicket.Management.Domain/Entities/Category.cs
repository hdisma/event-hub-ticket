using EventHubTicket.Management.Domain.Common;

namespace EventHubTicket.Management.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
