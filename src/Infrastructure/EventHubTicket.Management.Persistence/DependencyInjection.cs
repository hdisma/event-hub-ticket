using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Persistence.DbContexts;
using EventHubTicket.Management.Persistence.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventHubTicket.Management.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(configuration.GetConnectionString("EventHubTicket"));
#if DEBUG
            builder["Server"] = "localhost,1433";
#endif
            services.AddDbContext<EventHubTicketDbContext>(options =>
                options.UseSqlServer(builder.ConnectionString));

            services
                .AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>))
                .AddScoped<IEventRepository, EventRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
