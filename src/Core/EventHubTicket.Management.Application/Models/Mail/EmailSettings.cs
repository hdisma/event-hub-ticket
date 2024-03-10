namespace EventHubTicket.Management.Application.Models.Mail
{
    public class EmailSettings
    {
        public required string ApiKey { get; set; }
        public required string FromAddress { get; set; }
        public string FromName { get; set; } = string.Empty;
    }
}
