using EventHubTicket.Management.Application.Models.Mail;

namespace EventHubTicket.Management.Application.Abstractions.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
