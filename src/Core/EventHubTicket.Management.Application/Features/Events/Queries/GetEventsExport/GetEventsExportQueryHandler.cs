using EventHubTicket.Management.Application.Abstractions.Infrastructure;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Domain.Entities;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery,
        EventExportFileViewModel>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly ICsvExporter _csvExporter;

        public GetEventsExportQueryHandler(
            IAsyncRepository<Event> eventRepository,
            ICsvExporter csvExporter)
        {
            _eventRepository = eventRepository;
            _csvExporter = csvExporter;
        }

        public async Task<EventExportFileViewModel> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetAllAsync();

            return new EventExportFileViewModel
            {
                FileName = $"{Guid.NewGuid()}.csv",
                ContentType = "text/csv",
                Data = _csvExporter.ExportToCsv(events)
            };
        }
    }
}
