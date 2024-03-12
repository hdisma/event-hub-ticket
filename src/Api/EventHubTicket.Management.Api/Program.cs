using EventHubTicket.Management.Application;
using EventHubTicket.Management.Infrastructure;
using EventHubTicket.Management.Persistence;
using EventHubTicket.Management.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EventHubTicket.Management.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddPersistence(builder.Configuration);

            builder.Services.AddControllers();

            var app = builder.Build();

            ResetDatabaseAsync(app).Wait();

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }

        public static async Task ResetDatabaseAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetService<EventHubTicketDbContext>();

                if (context is not null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception)
            {
                // TODO: Log exception here
                throw;
            }
        }
    }
}
