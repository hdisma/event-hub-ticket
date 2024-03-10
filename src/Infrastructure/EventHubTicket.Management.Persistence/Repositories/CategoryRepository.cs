using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Domain.Entities;
using EventHubTicket.Management.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EventHubTicket.Management.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EventHubTicketDbContext eventHubTicketDbContext)
            : base(eventHubTicketDbContext)
        {
        }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents)
        {
            var categories = _dbContext.Categories.Include(c => c.Events);

            if (!includePassedEvents)
            {
                categories = (IIncludableQueryable<Category, ICollection<Event>?>)categories
                    .Where(c => c.Events!.All(e => e.Date >= DateTime.Now));
            }

            return await categories.ToListAsync();
        }
    }
}
