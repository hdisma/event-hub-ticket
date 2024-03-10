using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Persistence.DbContexts;
using EventHubTicket.Management.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventHubTicket.Management.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<EventHubTicketDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EventHubTicket")));

            services
                .AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>))
                .AddScoped<IEventRepository, EventRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
