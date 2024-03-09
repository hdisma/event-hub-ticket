using EventHubTicket.Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventHubTicket.Management.Persistence.DbContexts
{
    public class EventHubTicketDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public EventHubTicketDbContext(DbContextOptions<EventHubTicketDbContext> options)
            : base(options)
        {
        }
    }
}
