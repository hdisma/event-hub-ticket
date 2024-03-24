namespace EventHubTicket.Management.Application.Abstractions.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportToCsv<T>(IReadOnlyList<T> data) where T : class;
    }
}
