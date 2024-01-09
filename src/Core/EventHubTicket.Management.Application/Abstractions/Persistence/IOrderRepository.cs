using EventHubTicket.Management.Domain.Entities;

namespace EventHubTicket.Management.Application.Abstractions.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
    }
}
