using EventHubTicket.Management.Application.Abstractions.Infrastructure;
using EventHubTicket.Management.Application.Models.Mail;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

// TODO: This might need some refactor in the future...
namespace EventHubTicket.Management.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings { get; }

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendgridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendgridMessage);

            if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
