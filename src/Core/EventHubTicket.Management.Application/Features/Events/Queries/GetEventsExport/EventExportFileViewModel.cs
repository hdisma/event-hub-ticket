namespace EventHubTicket.Management.Application.Features.Events.Queries.GetEventsExport
{
    public class EventExportFileViewModel
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[]? Data { get; set; }
    }
}