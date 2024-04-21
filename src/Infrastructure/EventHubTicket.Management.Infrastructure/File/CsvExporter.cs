using CsvHelper;
using EventHubTicket.Management.Application.Abstractions.Infrastructure;
using System.Globalization;

// TODO: Update ExportToCsv to be async
namespace EventHubTicket.Management.Infrastructure.File
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportToCsv<T>(IReadOnlyList<T> data) where T : class
        {
            using var memoryStream = new MemoryStream();

            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(data);
            }

            return memoryStream.ToArray();
        }
    }
}
