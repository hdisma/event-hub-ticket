using EventHubTicket.Management.Domain.Common;

namespace EventHubTicket.Management.Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public int Total { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsPaid { get; set; }
        public Guid UserId { get; set; }
    }
}
