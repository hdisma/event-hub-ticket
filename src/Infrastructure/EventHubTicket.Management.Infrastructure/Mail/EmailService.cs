using EventHubTicket.Management.Application.Abstractions.Infrastructure;
using EventHubTicket.Management.Application.Models.Mail;

namespace EventHubTicket.Management.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendEmailAsync(Email email)
        {
            throw new NotImplementedException();
        }
    }
}
