using EventHubTicket.Management.Domain.Entities;

namespace EventHubTicket.Management.Application.Abstractions.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
    }
}
