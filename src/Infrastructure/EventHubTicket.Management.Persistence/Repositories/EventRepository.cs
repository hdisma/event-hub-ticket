using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Domain.Entities;
using EventHubTicket.Management.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EventHubTicket.Management.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(EventHubTicketDbContext eventHubTicketDbContext)
            : base(eventHubTicketDbContext)
        {
        }

        public async Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate)
        {
            return await _dbContext.Events
                .AnyAsync(e => e.Name == name && e.Date.Date.Equals(eventDate));
        }
    }
}
